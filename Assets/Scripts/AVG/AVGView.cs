using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace KiyuzuDev.ITGWDO.AVG
{
    public class AVGView : MonoBehaviour
    {
        #region Singleton

        public static AVGView Instance { get; private set; }

        private void OnDestroy()
        {
            Destroy(Instance);
        }

        #endregion

        private VisualElement rootVE;

        private Image outsidePic;
        private Image interiorPic;
        private Image jalousiePic;

        private Image fullCG;
        private Image smallCG;

        private Label nameLabel;
        private Label contentLabel;
        private Image continuePic;
        private Button continueButton;

        private VisualElement mindContainer;
        private Label mindContentLabel;
        private Button saveInButton;
        private VisualElement mindChoiceContainer;

        private VisualElement announcerContainer;
        private Label titleLabel;
        private Label dayLabel;

        private VisualElement choiceContainer;


        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this)
            {
                Destroy(gameObject);
                Instance = this;
            }

            rootVE = GetComponent<UIDocument>().rootVisualElement;

            outsidePic = rootVE.Q<Image>("OutsidePic");
            interiorPic = rootVE.Q<Image>("InteriorPic");
            jalousiePic = rootVE.Q<Image>("JalousiePic");

            fullCG = rootVE.Q<Image>("FullCGPic");
            smallCG = rootVE.Q<Image>("SmallCGPic");

            nameLabel = rootVE.Q<Label>("NameLabel");
            contentLabel = rootVE.Q<Label>("ContentLabel");
            continuePic = rootVE.Q<Image>("PlzContinuePic");
            continueButton = rootVE.Q<Button>("DialogueContinueButton");
            continueButton.RegisterCallback<MouseDownEvent>(OnContinue);

            mindContainer = rootVE.Q<VisualElement>("MindContainer");
            mindContentLabel = rootVE.Q<Label>("MindContentLabel");
            saveInButton = rootVE.Q<Button>("SaveInButton");
            saveInButton.RegisterCallback<MouseDownEvent>(OnSavingBox);
            mindChoiceContainer = rootVE.Q<VisualElement>("MindChoiceContainer");

            announcerContainer = rootVE.Q<VisualElement>("AnnouncerContainer");
            announcerContainer.focusable = false;
            announcerContainer.style.opacity = 0;
            titleLabel = rootVE.Q<Label>("titleLabel");
            dayLabel = rootVE.Q<Label>("DayLabel");

            choiceContainer = rootVE.Q<VisualElement>("ChoiceContainer");
        }

        public void ChangeToStyleView(WorldStyle _style)
        {
            switch (_style)
            {
                case WorldStyle.Modern:
                    interiorPic.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_in");
                    outsidePic.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_out");
                    if(GlobalDataManager.Instance.JalousieShutDown) 
                        jalousiePic.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_jalousie_shutten");
                    else
                        jalousiePic.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_jalousie_fullopen");
                    // shelf.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_shelf");
                    // wineListImg.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_winelist");
                    // wineListBtn.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_wineui");
                    break;
                case WorldStyle.RPG:
                    interiorPic.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_in");
                    outsidePic.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_out");
                    if(GlobalDataManager.Instance.JalousieShutDown) 
                        jalousiePic.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_jalousie_shutten");
                    else
                        jalousiePic.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_jalousie_fullopen");
                    // shelf.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_shelf");
                    // wineListImg.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_winelist");
                    // wineListBtn.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_wineui");
                    break;
                case WorldStyle.Utopia:
                    interiorPic.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_in");
                    outsidePic.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_out");
                    if(GlobalDataManager.Instance.JalousieShutDown) 
                        jalousiePic.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_jalousie_shutten");
                    else
                        jalousiePic.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_jalousie_fullopen");
                    // shelf.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_shelf");
                    // wineListImg.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_winelist");
                    // wineListBtn.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_wineui");
                    break;
            }
        }

        public void FullCGOn(Sprite img) => fullCG.sprite = img;
        public void FullCGOff() => fullCG.sprite = null;
        public void SmallCGOn(Sprite img) => smallCG.sprite = img;
        public void SmallCGOff() => smallCG.sprite = null;

        private string contentPassed;

        public void UpdateText(string personName, string content)
        {
            contentPassed = content.Replace("\\n", "\n");
            nameLabel.text = personName != "" ? personName : "";
            StartCoroutine(TextJump(contentPassed));
        }

        private IEnumerator TextJump(string _text = "")
        {
            contentLabel.text = "";
            DialogueTextNotJumping = false;
            foreach (var c in _text)
                if (DialogueTextNotJumping == false)
                {
                    contentLabel.text += c;
                    yield return new WaitForSeconds(DialogueTextJumpTime);
                }
            DialogueTextNotJumping = true;
        }

        private void TextStopJumping()
        {
            if (DialogueTextNotJumping) return;
            StopCoroutine(TextJump());
            DialogueTextNotJumping = true;
            contentLabel.text = contentPassed;
        }

        // TODO: continue Pic event

        private string mindPassed;

        public void UpdateMind(string mind)
        {
            mindContainer.style.opacity = 1;
            mindContainer.focusable = true;
            mindPassed = mind.Replace("\\n", "\n");
            StartCoroutine(MindTextJump(mindPassed));
        }

        private IEnumerator MindTextJump(string _text = "")
        {
            mindContentLabel.text = "";
            DialogueTextNotJumping = false;
            foreach (var c in _text)
                if (DialogueTextNotJumping == false)
                {
                    mindContentLabel.text += c;
                    yield return new WaitForSeconds(DialogueTextJumpTime);
                }
            

            DialogueTextNotJumping = true;
        }

        private void MindStopJumping()
        {
            if (MindTextNotJumping) return;
            StopCoroutine(MindTextJump());
            MindTextNotJumping = true;
            mindContentLabel.text = mindPassed;
        }

        void OnContinue(MouseDownEvent mouseDownEvent)
        {
            if (mouseDownEvent.button == 0)
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
        }

        void OnSavingBox(MouseDownEvent mouseDownEvent)
        {
            if (mouseDownEvent.button == 0)
            {
                mindContainer.style.opacity = 0;
                mindContainer.focusable = false;
            }
        }

        public void UpdateAnnouncementTitle(string content)
        {
            string[] array = content.Split('|');
            titleLabel.text = array[0];
            dayLabel.text = array[1];
            announcerContainer.style.opacity = 1;
            Invoke(nameof(Invisible), 3);
        }

        void Invisible()
        {
            announcerContainer.style.opacity = 0;
        }

        public void GenerateChoices(bool choiceAtMindBox, string content)
        {
            // TODO: choice
            
            // TODO: mind choice
        }
        
        public static bool DialogueTextNotJumping { get; set; }
        public static bool MindTextNotJumping { get; set; }

        [Range(0, 1)] public static float DialogueTextJumpTime = 0.075f;
        [Range(0, 1)] public static float MindTextJumpTime = 0.005f;
        
        private void Start()
        {
            DialogueTextNotJumping = true;
            MindTextNotJumping = true;
            if (DialogueTextJumpTime < 0f)
                DialogueTextJumpTime = 0.1f;
            if (MindTextJumpTime < 0f)
                MindTextJumpTime = 0.1f;
        }
        
    }
}
