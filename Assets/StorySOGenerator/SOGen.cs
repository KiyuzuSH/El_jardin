using System;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Game
{
    [Serializable]
    public class DialogueLineRaw
    {
        public int lineId = -1;
        public string dialogueLineType = "Narration";
        public string personName = "名字";
        public string content = "显示内容";
        public string events = "";
        public string personImg = "";
        public string position = "";
        public string atMindBox = "";
        public string spriteType = "";
        public string image = "";
    }
    
    public class SOGen : MonoBehaviour
    {
        // Asset文件保存路径
        private const string pathSO = "Assets/Resources/StorySO/";
        private const string pathPersonImgResource = "Sprites/Characters/";
        
        public DialogueLineRaw[] dialogueRaws;

        private void Start()
        {
            CreateTestAsset();
        }

        private void CreateTestAsset()
        {
            TextAsset tA = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/StoryScripts/TestChapterNew.csv");
            dialogueRaws = CSVSerializer.Deserialize<DialogueLineRaw>(tA.text);
            foreach (var lineRaw in dialogueRaws)
            {
                DialogueLine line = ScriptableObject.CreateInstance<DialogueLine>();
                line.lineId = lineRaw.lineId;
                Enum.TryParse(lineRaw.dialogueLineType + "Line",out line.DialogueLineType);
                line.personName = lineRaw.personName;
                line.content = lineRaw.content;
                // TODO: 定义事件加载逻辑
                // line.events = ;
                // TODO: 注意加载路径
                line.personImg = Resources.Load<Sprite>(pathPersonImgResource
                                                        + lineRaw.personName + "/"
                                                        + lineRaw.personImg);
                Enum.TryParse(lineRaw.position,out line.position);
                bool.TryParse(lineRaw.atMindBox,out line.atMindBox);
                Enum.TryParse(lineRaw.spriteType,out line.spriteType);
                // TODO: 定义图片命名规范
                // line.image = Resources.Load<Sprite>("Sprites/"+?);
                AssetDatabase.CreateAsset(line, 
                    "Assets/Resources/StorySO/TestChapter/" + line.lineId + ".asset");
                EditorUtility.SetDirty(line);
            }
            AssetDatabase.SaveAssets();
        }
    }
}