using TMPro;
using UnityEngine;

namespace Game
{
    public class ShowSideTitleManager : MonoBehaviour
    {
        public static ShowSideTitleManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this)
            {
                Destroy(gameObject);
                Instance = this;
            }
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            Destroy(Instance);
        }

        public TMP_Text Line0;
        public TMP_Text Line1;
        
        public void ShowTitle(string _text)
        {
            string[] content = _text.Split('|');
            Line0.text = content[0];
            Line1.text = content[1];
            if (!gameObject.activeSelf) gameObject.SetActive(true);
            Invoke(nameof(Inactive), 3);
        }
        
        public void Inactive() => gameObject.SetActive(false);
    }
}