using UnityEngine;

namespace Game
{
    public class Consts
    {
        /// <summary> Path of the Game Save Data </summary>
        public static readonly string DataPath = Application.persistentDataPath + @"\data.xml";
    }

    /// <summary> 脚本数据 </summary>
    public class ScriptData
    {
        public ResourceType loadType;
        /// <summary> 角色名称 </summary>
        public string characterName;
        /// <summary> 图片资源名字 </summary>
        public string spriteName;
        /// <summary> 对话内容 </summary>
        public string dialogueContent;
        /// <summary> 角色立绘在屏幕上的位置 </summary>
        public CharacterPos characterPos;
        /// <summary> 是否水平翻转 </summary>
        public bool isHorizontalFlipped;
        /// <summary> 音频类型 </summary>
        public AudioType audioType;
        /// <summary> 音频名称 </summary>
        public string audioName;
    }

    /// <summary> 资源类型 </summary>
    public enum ResourceType
    {
        Background = 1,
        Character = 2,
    }
    
    /// <summary> 声音类型 </summary>
    public enum AudioType
    {
        sfx = 1,
        bgm = 2,
        speech = 3,
    }

    /// <summary> 角色立绘在屏幕上的位置 </summary>
    public enum CharacterPos
    {
        Left = -1,
        Center = 0,
        Right = 1,
    }
}