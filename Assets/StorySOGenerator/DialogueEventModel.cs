namespace KiyuzuDev.ITGWDO.StoryData
{
    /// <summary> 对话线触发事件的类型 </summary>
    public enum EnumDialogueEventType
    {
        End = -1,
        To = 0,
        Choose = 1,
        CGLoad = 2,
        CGUnLoad = -2,
        BlackOn = 3,
        BlackOff = -3,
    }
    
    /// <summary> 事件触发的模型 </summary>
    [System.Serializable]
    public class DialogueEventModel
    {
        public EnumDialogueEventType eventType = EnumDialogueEventType.To;
        public string[] args;
    }
}