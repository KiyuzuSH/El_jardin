using System;
using System.Collections.Generic;
using System.Linq;
using KiyuzuDev.ITGWDO.StoryData;
using UnityEditor.Build.Pipeline.Tasks;
using UnityEngine;

namespace KiyuzuDev.ITGWDO.Core
{
    public class ScriptManager : MonoBehaviour
    {
        #region Singleton

        public static ScriptManager Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
        }

        #endregion

        #region StorySheet

        private static List<StorySheet> storyList;

        private static StorySheet _presentStory;
        public static StorySheet PresentStory
        {
            get => _presentStory;
            private set
            {
                _presentStory = value;
                if (value != null)
                {
                    idMap = new Dictionary<int, DialogueLine>();
                    foreach (DialogueLine line in PresentStory.dialogueLines)
                        idMap.Add(line.lineId, line);
                }
            }
        }

        public static string PresentStoryId { get; set; }

        private static Dictionary<int, DialogueLine> idMap;
        
        // public StorySheet GetStorySheet(string id)
        // {
        //     foreach (StorySheet storySheet in storyList)
        //         if (storySheet.storyId == id)
        //             return storySheet;
        //     return null;
        // }

        private void SetPresentStorySheet(string id)
        {
            foreach (StorySheet storySheet in storyList)
                if (storySheet.storyId == id)
                {
                    PresentStory = storySheet;
                    PresentStoryId = storySheet.storyId;
                    return;
                }
            PresentStory = null;
            PresentStoryId = null;
        }

        #endregion

        #region DialogueLine

        public static DialogueLine PresentLine { get; private set; }
        public static int PresentLineID { get; private set; }

        public DialogueLine GetDialogueLine(int lineId) => idMap[lineId];
        
        public void LoadLineById(string storyId, int lineId) 
            => PresentLine = PresentStory.dialogueLines[lineId];

        public void SetPresentLineId() => PresentLineID = PresentLine.lineId;
        
        public void LoadLineByIdPresent(int lineId) => PresentLine = PresentStory.dialogueLines[lineId];

        public DialogueLine LoadLineDataPresent(int lineId) => PresentStory.dialogueLines[lineId];

        public void SetLineDataPresent(int lineId) => PresentLine = PresentStory.dialogueLines[lineId];

        #endregion
        
        private void OnEnable()
        {
            storyList = Resources.LoadAll<StorySheet>("StorySO").ToList();
            SetPresentStorySheet("X");
        }
    }
}