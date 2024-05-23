using System.Collections.Generic;
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
        
        private void Start()
        {

        }

        public static DialogueLine PresentLine { get; private set; }
        
        public void ProcessLine(int lineId)
        {
            PresentLine = ScriptManager.Instance.LoadSpecificLine(lineId);
            switch (PresentLine.DialogueLineType)
            {
                case EnumDialogueLineType.TitleLine:
                    AVGView.Instance.UpdateAnnouncementTitle(PresentLine.content);
                    ToChoiceOnlyDealer(PresentLine.events);
                    break;
                case EnumDialogueLineType.NarrationLine:
                    AVGView.Instance.UpdateText(PresentLine.personName,PresentLine.content);
                    ToChoiceOnlyDealer(PresentLine.events);
                    break;
                case EnumDialogueLineType.MindLine:
                    AVGView.Instance.UpdateMind(PresentLine.content);
                    ToChoiceOnlyDealer(PresentLine.events);
                    break;
                case EnumDialogueLineType.ChooseLine:
                    AVGView.Instance.GenerateChoices(PresentLine.choiceAtMindBox, PresentLine.content);
                    ToChoiceOnlyDealer(PresentLine.events);
                    break;
                case EnumDialogueLineType.ControlLine:
                    
                    // TODO: First, take after command and process
                    
                    // TODO: Then proceed next line
                    
                    break;// return?
                
                case EnumDialogueLineType.GameLine:
                    
                    // TODO: Save Data
                    
                    // TODO: Turn to Game Scene
                    
                    break;
                case EnumDialogueLineType.CollectionLine:
                    
                    // TODO: Update Collection
                    
                    break;
            }
        }

        public void ToChoiceOnlyDealer(List<DialogueEventModel> events)
        {
            
        }
    }
}
