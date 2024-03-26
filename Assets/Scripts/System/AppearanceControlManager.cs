using UnityEngine;

namespace Game
{
    public class AppearanceControlManager : MonoBehaviour
    {
        public static AppearanceControlManager Instance { get; private set; }
        
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

        public GameObject AVGPanel;
        public GameObject BartendingPanel;
        
        
    }
}