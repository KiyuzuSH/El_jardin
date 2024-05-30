using UnityEngine;

namespace KiyuzuDev.ITGWDO.Core
{
    public class GlobalDataManager : MonoBehaviour
    {
        #region Singleton

        public static GlobalDataManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this)
            {
                Destroy(gameObject);
                Instance = this;
            }
            DontDestroyOnLoad(Instance);
        }

        private void OnDestroy()
        {
            Destroy(Instance);
        }

        #endregion
        
        public WorldStyle PresentWorldStyle { get; private set; }
        public bool JalousieShutDown { get; private set; }
        
        private void OnEnable()
        {
            JalousieShutDown = true;
            PresentWorldStyle = WorldStyle.Utopia;
        }

        public void SetWorldStyle(WorldStyle targetStyle)
        {
            PresentWorldStyle = targetStyle;
            View.AVGView.Instance.ChangeToStyleView(PresentWorldStyle);
        }

        private void Start()
        {
            // View.AVGView.Instance.ChangeToStyleView(PresentWorldStyle);
        }
    }
}
