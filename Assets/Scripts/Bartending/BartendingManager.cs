using UnityEngine;

namespace KiyuzuDev.ITGWDO
{
    public class BartendingManager : MonoBehaviour
    {
        #region Singleton

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

        #endregion

        [SerializeField] private GameObject ShakingPanel;
        [SerializeField] private GameObject AddingPanel;
        [SerializeField] private GameObject ShakeCupBox;

        public void SwitchToShake()
        {
            ShakingPanel.SetActive(true);
            AddingPanel.SetActive(false);
            ShakeCupBox.SetActive(false);
        }

    }
}
