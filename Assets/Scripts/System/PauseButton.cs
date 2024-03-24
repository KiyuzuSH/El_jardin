using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PauseButton : MonoBehaviour
    {
        public GameObject PausePanel;

        private void Start()
            => GetComponent<Button>().onClick.AddListener(PauseGame);
        
        private void PauseGame()
        {
            if (!PausePanel.activeSelf)
            {
                Time.timeScale = 0.0f;
                PausePanel.SetActive(true);
            }
        }

        private void OnDestroy()
            => GetComponent<Button>().onClick.RemoveAllListeners();
        
    }
}