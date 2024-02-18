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

        private void Start()
        {
            scripts = Resources.LoadAll<TextAsset>("StoryScripts").ToList();
            
            //TODO: Should can be determined by save data
            ScriptIndex = 0;
            
            currentSheet = SetCurrentSheet(scripts[ScriptIndex]); 
            
            //TODO: Should can be determined by save data
            CurrentLine = 0; 
        }
        
        #region Text Asset List

        private static List<TextAsset> scripts;
        private static int _scriptIndex;
        private int ScriptIndex
        {
            get => _scriptIndex;
            set => _scriptIndex = value;
        }

        #endregion

        #region Dialog sheet

        private List<string[]> currentSheet;
        
        public List<string[]> GetCurrentSheet() => currentSheet;
        
        private List<string[]> SetCurrentSheet(TextAsset _tA)
        {
            List<string[]> sheet = new();
            List<string> temp = _tA.text.Split('\n').ToList();
            foreach (var line in temp) 
                sheet.Add(line.Split(','));
            Debug.Log("Succeed to read");
            return sheet;
        }
        
        public void UpdateSheet()
        {
            //TODO: Update Content in the Sheet
        }

        private int _currentLine;
        public int CurrentLine
        {
            get => _currentLine;
            set => _currentLine = value;
        }

        #endregion
    }
}