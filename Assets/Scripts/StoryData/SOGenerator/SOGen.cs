using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using KiyuzuDev.ITGWDO.StoryData;

namespace KiyuzuDev.ITGWDO.StoryData
{
    [Serializable]
    public class DialogueLineRaw
    {
        public string lineId;
        public string dialogueLineType;
        public string personName;
        public string content;
        public string eventFore;
        public string eventForeArgs;
        public string eventMain;
        public string eventMainArgs;
        public string eventAfter;
        public string eventAfterArgs;
        public string toLine;
    }

    public class SOGen : MonoBehaviour
    {
        // Asset文件保存路径
        const string pathSO = "Assets/Resources/StorySO/";
        const string pathScripts = "Assets/Resources/StoryScripts/";

        private DialogueLineRaw[] dialogueRaws;
        
        private DialogueEvent ParseEvent(string _eventType, string _eventArgs)
        {
            var dEvent = new DialogueEvent();
            Enum.TryParse(_eventType, out dEvent.eventType);
            if (_eventArgs == null) dEvent.args = Array.Empty<string>();
            else
            {
                if (!_eventArgs.Contains(",")) dEvent.args = new[] { _eventArgs };
                else
                {
                    string[] arg = _eventArgs.Split(",");
                    dEvent.args = arg;
                }
            }

            return dEvent;
        }

        private int ParseToLine(string _toLine)
        {
            if (_toLine == null) return 0;
            else
            {
                int.TryParse(_toLine.Substring(1, 3), out int res);
                return res;
            }
        }

        private void CreateStoryAsset(string scriptFileName)
        {
            TextAsset tA = AssetDatabase.LoadAssetAtPath<TextAsset>(pathScripts + scriptFileName + ".csv");
            dialogueRaws = CSVSerializer.Deserialize<DialogueLineRaw>(tA.text);
            List<DialogueLine> lineList = new List<DialogueLine>();
            
            foreach (var lineRaw in dialogueRaws)
            {
                DialogueLine line = new DialogueLine();
                int.TryParse(lineRaw.lineId.Substring(1, 3), out line.lineId);
                Enum.TryParse(lineRaw.dialogueLineType, out line.DialogueLineType);
                line.personName = lineRaw.personName;
                line.content = lineRaw.content;
                line.eventFore = ParseEvent(lineRaw.eventFore, lineRaw.eventForeArgs);
                line.eventMain = ParseEvent(lineRaw.eventMain, lineRaw.eventMainArgs);
                line.eventAfter = ParseEvent(lineRaw.eventAfter, lineRaw.eventAfterArgs);
                line.toLine = ParseToLine(lineRaw.toLine);
                lineList.Add(line);
            }

            StorySheet story = ScriptableObject.CreateInstance<StorySheet>();
            story.storyId = dialogueRaws[0].lineId.Substring(0, 1);
            story.dialogueLines = lineList;
            AssetDatabase.CreateAsset(story, pathSO + scriptFileName + ".asset");
            EditorUtility.SetDirty(story);
            AssetDatabase.SaveAssets();
        }

        public void CreateDialogueLineIDMap()
        {
            List<StorySheet> storyList = Resources.LoadAll<StorySheet>("StorySO").ToList();
        }

        private void Start()
        {
            CreateStoryAsset("0531");
        }
    }

}