using UnityEngine;

namespace KiyuzuDev.ITGWDO.StoryData
{
    /// <summary> 这一句是什么类型 </summary>
    public enum EnumDialogueLineType
    {
        TitleLine,
        NarrationLine,
        MindLine,
        ChooseLine,
        MindChooseLine,
        ChoiceLine,
        ControlLine,
        GameLine,
    }
    
    public class DialogueLine : ScriptableObject
    {
        public int lineId;
        /// <summary> 本行类型 </summary>
        public EnumDialogueLineType DialogueLineType;
        /// <summary> 说话人名字 </summary>
        public string personName;
        [TextArea]
        public string content;
        public DialogueEvent eventFore;
        public DialogueEvent eventMain;
        public DialogueEvent eventAfter;
        public int toLine;
    }

    public enum EventPlace
    {
        Fore,
        Main,
        After,
    }
}