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
        }

        private void Start()
        {
            PresentStory = storyList[0];
            PresentStoryId = storyList[0].storyId;
            LoadIDs();
        }
		#endregion

		#region Fields
		private List<StorySheet> storyList;
        private StorySheet _presentStory;
        /// <summary>Line ID ������ story sheet ��������ı�</summary>
        /// <remarks>������Ϊ˽�г�Ա������ <c>PresentStory</c> ʱ���£���������</remarks>
        private Dictionary<int, int> lineIdIndexMap;
		#endregion

		#region Properties
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

		public static DialogueLine PresentLine { get; private set; }
        public static int PresentLineID { get; private set; }

        public int PresentStoryId { get; set; }

        public int StoryListSize => storyList.Count;
		#endregion

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

        public StorySheet LoadStorySheetById(int id)
        {
            foreach (StorySheet storySheet in storyList)
                if (storySheet.storyId == id)
                    return storySheet;
            return null;
        }

        public DialogueLine GetLineById(int lineId) {
            if(!lineIdIndexMap.ContainsKey(lineId)) {
                Debug.LogWarning($"��ͼ�ӹ���\"{PresentStory.name}\"�л�ȡ ID Ϊ\"{lineId}\"�� line ʧ�ܡ�");
                return null;
            }
            return PresentStory.dialogueLines[lineIdIndexMap[lineId]];
        }

        public void LoadLineById(int storyId, int lineId) 
            => PresentLine = LoadStorySheetById(storyId).dialogueLines[lineId];

        public void SetPresentLineId() => PresentStoryId = PresentLine.lineId;
        
        public void LoadLineByIdPresent(int lineId) => PresentLine = PresentStory.dialogueLines[lineId];

        public DialogueLine LoadLineDataPresent(int lineId) => PresentStory.dialogueLines[lineId];

        public void SetLineDataPresent(int lineId) => PresentLine = PresentStory.dialogueLines[lineId];

    }
}