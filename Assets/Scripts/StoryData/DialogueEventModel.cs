namespace KiyuzuDev.ITGWDO.StoryData
{
    /// <summary> 对话线触发事件的类型 </summary>
    public enum EnumDialogueEventType
    {
        None = 0,
        Wait,
        Style,
        CGLoad,
        CGUnLoad,
        BlackOn,
        BlackOff,
        HumanLoad,
        HumanChangePos,
        HumanUnload,
    }
    
    /// <summary> 事件触发的模型 </summary>
    [System.Serializable]
    public class DialogueEvent
    {
        public EnumDialogueEventType eventType = EnumDialogueEventType.None;
        public string[] args;
    }
}