using KiyuzuDev.ITGWDO.StoryData;
using UnityEngine;

namespace KiyuzuDev.ITGWDO.AVG
{
    public class DialogueManager : MonoBehaviour
    {
        #region Singleton

        public static DialogueManager Instance { get; private set; }
        
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
        
        public int CurrentStory { get; private set; }
        public int CurrentLine { get; private set; }
        
        private void Start()
        {
            // can delete
            CurrentStory = 99;
            // also can delete
            CurrentLine = 99001;
            ProcessLine(CurrentLine);
        }
        
        /// TODO: int CurrentStory & int CurrentLine
        /// Can be defined by SaveData Loading
        /// Write Method when SaveLoad written

        public static DialogueLine PresentLine { get; private set; }
        
        public void ProcessLine(int lineId)
        {
            PresentLine = ScriptManager.Instance.LoadSpecificLine(lineId);
            switch (PresentLine.DialogueLineType)
            {
                case EnumDialogueLineType.TitleLine:
                    // UpdateTitle
                    break;
                case EnumDialogueLineType.NarrationLine:
                    // UpdateText
                    break;
                case EnumDialogueLineType.MindLine:
                    // UpdateMind
                    break;
                case EnumDialogueLineType.ChooseLine:
                    // ProcessChoice
                    break;
                case EnumDialogueLineType.ControlLine:
                    // First, take after command and process
                    // Then proceed next line
                    break;// return?
                case EnumDialogueLineType.GameLine:
                    // Save Data
                    // Turn to Game Scene
                    break;
                case EnumDialogueLineType.CollectionLine:
                    // Update Collection
                    break;
            }
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

        private void GenerateMindChoice() => MindChoiceManager.Instance.GenerateChoice();

        // private void UpdateManPic(string _type, string _name, string _style, string _pos) =>
            // PictureViewManager.Instance.UpdateManPic(_type, _name, _style, _pos);

        private void ShowLines(string _text) => MindShowManager.Instance.ShowLines(_text);
    }
}
