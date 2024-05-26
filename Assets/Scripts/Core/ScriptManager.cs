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
        
        private void OnDestroy()
        {
            Destroy(Instance);
        }

        #endregion
        
        private List<StorySheet> storyList;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this)
            {
                Destroy(gameObject);
                Instance = this;
            }
            
            storyList = Resources.LoadAll<StorySheet>("StorySO").ToList();
        }

        public int StoryListSize => storyList.Count;

        public List<int> StoryIndexList()
        {
            List<int> res = new List<int>();
            foreach (StorySheet storySheet in storyList) res.Add(storySheet.storyId);
            return res;
        }

        public StorySheet LoadStorySheetById(int id)
        {
            foreach (StorySheet storySheet in storyList)
                if (storySheet.storyId == id)
                    return storySheet;
            return null;
        }
        
        public DialogueLine LoadSpecificLine(int storyId, int lineId) 
            => LoadStorySheetById(storyId).GetSpecificLine(lineId);

        public DialogueLine LoadSpecificLine(int lineId)
            => LoadSpecificLine(int.Parse(lineId.ToString().Substring(0, 1)), lineId);



    }
}