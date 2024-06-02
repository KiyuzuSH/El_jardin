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
        
        public int NextLineID { get; set; }
        
        public string DoneWineName { get; set; }
        
        // TODO: Wine DATA
        
        private void OnEnable()
        {
            JalousieShutDown = false;
            PresentWorldStyle = WorldStyle.Utopia;
        }

        public void SetWorldStyle(WorldStyle targetStyle)
        {
            if(View.AVGView.Instance != null)
            {
                Debug.Log("Changed AVGView Style");
                View.AVGView.Instance.ChangeToStyleView(targetStyle);
            }
            if(View.AVGBackgroundView.Instance != null)
            {
                Debug.Log("Changed AVGBackgroundView Style");
                View.AVGBackgroundView.Instance.ChangeToStyleView(targetStyle);
            }
            PresentWorldStyle = targetStyle;
		}

        private void Start()
        {
            SetWorldStyle(PresentWorldStyle);
        }
    }
}
