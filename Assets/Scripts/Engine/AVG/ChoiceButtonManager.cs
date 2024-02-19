using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ChoiceButtonManager : MonoBehaviour
    {
        public static ChoiceButtonManager Instance { get; private set; }
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
        public GameObject buttonChoice;
        
        public void GenerateChoice()
        {
            if (SMI.GetCurrentLine(SMI.CurrentLine)[1] == "&")
            {
                var btn = Instantiate(buttonChoice, gridButton);
                var id = SMI.CurrentLine;
                btn.GetComponentInChildren<TMP_Text>().text = SMI.GetCurrentLine(SMI.CurrentLine)[4];
                btn.GetComponent<Button>().onClick.AddListener
                (
                    delegate { OnChoiceClick(id); }
                );
                if (SMI.GetCurrentLine(SMI.CurrentLine + 1)[1] == "&")
                {
                    SMI.CurrentLine++;
                    GenerateChoice();
                }
            }
        }
        
        private void OnChoiceClick(int _id)
        {
            SMI.CurrentLine = int.Parse(SMI.GetCurrentLine(_id)[2]);
            DialogueManager.Instance.CheckCurrentLine();
            for (int i = 0; i < gridButton.childCount; i++) Destroy(gridButton.GetChild(i).gameObject);
        }
    }
}