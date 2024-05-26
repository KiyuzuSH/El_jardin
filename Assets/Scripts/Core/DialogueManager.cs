using KiyuzuDev.ITGWDO.StoryData;
using KiyuzuDev.ITGWDO.View;
using UnityEngine;

namespace KiyuzuDev.ITGWDO.Core
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
        
        private static DialogueLine PresentLine { get; set; }
        public static int PresentLineID { get; set; }

        private void OnEnable() { }
        private void OnDisable() { }

        private void Start()
        {
            LoadLineById(110001);
            ProcessLine();
        }

        void LoadLineById(int id)
        {
            PresentLine = ScriptManager.Instance.LoadSpecificLine(id);
        }
        
        private void ProcessLine()
        {
            PresentLineID = PresentLine.lineId;
            // Deal Event Fore
            // Deal Event Main, maybe not here, in the switches
            switch (PresentLine.DialogueLineType)
            {
                case EnumDialogueLineType.TitleLine:
                    AVGView.Instance.UpdateAnnouncementTitle(PresentLine.content);
                    break;
                case EnumDialogueLineType.NarrationLine:
                    AVGView.Instance.UpdateText(PresentLine.personName,PresentLine.content);
                    break;
                case EnumDialogueLineType.MindLine:
                    AVGView.Instance.UpdateMind(PresentLine.content);
                    break;
                case EnumDialogueLineType.ChooseLine:
                    // 选择choices生成
                    return;
                case EnumDialogueLineType.MindChooseLine:
                    // 选择choices生成
                    return;
                case EnumDialogueLineType.ChoiceLine:
                    // choices，在这里为choices生成实例？
                    break;// needed?
                case EnumDialogueLineType.ControlLine:
                    Debug.Log(PresentLine.personName + "\n" + PresentLine.content);
                    // Process event Main
                    // Process event After
                    // Process toLine
                    return;
                case EnumDialogueLineType.GameLine:
                    // TODO: Turn to Game Scene
                    break;
            }
            // wait for input
            // Deal eventAfter
            LoadLineById(PresentLine.toLine);
        }

        private void ProcessEventFore()
        {
            switch (PresentLine.eventFore.eventType)
            {
                case EnumDialogueEventType.None:
                    return;
                case EnumDialogueEventType.To:
                    
                    break;
                case EnumDialogueEventType.Wait:

                    break;
                case EnumDialogueEventType.Style:

                    break;
                case EnumDialogueEventType.CGLoad:

                    break;
                case EnumDialogueEventType.CGUnLoad:

                    break;
                case EnumDialogueEventType.BlackOn:

                    break;
                case EnumDialogueEventType.BlackOff:

                    break;
            }
            
        }
    }
}
