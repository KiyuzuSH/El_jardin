using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class WinelistManager : MonoBehaviour
    {
        public static WinelistManager Instance { get; private set; }
        
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

        private static List<TextAsset> winelist;
    }
}