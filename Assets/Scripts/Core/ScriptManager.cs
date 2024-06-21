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
        
        #region Life cycle
        
        private void OnEnable()
        {
            storyList = Resources.LoadAll<StorySheet>("StorySO").ToList();
            SetPresentStorySheet("A");
        }

        private void Start()
        {
            SetLineById(1);
        }

        #endregion
        
        #region StorySheet

        private List<StorySheet> storyList;
        
        private StorySheet _presentStory;
        public StorySheet PresentStory
        {
            get => _presentStory;
            private set {
                _presentStory = value;

                // Update `lineIdIndexMap` here.
                lineIdIndexMap = new();
                for(int index = 0; index < _presentStory.dialogueLines.Count; ++index) {
                    var line = _presentStory.dialogueLines[index];
                    lineIdIndexMap.Add(line.lineId, index);
                }
            }
        }
        
        /// <summary>Line ID 到其在 story sheet 里的索引的表。</summary>
        /// <remarks>仅仅作为私有成员在设置 <c>PresentStory</c> 时更新，不公开。</remarks>
        private Dictionary<int, int> lineIdIndexMap;
        
        // public string PresentStoryId { get; set; }

        // public int StoryListSize => storyList.Count;
        
        private void SetPresentStorySheet(string id)
        {
            foreach (StorySheet storySheet in storyList)
                if (storySheet.storyId == id)
                {
                    PresentStory = storySheet;
                    // PresentStoryId = storySheet.storyId;
                    return;
                }
            PresentStory = null;
            // PresentStoryId = null;
        }
        
        // public StorySheet GetStorySheet(string id)
        // {
        //     foreach (StorySheet storySheet in storyList)
        //         if (storySheet.storyId == id)
        //             return storySheet;
        //     return null;
        // }

        #endregion

        #region DialogueLine
        
        public static DialogueLine PresentLine { get; private set; }
        
        public static int PresentLineID { get; private set; }
        
        public void SetLineById(int lineId) {
            if (!lineIdIndexMap.ContainsKey(lineId))
            {
                Debug.LogWarning($"试图从故事\"{PresentStory.name}\"中设定 ID=\"{lineId}\"的DialogueLine失败.");
                PresentLine = null;
                PresentLineID = -1;
                return;
            }
            PresentLine = PresentStory.dialogueLines[lineIdIndexMap[lineId]];
            PresentLineID = lineId;
        }

        public DialogueLine GetLineById(int lineId)
        {
            if(!lineIdIndexMap.ContainsKey(lineId)) {
                Debug.LogWarning($"试图从故事\"{PresentStory.name}\"中获取 ID=\"{lineId}\"的DialogueLine失败.");
                return null;
            }
            return PresentStory.dialogueLines[lineIdIndexMap[lineId]];
        }

        #endregion
    }
}

