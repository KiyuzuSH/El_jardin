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
                case "END":
                    gameObject.SetActive(false);
                    buttonContinue.gameObject.SetActive(false);
                    Debug.Log("All Content Done");
                    return;//TODO: Can turn to other place
                case "&":
                    GetComponent<CanvasGroup>().alpha = 0;
                    buttonContinue.gameObject.SetActive(false);
                    GenerateChoice();
                    break;
                case "!":
                    GetComponent<CanvasGroup>().alpha = 0;
                    buttonContinue.gameObject.SetActive(true);
                    ShowTitle(SMI.GetCurrentLine(SMI.CurrentLine)[4]);
                    SMI.CurrentLine++;
                    CheckCurrentLine();
                    break;
                // case "^":
                //     
                //     break;
                case "":
                    GetComponent<CanvasGroup>().alpha = 1;
                    UpdateText(
                        SMI.GetCurrentLine(SMI.CurrentLine)[3], 
                        SMI.GetCurrentLine(SMI.CurrentLine)[4]
                        );
                    // PictureViewManager.Instance.
                    //     UpdateManPic(
                    //         SMI.GetCurrentLine(SMI.CurrentLine)[8],
                    //         SMI.GetCurrentLine(SMI.CurrentLine)[9],
                    //         SMI.GetCurrentLine(SMI.CurrentLine)[10],
                    //         SMI.GetCurrentLine(SMI.CurrentLine)[11]
                    //         );
                    buttonContinue.gameObject.SetActive(true);
                    break;
            }
            SMI.CurrentLine = int.Parse(SMI.GetCurrentLine(SMI.CurrentLine)[2]);
        }
        
        private void OnContinueDialogue()
        {
            if (!DialogueViewManager.Instance.TextJumpFinished)
            {
                DialogueViewManager.Instance.StopJumping();
                return;
            }
            CheckCurrentLine();
        }
        
        private void UpdateText(string _name,string _text) => DialogueViewManager.Instance.UpdateText(_name, _text);

        private void ShowTitle(string _text) => SideTitleViewManager.Instance.ShowTitle(_text);

        private void GenerateChoice() => ChoiceButtonManager.Instance.GenerateChoice();
    }
}
