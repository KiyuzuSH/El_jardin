using System.Collections.Generic;
using UnityEngine;

namespace KiyuzuDev.ITGWDO.StoryData
{
    [CreateAssetMenu(fileName = "Story_",menuName = "Story")]
    public class StorySheet : ScriptableObject
    {
        public int storyId;
        public List<DialogueLine> dialogueLines;

        public DialogueLine GetSpecificLineById(int id) => dialogueLines[id];
    }
}