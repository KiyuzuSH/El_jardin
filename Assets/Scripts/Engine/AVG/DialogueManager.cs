using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }
        private readonly ScriptManager SMI = ScriptManager.Instance;
        
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
            ShowDialogueManager.Instance.TextJumpFinished = true;
            gameObject.SetActive(true);
            buttonContinue.GetComponent<Button>().onClick.AddListener(OnContinueDialogue);
            OnContinueDialogue();
        }
        
        private void OnDestroy()
        {
            buttonContinue.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(Instance);
        }
        
        public Button buttonContinue;
        
        public void SetJumpState(bool _state) => ShowDialogueManager.Instance.TextJumpFinished = _state;
        
        public void CheckCurrentLine()
        {
            switch (SMI.GetCurrentSheet()[SMI.CurrentLine][1])
            {
                case "&":
                    buttonContinue.gameObject.SetActive(false); 
                    gameObject.SetActive(false);
                    GenerateChoice();
                    break;
                case "!":
                    gameObject.SetActive(false);
                    ShowTitle(SMI.GetCurrentSheet()[SMI.CurrentLine][6]);
                    var next = int.Parse(SMI.GetCurrentSheet()[SMI.CurrentLine][2]);
                    SMI.CurrentLine = next;
                    CheckCurrentLine();
                    break;
                case "":
                    gameObject.SetActive(true);
                    UpdateText(SMI.GetCurrentSheet()[SMI.CurrentLine][3],SMI.GetCurrentSheet()[SMI.CurrentLine][6]);
                    // TODO: 立绘
                    // UpdateImage(dialogueSheet[currentLine][3],dialogueSheet[currentLine][4]);
                    buttonContinue.gameObject.SetActive(true);
                    break;
            }
        }
        
        private void OnContinueDialogue()
        {
            if (gameObject.activeInHierarchy)
            {
                if (ShowDialogueManager.Instance.TextJumpFinished)
                {
                    var next = int.Parse(SMI.GetCurrentSheet()[SMI.CurrentLine][2]);
                    SMI.CurrentLine = next;
                    if (SMI.CurrentLine == SMI.GetCurrentSheet().Count || SMI.GetCurrentSheet()[SMI.CurrentLine][0] == "")
                        gameObject.SetActive(false);
                    CheckCurrentLine();
                }
            }
        }
        
        private void UpdateText(string _name,string _text) => ShowDialogueManager.Instance.UpdateText(_name, _text);

        private void ShowTitle(string _text) => ShowSideTitleManager.Instance.ShowTitle(_text);

        private void GenerateChoice()
        {
            ChoiceButtonManager.Instance.GenerateChoice();
        }
    }
}
