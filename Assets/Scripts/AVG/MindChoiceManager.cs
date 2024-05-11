using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MindChoiceManager : MonoBehaviour
    {
        public static MindChoiceManager Instance { get; private set; }
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
        }

        private void OnDestroy()
        {
            Destroy(Instance);
        }

        public Transform gridButton;
        public GameObject buttonMindChoice;
        
        public void GenerateChoice()
        {
            if (SMI.GetLine(SMI.CurrentLine)[1] == "^&")
            {
                var btn = Instantiate(buttonMindChoice, gridButton);
                btn.GetComponentInChildren<TMP_Text>().text = SMI.GetLine(SMI.CurrentLine)[4];
                btn.GetComponent<Button>().onClick.AddListener(OnChoiceClick);
                if (SMI.GetLine(SMI.CurrentLine + 1)[1] == "^&")
                {
                    SMI.CurrentLine++;
                    GenerateChoice();
                }
            }
        }
        
        private void OnChoiceClick()
        {
            DialogueManager.Instance.CheckCurrentLine();
            for (int i = 0; i < gridButton.childCount; i++) Destroy(gridButton.GetChild(i).gameObject);
        }
    }
}