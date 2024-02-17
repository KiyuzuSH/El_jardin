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

        private void Start()
        {
            scripts = SetScriptList();
            
            ScriptIndex = 0;
            if (scripts.Count > 0)
                dialogueSheet = SetDialogueSheet(scripts[ScriptIndex]);
            //TODO: Should can be determined by save data
            
            CurrentLine = DialogueManager.Instance.currentLine;
            //TODO:Sync
        }

        private void OnDestroy()
        {
            Destroy(Instance);
        }
        
        #region Text Asset List

        [SerializeField] private List<TextAsset> scripts;
        
        private List<TextAsset> SetScriptList() => Resources.LoadAll<TextAsset>("StoryScripts").ToList();

        private List<TextAsset> GetScriptList() => scripts;
        
        
        private int _scriptIndex;
        public int ScriptIndex { get; set; }

        #endregion

        #region Dialog sheet

        private List<string[]> dialogueSheet;
        
        public List<string[]> GetDialogueSheet() => dialogueSheet;
        
        private List<string[]> SetDialogueSheet(TextAsset _tA)
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


        public int CurrentLine { get; set; }

        #endregion
    }
}