using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class SentenceLine
    {
        public string character;
        [TextArea]
        public string content;
    }
    
    [CreateAssetMenu(fileName = "DialogueSO", menuName = "Dialog/DialogueData")]
    public class DialogueData : ScriptableObject
    {
        public List<SentenceLine> dataList = new List<SentenceLine>();
    }
}
