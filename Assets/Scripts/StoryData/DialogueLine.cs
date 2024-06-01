using System;
using UnityEngine;

namespace KiyuzuDev.ITGWDO.StoryData
{
    /// <summary> 对话线触发事件的类型 </summary>
    public enum EnumDialogueEventType
    {
        None = 0,
        Wait, // NOT USED NOW 0531 args[0]=secondInt
        Style, // args[0]=WorldStyle.XXX
        CGLoad, // args[0]=Full/Part, args[1]=Resources/..
        CGUnLoad, // args[0]=Full/Part/All
        BlackOn, // args[0]=durationSecondInt
        BlackOff, // args[0]=durationSecondInt
        HumanLoad, // args[0]=xPos, args[1]=Resources/..
        HumanPosChange, // NOT USED NOW 0531 args[0]=ID, args[1]=newXPos
        HumanChange, // args[0]=ID, args[1]=Resources/..
        HumanUnload, // args[0]=ID
        HumanAllClear, // NO args needed
    }

    /// <summary> 事件触发的模型 </summary>
    [System.Serializable]
    public class DialogueEvent
    {
        public EnumDialogueEventType eventType = EnumDialogueEventType.None;
        public string[] args;
    }
    
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
    
    [Serializable]
    public class DialogueLine
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