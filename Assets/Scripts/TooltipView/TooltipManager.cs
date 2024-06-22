using UnityEngine;

namespace KiyuzuDev.ITGWDO.TooltipView
{
    public class TooltipManager : MonoBehaviour
    {
        #region Singleton

        public static TooltipManager Instance { get; private set; }

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

        public Tooltip tooltip;

        public static void Show(string _content)
        {
            Instance.tooltip.SetText(_content);
            Instance.tooltip.gameObject.SetActive(true);
        }
        
        public static void Hide()
        {
            Instance.tooltip.gameObject.SetActive(false);
        }
        
        
        
    }
}
