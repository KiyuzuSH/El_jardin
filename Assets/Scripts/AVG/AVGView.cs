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

        private Label mindContentLabel;
        private Button saveInButton;
        private VisualElement mindChoiceContainer;

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

            mindContentLabel = rootVE.Q<Label>("MindContentLabel");
            saveInButton = rootVE.Q<Button>("SaveInButton");
            saveInButton.RegisterCallback<MouseDownEvent>(OnSavingBox);
            mindChoiceContainer = rootVE.Q<VisualElement>("MindChoiceContainer");

            titleLabel = rootVE.Q<Label>("titleLabel");
            dayLabel = rootVE.Q<Label>("DayLabel");

            choiceContainer = rootVE.Q<VisualElement>("ChoiceContainer");
        }

        // TODO: Change out in ja 3 pics by changing style enum

        public void FullCGOn(Texture2D img) => fullCG.style.backgroundImage = img;
        public void FullCGOff() => fullCG.style.backgroundImage = null;
        public void SmallCGOn(Texture2D img) => smallCG.style.backgroundImage = img;
        public void SmallCGOff() => smallCG.style.backgroundImage = null;

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
                // TODO: Save it in! 
            }
        }

        public void UpdateAnnouncementTitle(string content)
        {
            string[] array = content.Split('|');
            titleLabel.text = array[0];
            dayLabel.text = array[1];
            // TODO: If not Exist show me 
            // Invoke(nameof(Inactive), 3);
        }

        public void GenerateChoices(bool choiceAtMindBox, string content)
        {

        }

        // TODO: choice
        
        // TODO: mind choice
        
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
