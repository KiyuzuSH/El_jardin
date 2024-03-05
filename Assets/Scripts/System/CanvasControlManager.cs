using UnityEngine;

namespace Game
{
    public class CanvasControlManager : MonoBehaviour
    {
        public static CanvasControlManager Instance { get; private set; }
        
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
    }
}