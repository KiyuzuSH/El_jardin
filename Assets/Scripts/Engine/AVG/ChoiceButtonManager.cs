using TMPro;
using UnityEngine;

namespace Game
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

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            Destroy(Instance);
        }
        
        public void ShowButton(string _text)
        {
            string[] content = _text.Split('|');
            gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = content[0];
            gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = content[1];
            if (!gameObject.activeSelf) gameObject.SetActive(true);
            Invoke(nameof(Inactive), 3);
        }
        
        public void Inactive() => gameObject.SetActive(false);
    }
}