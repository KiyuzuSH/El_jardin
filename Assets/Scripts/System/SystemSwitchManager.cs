using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class SystemSwitchManager : MonoBehaviour
    {
        public static SystemSwitchManager Instance { get; private set; }
        
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