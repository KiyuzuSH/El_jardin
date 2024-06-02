using System;
using System.Collections.Generic;
using System.Linq;
using KiyuzuDev.ITGWDO.StoryData;
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
        
        private List<StorySheet> storyList;

        private StorySheet _presentStory;
        public StorySheet PresentStory
        {
            get => _presentStory;
            private set => _presentStory = value;
        }

        public int PresentStoryId { get; set; }

        public int StoryListSize => storyList.Count;
        
        private void OnEnable()
        {
            storyList = Resources.LoadAll<StorySheet>("StorySO").ToList();
        }

        private void Start()
        {
            PresentStory = storyList[0];
            PresentStoryId = storyList[0].storyId;
            LoadIDs();
        }

        private void LoadIDs(){}
        
        private void UpdateStory(int id)
        {
            foreach (var sheet in storyList)
            {
                if (sheet.storyId == id)
                {
                    PresentStory = sheet;
                    PresentStoryId = id;
                }
            }
        }
        
        public static DialogueLine PresentLine { get; private set; }
        public static int PresentLineID { get; private set; }

        public StorySheet LoadStorySheetById(int id)
        {
            foreach (StorySheet storySheet in storyList)
                if (storySheet.storyId == id)
                    return storySheet;
            return null;
        }

        public void LoadLineById(int storyId, int lineId) 
            => PresentLine = LoadStorySheetById(storyId).dialogueLines[lineId];

        public void SetPresentLineId() => PresentStoryId = PresentLine.lineId;
        
        public void LoadLineByIdPresent(int lineId) => PresentLine = PresentStory.dialogueLines[lineId];

        public DialogueLine LoadLineDataPresent(int lineId) => PresentStory.dialogueLines[lineId];

        public void SetLineDataPresent(int lineId) => PresentLine = PresentStory.dialogueLines[lineId];

    }
}