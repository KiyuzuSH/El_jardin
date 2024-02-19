using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }
        private ScriptManager SMI { get; set; }
        
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this)
            {
                Destroy(gameObject);
                Instance = this;
            }
        }
        
        private void Start()
        {
            SMI = ScriptManager.Instance;
            gameObject.SetActive(true);
            buttonContinue.onClick.AddListener(OnContinueDialogue);
            OnContinueDialogue();
        }
        
        private void OnDestroy()
        {
            buttonContinue.onClick.RemoveListener(OnContinueDialogue);
            Destroy(Instance);
        }
        
        public Button buttonContinue;

        public void CheckCurrentLine()
        {
            switch (SMI.GetCurrentLine(SMI.CurrentLine)[1])
            {
                case "&":
                    buttonContinue.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                    GenerateChoice();
                    break;
                case "!":
                    gameObject.SetActive(false);
                    ShowTitle(SMI.GetCurrentLine(SMI.CurrentLine)[4]);
                    SMI.CurrentLine = int.Parse(SMI.GetCurrentLine(SMI.CurrentLine)[2]);
                    CheckCurrentLine();
                    break;
                case "":
                    gameObject.SetActive(true);
                    UpdateText(
                        SMI.GetCurrentLine(SMI.CurrentLine)[3], 
                        SMI.GetCurrentLine(SMI.CurrentLine)[4]
                        );
                    PictureViewManager.Instance.
                        UpdateManPic(
                            SMI.GetCurrentLine(SMI.CurrentLine)[7],
                            SMI.GetCurrentLine(SMI.CurrentLine)[8]
                            );
                    buttonContinue.gameObject.SetActive(true);
                    break;
            }
        }
        
        private void OnContinueDialogue()
        {
            if (!DialogueViewManager.Instance.TextJumpFinished)
            {
                DialogueViewManager.Instance.StopJumping();
                return;
            }
            if (SMI.GetCurrentLine(SMI.CurrentLine)[1] == "END")
            {
                gameObject.SetActive(false);
            }
            CheckCurrentLine();
            SMI.CurrentLine = int.Parse(SMI.GetCurrentLine(SMI.CurrentLine)[2]);
        }
        
        private void UpdateText(string _name,string _text) => DialogueViewManager.Instance.UpdateText(_name, _text);

        private void ShowTitle(string _text) => SideTitleViewManager.Instance.ShowTitle(_text);

        private void GenerateChoice() => ChoiceButtonManager.Instance.GenerateChoice();
    }
}
