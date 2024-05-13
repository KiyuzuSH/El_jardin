using System.Collections.Generic;
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
        ControlLine,
        GameLine,
        CollectionLine,
    }
    
    /// <summary> 一句对白，any kind ok </summary>
    public class DialogueLine : ScriptableObject
    {
        public int lineId;
        /// <summary> 本行类型 </summary>
        public EnumDialogueLineType DialogueLineType;
        /// <summary> 说话人名字 </summary>
        public string personName;
        [TextArea]
        public string content;
        /// <summary> 触发事件列表 </summary>
        public List<DialogueEventModel> events;
        /// <summary> 选项是否在想法框 </summary>
        public bool choiceAtMindBox;
    }
}