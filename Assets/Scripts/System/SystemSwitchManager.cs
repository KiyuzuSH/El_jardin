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

        // Start Panel
        public GameObject startPanel;
        
        // Hourglass
        public GameObject hourglass;
        
        // AVG Bundle
        public GameObject AVGPanel;
        public GameObject CharacterPanel;
        
        // Bar Tending Bundle
        public GameObject bartendPanel;
        public GameObject cupCompo;
        
        // Shaking Bundle
        public GameObject shakingPanel;

        public void Start()
        {
            startPanel.SetActive(true);
            hourglass.SetActive(false);
            AVGPanel.SetActive(false);
            CharacterPanel.SetActive(false);
            bartendPanel.SetActive(false);
            cupCompo.SetActive(false);
            shakingPanel.SetActive(false);
        }

        public void AVGMode()
        {
            startPanel.SetActive(false);
            hourglass.SetActive(true);
            AVGPanel.SetActive(true);
            CharacterPanel.SetActive(true);
            bartendPanel.SetActive(false);
            cupCompo.SetActive(false);
            shakingPanel.SetActive(false);
        }

        public void BarTendMode()
        {
            startPanel.SetActive(false);
            hourglass.SetActive(true);
            AVGPanel.SetActive(false);
            CharacterPanel.SetActive(false);
            bartendPanel.SetActive(true);
            cupCompo.SetActive(true);
            shakingPanel.SetActive(false);
        }

        public void ShakeMode()
        {
            startPanel.SetActive(false);
            hourglass.SetActive(true);
            AVGPanel.SetActive(false);
            CharacterPanel.SetActive(false);
            bartendPanel.SetActive(false);
            cupCompo.SetActive(false);
            shakingPanel.SetActive(true);
        }
    }
}