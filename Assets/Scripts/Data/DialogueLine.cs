using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum EnumDialogueLineType
    {
        TitleLine,
        NarrationLine,
        PersonLine,
        MindLine,
        ChooseLine,
        CGLine,
        ControlLine,
        // GameLine,
        // BoxLine,
    }
    
    /// <summary> 角色立绘在屏幕上的位置 </summary>
    public enum EnumCharacterPos
    {
        Left3,
        Center3,
        Right3,
        Left2,
        Right2,
    }

    /// <summary> 对话线触发事件的类型 </summary>
    public enum EnumDialogueEventType
    {
        End = -1,
        Next = 0,
        Choose = 1,
        Jump = 2,
    }

    public enum EnumSpriteType
    {
        CLEAR,
        StandaloneBGP,
        FullScreenFrontPic,
        SmallPic,
    }
    
    public class DialogueEventModel
    {
        public EnumDialogueEventType eventType;
        public string args;
    }

    [CreateAssetMenu(fileName = "DialogueLine_",menuName = "DialogueLine",order = 1)]
    public class DialogueLine : ScriptableObject
    {
        public int lineId;
        /// <summary> 本行类型 </summary>
        public EnumDialogueLineType DialogueLineType = EnumDialogueLineType.NarrationLine;
        /// <summary> 说话人名字 </summary>
        public string personName = "名字";
        [TextArea]
        public string content = "显示内容";
        /// <summary> 触发事件列表 </summary>
        public List<DialogueEventModel> events = null;
        /// <summary> 人物立绘 </summary>
        public Sprite personImg;
        /// <summary> 人物立绘位置 </summary>
        public EnumCharacterPos position = EnumCharacterPos.Center3;
        /// <summary> 选项是否在想法框 </summary>
        public bool atMindBox;
        /// <summary> 图片类型 </summary>
        public EnumSpriteType spriteType = EnumSpriteType.StandaloneBGP;
        /// <summary> 要显示的图片 </summary>
        public Sprite image;
    }
}