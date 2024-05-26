using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            _textTypewriter = null;
            _mindTypewriter = null;
            if (TextTimePerChar < 0f) TextTimePerChar = 0.1f;
            if (MindTimePerChar < 0f) MindTimePerChar = 0.1f;
            // ChangeToStyleView(GlobalDataManager.Instance.PresentWorldStyle);
        }

        #region 风格改变
        
        [SerializeField] private Image outsidePic;
        [SerializeField] private Image interiorPic;
        [SerializeField] private GameObject jalousiePic;
        
        public void ChangeToStyleView(WorldStyle _style)
        {
            switch (_style)
            {
                case WorldStyle.Modern:
                    interiorPic.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_in");
                    outsidePic.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_out");
                    if(GlobalDataManager.Instance.JalousieShutDown) 
                        jalousiePic.GetComponent<Image>().sprite 
                            = Resources.Load<Sprite>("Sprites/Theme/modern/modern_jalousie_shutten");
                    else
                        jalousiePic.GetComponent<Image>().sprite 
                            = Resources.Load<Sprite>("Sprites/Theme/modern/modern_jalousie_fullopen");
                    // shelf.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_shelf");
                    // wineListImg.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_winelist");
                    // wineListBtn.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_wineui");
                    break;
                case WorldStyle.RPG:
                    interiorPic.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_in");
                    outsidePic.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_out");
                    if(GlobalDataManager.Instance.JalousieShutDown) 
                        jalousiePic.GetComponent<Image>().sprite 
                            = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_jalousie_shutten");
                    else
                        jalousiePic.GetComponent<Image>().sprite 
                            = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_jalousie_fullopen");
                    // shelf.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_shelf");
                    // wineListImg.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_winelist");
                    // wineListBtn.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_wineui");
                    break;
                case WorldStyle.Utopia:
                    interiorPic.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_in");
                    outsidePic.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_out");
                    if(GlobalDataManager.Instance.JalousieShutDown) 
                        jalousiePic.GetComponent<Image>().sprite 
                            = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_jalousie_shutten");
                    else
                        jalousiePic.GetComponent<Image>().sprite 
                            = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_jalousie_fullopen");
                    // shelf.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_shelf");
                    // wineListImg.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_winelist");
                    // wineListBtn.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_wineui");
                    break;
            }
        }
        
        #endregion

        #region 大小CG显隐

        [SerializeField] private GameObject fullCG;
        [SerializeField] private GameObject smallCG;

        public void FullCGOn(Sprite img)
        {
            fullCG.GetComponent<Image>().sprite = img;
            fullCG.GetComponent<CanvasGroup>().alpha = 1;
        }

        public void FullCGOff()
        {
            fullCG.GetComponent<Image>().sprite = null;
            fullCG.GetComponent<CanvasGroup>().alpha = 0;
        }
        public void SmallCGOn(Sprite img)
        {
            var tmp = smallCG.GetComponent<Image>();
            tmp.sprite = img;
            tmp.SetNativeSize();
            smallCG.GetComponent<CanvasGroup>().alpha = 1;
        }

        public void SmallCGOff()
        {
            smallCG.GetComponent<Image>().sprite = null;
            smallCG.GetComponent<CanvasGroup>().alpha = 0;
        }

        #endregion

        #region 主文本显示

        [Range(0, 1)] private static float TextTimePerChar = 0.01f;

        [SerializeField] private GameObject dialogueContainer;
        [SerializeField] private GameObject nameBox;
        [SerializeField] private TMP_Text textName;
        [SerializeField] private TMP_Text textDialogue;
        // [SerializeField] private Image continuePic;
        
        private static readonly string[] _uguiSymbols = { "b", "i" };
        private static readonly string[] _uguiCloseSymbols = { "b", "i", "size", "color" };
        
        private string contentPassed;
        private Coroutine _textTypewriter;

        public void UpdateText(string personName, string content)
        {
            contentPassed = content.Replace("\\n", "\n").Replace("\\t","\t");
            if (personName == "") UnshowNameBox();
            else ShowNameBox(personName);
            _textTypewriter = StartCoroutine(TextTypewriter(contentPassed));
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
            _textTypewriter = null;
            // Can call a Complete back
        }

        private void SkipTextTypewriter()
        {
            if (_textTypewriter == null) return;
            StopCoroutine(TextTypewriter());
            _textTypewriter = null;
            textDialogue.text = contentPassed;
        }
        
        private void ShowNameBox(string _name)
        {
            nameBox.SetActive(true);
            textName.text = _name;
        }

        private void UnshowNameBox()
        {
            textName.text = "";
            nameBox.SetActive(false);
        }

        // continue Pic event

        #endregion

        #region 脑海想法显示

        [Range(0, 1)] private static float MindTimePerChar = 0.005f;
        
        [SerializeField] private GameObject mindContainer;
        [SerializeField] private TMP_Text textMind;
        
        private string mindPassed;
        private Coroutine _mindTypewriter;

        public void UpdateMind(string mind)
        {
            mindPassed = mind.Replace("\\n", "\n").Replace("\\t","\t");
            _mindTypewriter = StartCoroutine(MindTypewriter(mindPassed));
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
            _mindTypewriter = null;
            // Can call a Complete back
        }

        private void SkipMindTypewriter()
        {
            if (_mindTypewriter == null) return;
            StopCoroutine(MindTypewriter());
            _mindTypewriter = null;
            textMind.text = mindPassed;
        }

        #endregion
        
        [SerializeField] private GameObject announcerContainer;
        [SerializeField] private TMP_Text titleLabel;
        [SerializeField] private TMP_Text dayLabel;
        
        public void UpdateAnnouncementTitle(string content)
        {
            string[] array = content.Split('|');
            titleLabel.text = array[1];
            dayLabel.text = array[0];
            if(!announcerContainer.activeSelf) announcerContainer.SetActive(true);
            Invoke(nameof(Invisible), 3);
        }

        void Invisible()
        {
            announcerContainer.SetActive(false);
        }

        [SerializeField] private Transform gridButton;
        [SerializeField] private GameObject buttonChoicePrefab;
        public void GenerateChoices()
        {
            // TODO: choice
            
            // if (SMI.GetLine(SMI.CurrentLine)[1] == "^&")
            // {
            //     var btn = Instantiate(buttonMindChoice, gridButton);
            //     btn.GetComponentInChildren<TMP_Text>().text = SMI.GetLine(SMI.CurrentLine)[4];
            //     btn.GetComponent<Button>().onClick.AddListener(OnChoiceClick);
            //     if (SMI.GetLine(SMI.CurrentLine + 1)[1] == "^&")
            //     {
            //         SMI.CurrentLine++;
            //         GenerateChoice();
            //     }
            // }
        }
        private void OnChoiceClick()
        {
            // DialogueManager.Instance.CheckCurrentLine();
            // for (int i = 0; i < gridButton.childCount; i++) Destroy(gridButton.GetChild(i).gameObject);
        }
        
        [SerializeField] private Transform gridMindButton;
        [SerializeField] private GameObject buttonMindChoicePrefab;
        public void GenerateMindChoices()
        {
            // TODO: mind choice
        }
        private void OnMindChoiceClick()
        {
            
        }

        // Continue button event, move to other place
        void OnContinue()
        {
            if (_textTypewriter != null)
            {
                SkipTextTypewriter();
                return;
            }

            if (_mindTypewriter != null)
            {
                SkipMindTypewriter();
                return;
            }

            // TODO: message of get continue
        }

        // mind box moving event, may move other place
        void OnSavingBox()
        {

        }
    }
}
