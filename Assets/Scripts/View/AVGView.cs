using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using KiyuzuDev.ITGWDO.Core;
using KiyuzuDev.ITGWDO.StoryData;

namespace KiyuzuDev.ITGWDO.View
{
    public class AVGView : MonoBehaviour
    {
        #region Singleton

        public static AVGView Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this)
            {
                Destroy(gameObject);
                Instance = this;
            }
        }
        
        private void OnDestroy()
        {
            Destroy(Instance);
        }

        #endregion
        
        private void OnEnable()
        {
            haveTextTypewriter = false;
            haveMindTypewriter = false;
            if (TextTimePerChar < 0f) TextTimePerChar = 0.1f;
            if (MindTimePerChar < 0f) MindTimePerChar = 0.1f;
            continuePicAnim = continuePic.GetComponent<Animator>();
            continuePicImg = continuePic.GetComponent<Image>();
            continuePic.SetActive(false);
            mindContainer.SetActive(false);
        }

        #region 风格改变

        private const string pathModernSprites = "Sprites/Theme/modern/";
        private const string pathRPGSprites = "Sprites/Theme/rpg/";
        private const string pathUtopiaSprites = "Sprites/Theme/utopia/";
        
        [SerializeField] private GameObject nameBox;
        [SerializeField] private Image dialogueBox;
        [SerializeField] private GameObject continuePic;
        [SerializeField] private Image mindImg;
        [SerializeField] private Image announcerImg;
        private Image continuePicImg;
        private Animator continuePicAnim;
        [SerializeField] private Sprite modernDown;
        [SerializeField] private Sprite rpgDown;
        [SerializeField] private Sprite utopiaDown;
        private string currentState;
        
        public void ChangeToStyleView(WorldStyle _style)
        {
            switch (_style)
            {
                case WorldStyle.Modern:
                    nameBox.GetComponent<Image>().sprite =
                        Resources.Load<Sprite>(pathModernSprites + "gui/modern_d_namebox");
                    dialogueBox.sprite = Resources.Load<Sprite>(pathModernSprites + "gui/modern_d_textbox");
                    mindImg.sprite = Resources.Load<Sprite>(pathModernSprites + "gui/modern_mindbox");
                    announcerImg.sprite = Resources.Load<Sprite>(pathModernSprites + "gui/modern_announcer");
                    continuePicImg.sprite = modernDown;
                    currentState = "modern_d_down_aniclip";
                    break;
                case WorldStyle.RPG:
                    nameBox.GetComponent<Image>().sprite =
                        Resources.Load<Sprite>(pathRPGSprites + "gui/rpg_d_namebox");
                    dialogueBox.sprite = Resources.Load<Sprite>(pathRPGSprites + "gui/rpg_d_textbox");
                    mindImg.sprite = Resources.Load<Sprite>(pathRPGSprites + "gui/rpg_mindbox");
                    announcerImg.sprite = Resources.Load<Sprite>(pathRPGSprites + "gui/rpg_announcer");
                    continuePicImg.sprite = rpgDown;
                    currentState = "rpg_d_down_aniclip";
                    break;
                case WorldStyle.Utopia:
                    nameBox.GetComponent<Image>().sprite =
                        Resources.Load<Sprite>(pathUtopiaSprites + "gui/utopia_d_namebox");
                    dialogueBox.sprite = Resources.Load<Sprite>(pathUtopiaSprites + "gui/utopia_d_textbox");
                    mindImg.sprite = Resources.Load<Sprite>(pathUtopiaSprites + "gui/utopia_mindbox");
                    announcerImg.sprite = Resources.Load<Sprite>(pathUtopiaSprites + "gui/utopia_announcer");
                    continuePicImg.sprite = utopiaDown;
                    currentState = "utopia_d_down_aniclip";
                    break;
            }
            continuePicAnim.Play(currentState);
            // shelf.sprite
            // wineListImg.sprite
            // wineListBtn.sprite
        }
        
        #endregion
        
        #region 主文本显示

        [Range(0, 1)] private static float TextTimePerChar = 0.01f;

        [SerializeField] private GameObject dialogueContainer;
        [SerializeField] private TMP_Text textName;
        [SerializeField] private TMP_Text textDialogue;
        // [SerializeField] private Image continuePic;
        
        private static readonly string[] _uguiSymbols = { "b", "i" };
        private static readonly string[] _uguiCloseSymbols = { "b", "i", "size", "color" };
        
        private string contentPassed;
        public bool haveTextTypewriter;

        public void UpdateText(string personName, string content)
        {
            contentPassed = content.Replace("\\n", "\n").Replace("\\t","\t");
            if (personName == "") UnshowNameBox();
            else ShowNameBox(personName);
            continuePic.SetActive(false);
            haveTextTypewriter = true;
            StartCoroutine(TextTypewriter(contentPassed));
        }
        
        private IEnumerator TextTypewriter(string _text = "")
        {
            textDialogue.text = "";
            string _tmpText = "";
            var length = _text.Length;
            bool tagOpened = false;
            string tagType = "";
            for (var i = 0; i < length; i++)
            {
                bool symbolDetected = false;
                // Enter Symbol
                foreach (var symbol in _uguiSymbols)
                {
                    var sign = $"<{symbol}>";
                    if (_text[i] == '<' 
                        && i + 1 + symbol.Length < length 
                        && _text.Substring(i, 2 + symbol.Length).Equals(sign))
                    {
                        _tmpText += sign;
                        i += 2 + symbol.Length - 1;
                        symbolDetected = true;
                        tagOpened = true;
                        tagType = symbol;
                        break;
                    }
                }
                
                // take color
                if (_text[i] == '<'
                    && i + (1 + 15) < length
                    && _text.Substring(i, 2 + 6).Equals("<color=#")
                    && _text[i + 16] == '>')
                {
                    _tmpText += _text.Substring(i, 2 + 6 + 8);
                    i += (2 + 14) - 1;
                    symbolDetected = true;
                    tagOpened = true;
                    tagType = "color";
                }

                // take size
                if (_text[i] == '<'
                    && i + 5 < length
                    && _text.Substring(i, 6).Equals("<size="))
                {
                    string parseSize = "";
                    float size = textDialogue.fontSize;
                    for (var j = i + 6; j < length; j++)
                    {
                        if (_text[j] == '>') break;
                        parseSize += _text[j];
                    }

                    if (float.TryParse(parseSize, out size))
                    {
                        _tmpText += _text.Substring(i, 7 + parseSize.Length);
                        i += (7 + parseSize.Length) - 1;
                        symbolDetected = true;
                        tagOpened = true;
                        tagType = "size";
                    }
                }

                // Exit Symbol
                foreach (var symbol in _uguiCloseSymbols)
                {
                    var sign = $"<{symbol}>";
                    if (_text[i] == '<'
                        && i + 2 + symbol.Length < length
                        && _text.Substring(i, 3 + symbol.Length).Equals(sign))
                    {
                        _tmpText += sign;
                        i += 3 + symbol.Length - 1;
                        symbolDetected = true;
                        tagOpened = false;
                        break;
                    }
                }

                if (symbolDetected) continue;

                _tmpText += _text[i];
                textDialogue.text = _tmpText + (tagOpened ? $"</{tagType}>" : "");
                yield return new WaitForSeconds(TextTimePerChar);
            }
            haveTextTypewriter = false;
            continuePic.SetActive(true);
            continuePicAnim.Play(currentState);
            // Can call a Complete back
        }

        public void SkipTextTypewriter()
        {
            if (haveTextTypewriter == false) return;
            StopCoroutine(TextTypewriter());
            haveTextTypewriter = false;
            continuePic.SetActive(true);
            continuePicAnim.Play(currentState);
            textDialogue.text = contentPassed;
        }
        
        private void ShowNameBox(string _name)
        {
            nameBox.GetComponent<CanvasGroup>().alpha = 1;
            textName.text = _name;
        }

        private void UnshowNameBox()
        {
            textName.text = "";
            nameBox.GetComponent<CanvasGroup>().alpha = 0;
        }

        // continue Pic event

        #endregion

        #region 脑海想法显示

        [Range(0, 1)] private static float MindTimePerChar = 0.005f;
        
        [SerializeField] private GameObject mindContainer;
        [SerializeField] private TMP_Text textMind;
        
        private string mindPassed;
        public bool haveMindTypewriter;

        public bool ismindAva() => mindContainer.activeSelf;
        
        public void SetAvailable()
        {
            mindContainer.SetActive(true);
        }

        public void UpdateMind(string mind)
        {
            mindPassed = mind.Replace("\\n", "\n").Replace("\\t","\t");
            continuePic.SetActive(false);
            haveMindTypewriter = true;
            StartCoroutine(MindTypewriter(mindPassed));
        }

        private IEnumerator MindTypewriter(string _text = "")
        {
            textMind.text = "";
            string _tmpText = "";
            var length = _text.Length;
            bool tagOpened = false;
            string tagType = "";
            for (var i = 0; i < length; i++)
            {
                bool symbolDetected = false;
                // Enter Symbol
                foreach (var symbol in _uguiSymbols)
                {
                    var sign = $"<{symbol}>";
                    if (_text[i] == '<' 
                        && i + 1 + symbol.Length < length 
                        && _text.Substring(i, 2 + symbol.Length).Equals(sign))
                    {
                        _tmpText += sign;
                        i += 2 + symbol.Length - 1;
                        symbolDetected = true;
                        tagOpened = true;
                        tagType = symbol;
                        break;
                    }
                }
                
                // take color
                if (_text[i] == '<'
                    && i + (1 + 15) < length
                    && _text.Substring(i, 2 + 6).Equals("<color=#")
                    && _text[i + 16] == '>')
                {
                    _tmpText += _text.Substring(i, 2 + 6 + 8);
                    i += (2 + 14) - 1;
                    symbolDetected = true;
                    tagOpened = true;
                    tagType = "color";
                }

                // take size
                if (_text[i] == '<'
                    && i + 5 < length
                    && _text.Substring(i, 6).Equals("<size="))
                {
                    string parseSize = "";
                    float size = textMind.fontSize;
                    for (var j = i + 6; j < length; j++)
                    {
                        if (_text[j] == '>') break;
                        parseSize += _text[j];
                    }

                    if (float.TryParse(parseSize, out size))
                    {
                        _tmpText += _text.Substring(i, 7 + parseSize.Length);
                        i += (7 + parseSize.Length) - 1;
                        symbolDetected = true;
                        tagOpened = true;
                        tagType = "size";
                    }
                }

                // Exit Symbol
                foreach (var symbol in _uguiCloseSymbols)
                {
                    var sign = $"<{symbol}>";
                    if (_text[i] == '<'
                        && i + 2 + symbol.Length < length
                        && _text.Substring(i, 3 + symbol.Length).Equals(sign))
                    {
                        _tmpText += sign;
                        i += 3 + symbol.Length - 1;
                        symbolDetected = true;
                        tagOpened = false;
                        break;
                    }
                }

                if (symbolDetected) continue;

                _tmpText += _text[i];
                textMind.text = _tmpText + (tagOpened ? $"</{tagType}>" : "");
                yield return new WaitForSeconds(MindTimePerChar);
            }
            haveMindTypewriter = false;
            continuePic.SetActive(true);
            continuePicAnim.Play(currentState);
            // Can call a Complete back
        }

        public void SkipMindTypewriter()
        {
            if (haveMindTypewriter == false) return;
            StopCoroutine(MindTypewriter());
            haveMindTypewriter = false;
            continuePic.SetActive(true);
            continuePicAnim.Play(currentState);
            textMind.text = mindPassed;
        }

        #endregion

        #region 报幕条

        [SerializeField] private CanvasGroup announcerContainer;
        [SerializeField] private TMP_Text titleLabel;
        [SerializeField] private TMP_Text dayLabel;
        
        public void UpdateAnnouncementTitle(string content)
        {
            string[] array = content.Split("\\n");
            titleLabel.text = array[1];
            dayLabel.text = array[0]; 
            announcerContainer.alpha = 1;
            Invoke(nameof(Invisible), 3);
        }

        void Invisible() => 
            DOTween.To(
                x => announcerContainer.alpha = x,
                1, 0, 0.75f).SetAutoKill(true);

        #endregion

        #region 生成选项

        [SerializeField] private Transform gridButton;
        [SerializeField] private GameObject buttonChoicePrefab;
        public void GenerateChoices()
        {
            for (int id = ScriptManager.PresentLineID + 1;; id++)
            {
                var dLine = ScriptManager.Instance.LoadLineDataPresent(id);
                var btn = Instantiate(buttonChoicePrefab, gridButton);
                btn.GetComponentInChildren<TMP_Text>().text = dLine.content;
                btn.GetComponent<Button>().onClick.AddListener(
                    delegate
                    {
                        OnChoiceClick(dLine.toLine);
                    });
                
                switch (GlobalDataManager.Instance.PresentWorldStyle)
                {
                    case WorldStyle.Modern:
                        btn.GetComponent<Image>().sprite =
                            Resources.Load<Sprite>(pathModernSprites + "gui/modern_choicebutton");
                        break;
                    case WorldStyle.RPG:
                        btn.GetComponent<Image>().sprite =
                            Resources.Load<Sprite>(pathRPGSprites + "gui/rpg_choicebutton");
                        break;
                    case WorldStyle.Utopia:
                        btn.GetComponent<Image>().sprite =
                            Resources.Load<Sprite>(pathUtopiaSprites + "gui/utopia_choicebutton");
                        break;
                }

                if (ScriptManager.Instance.LoadLineDataPresent(id + 1).DialogueLineType != EnumDialogueLineType.ChoiceLine) break;
            }
        }
        
        private void OnChoiceClick(int toId)
        {
            Debug.Log("Clicked");
            ScriptManager.Instance.SetLineDataPresent(toId);
            DialogueManager.Instance.ProcessLine();
            for (int i = 0; i < gridButton.childCount; i++) Destroy(gridButton.GetChild(i).gameObject);
            DialogueManager.Instance.ProcessLine();
        }
                
        #endregion

        #region 生成mind选项

        [SerializeField] private Transform gridMindButton;
        [SerializeField] private GameObject buttonMindChoicePrefab;
        public void GenerateMindChoices()
        {
            for (int id = ScriptManager.PresentLineID + 1;; id++)
            {
                var btn = Instantiate(buttonMindChoicePrefab, gridMindButton);
                switch (GlobalDataManager.Instance.PresentWorldStyle)
                {
                    case WorldStyle.Modern:
                        btn.GetComponent<Image>().sprite =
                            Resources.Load<Sprite>(pathModernSprites + "gui/modern_choicebutton");
                        break;
                    case WorldStyle.RPG:
                        btn.GetComponent<Image>().sprite =
                            Resources.Load<Sprite>(pathRPGSprites + "gui/rpg_choicebutton");
                        break;
                    case WorldStyle.Utopia:
                        btn.GetComponent<Image>().sprite =
                            Resources.Load<Sprite>(pathUtopiaSprites + "gui/utopia_choicebutton");
                        break;
                }
                btn.GetComponentInChildren<TMP_Text>().text = ScriptManager.Instance.LoadLineDataPresent(id).content;
                btn.GetComponent<Button>().onClick.AddListener(
                    delegate
                    {
                        OnMindChoiceClick(ScriptManager.Instance.LoadLineDataPresent(id).toLine);
                    });
                if (ScriptManager.Instance.LoadLineDataPresent(id + 1).DialogueLineType != EnumDialogueLineType.ChoiceLine) break;
            }
        }
        
        private void OnMindChoiceClick(int toId)
        {
            Debug.Log("Clicked");
            ScriptManager.Instance.SetLineDataPresent(toId);
            for (int i = 0; i < gridMindButton.childCount; i++) Destroy(gridMindButton.GetChild(i).gameObject);
            DialogueManager.Instance.ProcessLine();
        }

        #endregion
        
        #region 大小CG显隐

        [SerializeField] private GameObject fullCG;
        [SerializeField] private GameObject smallCG;

        public void FullCGOn(Sprite img)
        {
            fullCG.GetComponent<CanvasGroup>().alpha = 1;
            fullCG.GetComponent<Image>().sprite = img;
        }

        public void FullCGOff()
        {
            fullCG.GetComponent<Image>().sprite = null;
            fullCG.GetComponent<CanvasGroup>().alpha = 0;
        }
        public void PartCGOn(Sprite img)
        {
            smallCG.GetComponent<CanvasGroup>().alpha = 1;
            smallCG.GetComponent<Image>().sprite = img;
            smallCG.GetComponent<Image>().SetNativeSize();
        }

        public void PartCGOff()
        {
            smallCG.GetComponent<Image>().sprite = null;
            smallCG.GetComponent<CanvasGroup>().alpha = 0;
        }

        #endregion
    }
}
