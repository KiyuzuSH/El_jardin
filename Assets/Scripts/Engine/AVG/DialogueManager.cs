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

        private GameObject dialoguePanel;
        
        /// <summary> 对话表 </summary>
        private List<string[]> dialogueSheet;
        /// <summary> 当前行index </summary>
        public int currentLine;
        
        [Header("UI组件")]
        // TODO: 立绘
        public Image tachie;
        
        [Tooltip("下一条的按钮")] public Button buttonContinue;

        [Tooltip("选项按钮组")] public Transform gridButton;
        [Tooltip("选项按钮预制件")] public GameObject buttonChoice;

        [SerializeField] private Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();
        
        private bool textJumpFinished;
        public void SetJumpState(bool _state) => textJumpFinished = _state;
        
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
            textJumpFinished = true;
            dialogueSheet = ScriptManager.Instance.GetDialogueSheet();//TODO: Choose time to update
            currentLine = 0;//TODO: who takes control?
            dialoguePanel = transform.GetChild(0).gameObject;
            dialoguePanel.SetActive(true);
            buttonContinue.GetComponent<Button>().onClick.AddListener(OnContinueDialogue);
            OnContinueDialogue();
        }
        
        private void OnDestroy()
        {
            buttonContinue.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(Instance);
        }
        
        private void CheckCurrentLine()
        {
            switch (dialogueSheet[currentLine][1])
            {
                case "&":
                    buttonContinue.gameObject.SetActive(false); 
                    dialoguePanel.SetActive(false);
                    GenerateChoice();
                    break;
                case "!":
                    dialoguePanel.SetActive(false);
                    ShowTitle(dialogueSheet[currentLine][6]);
                    var next = int.Parse(dialogueSheet[currentLine][2]);
                    currentLine = next;
                    CheckCurrentLine();
                    break;
                case "":
                    dialoguePanel.SetActive(true);
                    UpdateText(dialogueSheet[currentLine][3],dialogueSheet[currentLine][6]);
                    // TODO: 立绘
                    // UpdateImage(dialogueSheet[currentLine][3],dialogueSheet[currentLine][4]);
                    buttonContinue.gameObject.SetActive(true);
                    break;
            }
        }
        
        private void OnContinueDialogue()
        {
            if (dialoguePanel.activeInHierarchy)
            {
                if (textJumpFinished)
                {
                    var next = int.Parse(dialogueSheet[currentLine][2]);
                    currentLine = next;
                    if (currentLine == dialogueSheet.Count || dialogueSheet[currentLine][0] == "")
                        dialoguePanel.SetActive(false);
                    CheckCurrentLine();
                }
            }
        }
        
        private void UpdateText(string _name,string _text) => ShowDialogueManager.Instance.UpdateText(_name, _text);

        private void ShowTitle(string _text) => ShowSideTitleManager.Instance.ShowTitle(_text);

        private void GenerateChoice()
        {
            if (dialogueSheet[currentLine][1] == "&")
            {
                var btn = Instantiate(buttonChoice, gridButton);
                btn.GetComponentInChildren<TMP_Text>().text = dialogueSheet[currentLine][6];
                var id = currentLine;
                btn.GetComponent<Button>().onClick.AddListener
                (
                    delegate { OnChoiceClick(id); }
                );
                if (dialogueSheet[currentLine + 1][1] == "&")
                {
                    currentLine++;
                    GenerateChoice();
                }
            }
        }
        
        private void OnChoiceClick(int _id)
        {
            currentLine = int.Parse(dialogueSheet[_id][2]);
            CheckCurrentLine();
            for (var i = 0; i < gridButton.childCount; i++) Destroy(gridButton.GetChild(i).gameObject);
        }
    }
}
