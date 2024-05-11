using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class ScriptManager : MonoBehaviour
    {
        public static ScriptManager Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this)
            {
                Destroy(gameObject);
                Instance = this;
            }
        }
        
        private void OnDestroy()
        {
            Destroy(Instance);
        }

        private List<TextAsset> scriptAssets;

        private int _scriptIndex;
        
        
        public int CurrentLine { get; set; }
        
        private void Start()
        {
            scriptAssets = Resources.LoadAll<TextAsset>("StoryScripts").ToList();
            
            _scriptIndex = 0;
            
            currentSheet = SetCurrentSheet(scriptAssets[_scriptIndex]); 
            
            //TODO: Should can be determined by save data
            CurrentLine = 0; 
        }

        #region Dialog sheet

        private List<string[]> currentSheet;

        public string[] GetLine(int _id) => currentSheet[_id];
        
        private List<string[]> SetCurrentSheet(TextAsset _tA)
        {
            List<string[]> sheet = new();
            List<string> temp = _tA.text.Split('\n').ToList();
            foreach (var line in temp) 
                sheet.Add(line.Split(','));
            return sheet;
        }
        
        
        #endregion
    }
}