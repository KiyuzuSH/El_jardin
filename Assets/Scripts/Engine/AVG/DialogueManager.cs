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
        
        public Button buttonDialogueContinue;
        public Button buttonMindContinue;
        
        private void Start()
        {
            SMI = ScriptManager.Instance;
            gameObject.SetActive(true);
            buttonDialogueContinue.onClick.AddListener(OnContinueDialogue);
            buttonMindContinue.onClick.AddListener(OnContinueDialogue);
            OnContinueDialogue();
        }
        
        private void OnDestroy()
        {
            buttonDialogueContinue.onClick.RemoveAllListeners();
            buttonMindContinue.onClick.RemoveAllListeners();
            Destroy(Instance);
        }

        public void CheckCurrentLine()
        {
            switch (SMI.GetLine(SMI.CurrentLine)[1])
            {
                case "END":
                    // gameObject.SetActive(false);
                    // buttonDialogueContinue.gameObject.SetActive(false);
                    // Debug.Log("All Content Done");
                    // return;//TODO: Can turn to other place
                case "&":
                    GetComponent<CanvasGroup>().alpha = 0;
                    buttonDialogueContinue.gameObject.SetActive(false);
                    buttonMindContinue.gameObject.SetActive(false);
                    GenerateChoice();
                    break;
                case "!":
                    GetComponent<CanvasGroup>().alpha = 0;
                    buttonDialogueContinue.gameObject.SetActive(true);
                    buttonMindContinue.gameObject.SetActive(true);
                    ShowTitle(SMI.GetLine(SMI.CurrentLine)[4]);
                    SMI.CurrentLine++;
                    CheckCurrentLine();
                    break;
                case "^":
                    GetComponent<CanvasGroup>().alpha = 0;
                    buttonDialogueContinue.gameObject.SetActive(true);
                    buttonMindContinue.gameObject.SetActive(true);
                    ShowLines(SMI.GetLine(SMI.CurrentLine)[4]);
                    if (SMI.GetLine(SMI.CurrentLine + 1)[1] == "")
                    {
                        SMI.CurrentLine++;
                        CheckCurrentLine();
                    }
                    break;
                case "":
                    GetComponent<CanvasGroup>().alpha = 1;
                    UpdateText(
                        SMI.GetLine(SMI.CurrentLine)[3], 
                        SMI.GetLine(SMI.CurrentLine)[4]
                        );
                    // UpdateManPic(
                    //     SMI.GetCurrentLine(SMI.CurrentLine)[8],
                    //     SMI.GetCurrentLine(SMI.CurrentLine)[9],
                    //     SMI.GetCurrentLine(SMI.CurrentLine)[10],
                    //     SMI.GetCurrentLine(SMI.CurrentLine)[11]
                    //     );
                    buttonDialogueContinue.gameObject.SetActive(true);
                    buttonMindContinue.gameObject.SetActive(true);
                    break;
            }
            SMI.CurrentLine = int.Parse(SMI.GetLine(SMI.CurrentLine)[2]);
        }
        
        private void OnContinueDialogue()
        {
            if (!AVGConsts.DialogueTextNotJumping)
            {
                DialogueViewManager.Instance.StopJumping();
                return;
            }
            if (!AVGConsts.MindTextNotJumping)
            {
                MindShowManager.Instance.StopJumping();
                return;
            }
            CheckCurrentLine();
        }
        
        private void UpdateText(string _name,string _text) => DialogueViewManager.Instance.UpdateText(_name, _text);

        private void ShowTitle(string _text) => SideTitleViewManager.Instance.ShowTitle(_text);

        private void GenerateChoice() => ChoiceButtonManager.Instance.GenerateChoice();

        private void UpdateManPic(string _type, string _name, string _style, string _pos) =>
            PictureViewManager.Instance.UpdateManPic(_type, _name, _style, _pos);

        private void ShowLines(string _text) => MindShowManager.Instance.ShowLines(_text);
    }
}
