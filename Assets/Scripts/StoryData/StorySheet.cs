using System.Collections.Generic;
using UnityEngine;

namespace KiyuzuDev.ITGWDO.StoryData
{
    [CreateAssetMenu(fileName = "Story_",menuName = "Story")]
    public class StorySheet : ScriptableObject
    {
        public int storyId;
        [SerializeField]
        public List<DialogueLine> dialogueLines;
    }
}