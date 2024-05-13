using UnityEngine;

namespace KiyuzuDev.ITGWDO.AVG
{
    public class ChoiceButtonManager : MonoBehaviour
    {
        public static ChoiceButtonManager Instance { get; private set; }

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