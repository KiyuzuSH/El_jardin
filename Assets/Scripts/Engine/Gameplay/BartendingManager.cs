using UnityEngine;

namespace Game
{
    public class BartendingManager : MonoBehaviour
    {
        public static BartendingManager Instance { get; private set; }
        
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
