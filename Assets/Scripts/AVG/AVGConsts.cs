using UnityEngine;

namespace KiyuzuDev.ITGWDO.AVGEngine
{
    public class AVGConsts : MonoBehaviour
    {
        public static bool DialogueTextNotJumping { get; set; }
        public static bool MindTextNotJumping { get; set; }

        [Range(0, 1)] public static float DialogueTextJumpTime = 0.075f;
        [Range(0, 1)] public static float MindTextJumpTime = 0.005f;
        
        private void Start()
        {
            DialogueTextNotJumping = true;
            MindTextNotJumping = true;
            if (DialogueTextJumpTime < 0f)
                DialogueTextJumpTime = 0.1f;
            if (MindTextJumpTime < 0f)
                MindTextJumpTime = 0.1f;
        }
    }
}