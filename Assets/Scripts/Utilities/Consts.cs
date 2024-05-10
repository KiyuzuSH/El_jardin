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

    public struct WineIngr
    {
        public GameObject[] lstGin;
        public GameObject[] lstWhisky;
        public GameObject[] lstTequila;
        public GameObject[] lstRum;
        public GameObject[] lstVodka;
        public GameObject iceGO;
        public GameObject[] lstHoney;
        public GameObject[] lstSpice;
        public GameObject[] lstSalt;
        public GameObject[] lstRose;
        public GameObject[] lstCitrus;
        public bool lemonAdded;
        public bool berryAdded;
    }

    public enum IngrType
    {
        Gin = 0,
        Whisky = 1,
        Tequila = 2,
        Rum = 3,
        Vodka = 4,
        Ice = -1,
        Lemon = -2,//??
        Honey = 5,
        Berry = -3,//??
        Spice = 16,
        Salt = 17,
        Rose = 6,
        Citrus = 7,
    }

    public enum WorldStyle
    {
        Modern = 1,
        RPG = 2,
        Utopia = 3,
    }
    
    public enum EnumDialogueLineType
    {
        NarrationLine,
        PersonLine,
        MindLine,
        ChooseLine,
        CGLine,
        GameLine,
        BoxLine,
    }
    
    /// <summary> 角色立绘在屏幕上的位置 </summary>
    public enum EnumCharacterPos
    {
        Left = -1,
        Center = 0,
        Right = 1,
    }

    /// <summary> 对话线触发事件的类型 </summary>
    public enum EnumDialogueEventType
    {
        end = -1,
        next = 0,
        choose = 1,
        jump = 2,
    }
}