using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "StoryLine_",menuName = "StoryLine",order = 2)]
    public class StoryLines : ScriptableObject
    {
        public int chapterId;
        /// <summary> 章节标题 </summary>
        public string title = "章节标题";
        public List<DialogueLine> dialogueLines;
    }
}