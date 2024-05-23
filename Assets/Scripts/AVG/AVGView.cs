using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KiyuzuDev.ITGWDO.AVG
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
        
        private static bool DialogueTextNotJumping { get; set; }
        private static bool MindTextNotJumping { get; set; }

        [Range(0, 1)] private static float DialogueTextJumpTime = 0.075f;
        [Range(0, 1)] private static float MindTextJumpTime = 0.005f;
        
        private void OnEnable()
        {
            DialogueTextNotJumping = true;
            MindTextNotJumping = true;
            if (DialogueTextJumpTime < 0f)
                DialogueTextJumpTime = 0.1f;
            if (MindTextJumpTime < 0f)
                MindTextJumpTime = 0.1f;
            ChangeToStyleView(GlobalDataManager.Instance.PresentWorldStyle);
        }

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
                        jalousiePic.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_jalousie_shutten");
                    else
                        jalousiePic.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_jalousie_fullopen");
                    // shelf.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_shelf");
                    // wineListImg.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_winelist");
                    // wineListBtn.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_wineui");
                    break;
                case WorldStyle.RPG:
                    interiorPic.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_in");
                    outsidePic.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_out");
                    if(GlobalDataManager.Instance.JalousieShutDown) 
                        jalousiePic.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_jalousie_shutten");
                    else
                        jalousiePic.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_jalousie_fullopen");
                    // shelf.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_shelf");
                    // wineListImg.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_winelist");
                    // wineListBtn.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_wineui");
                    break;
                case WorldStyle.Utopia:
                    interiorPic.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_in");
                    outsidePic.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_out");
                    if(GlobalDataManager.Instance.JalousieShutDown) 
                        jalousiePic.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_jalousie_shutten");
                    else
                        jalousiePic.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_jalousie_fullopen");
                    // shelf.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_shelf");
                    // wineListImg.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_winelist");
                    // wineListBtn.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_wineui");
                    break;
            }
        }
        
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

        [SerializeField] private GameObject dialogueContainer;
        [SerializeField] private GameObject nameBox;
        [SerializeField] private TMP_Text textName;
        [SerializeField] private TMP_Text textDialogue;
        // [SerializeField] private Image continuePic;
        
        private string contentPassed;

        public void UpdateText(string personName, string content)
        {
            contentPassed = content.Replace("\\n", "\n");
            if (personName == "") UnshowNameBox();
            else ShowNameBox(personName);
            StartCoroutine(TextJump(contentPassed));
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

        private IEnumerator TextJump(string _text = "")
        {
            // textDialogue.text = "";
            DialogueTextNotJumping = false;
            foreach (var c in _text)
                if (DialogueTextNotJumping == false)
                {
                    textDialogue.text += c;
                    yield return new WaitForSeconds(DialogueTextJumpTime);
                }
            DialogueTextNotJumping = true;
        }

        private void TextStopJumping()
        {
            if (DialogueTextNotJumping) return;
            StopCoroutine(TextJump());
            DialogueTextNotJumping = true;
            textDialogue.text = contentPassed;
        }

        // continue Pic event
        
        [SerializeField] private GameObject mindContainer;
        [SerializeField] private TMP_Text textMind;
        [SerializeField] private Button saveRightButton;
        
        private string mindPassed;

        public void UpdateMind(string mind)
        {
            mindPassed = mind.Replace("\\n", "\n");
            StartCoroutine(MindTextJump(mindPassed));
        }

        private IEnumerator MindTextJump(string _text = "")
        {
            // textMind.text = "";
            DialogueTextNotJumping = false;
            foreach (var c in _text)
                if (DialogueTextNotJumping == false)
                {
                    textMind.text += c;
                    yield return new WaitForSeconds(DialogueTextJumpTime);
                }
            DialogueTextNotJumping = true;
        }

        private void MindStopJumping()
        {
            if (MindTextNotJumping) return;
            StopCoroutine(MindTextJump());
            MindTextNotJumping = true;
            textMind.text = mindPassed;
        }
        
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
        public void GenerateChoices(bool choiceAtMindBox, string content)
        {
            // TODO: choice
            
            // TODO: mind choice
        }
        private void OnChoiceClick()
        {
            
        }
        
        [SerializeField] private Transform gridMindButton;
        [SerializeField] private GameObject buttonMindChoicePrefab;
        public void GenerateMindChoices()
        {
            
        }
        private void OnMindChoiceClick()
        {
            
        }

        // Continue button event, move to other place
        void OnContinue()
        {
            if (!DialogueTextNotJumping)
            {
                TextStopJumping();
                return;
            }

            if (!MindTextNotJumping)
            {
                MindStopJumping();
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
