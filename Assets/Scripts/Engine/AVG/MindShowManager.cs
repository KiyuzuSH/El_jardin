using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MindShowManager : MonoBehaviour
    {
        public static MindShowManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this)
            {
                Destroy(gameObject);
                Instance = this;
            }
            gameObject.SetActive(false);
        }

        private void Start()
        {
            Lines = GetComponentInChildren<TMP_Text>();
            textBox = GetComponent<ScrollRect>();
            scrollbar = GetComponent<ScrollRect>().verticalScrollbar;
        }

        private void OnDestroy()
        { 
            Destroy(Instance);
        }
        
        private TMP_Text Lines;
        private ScrollRect textBox;
        private Scrollbar scrollbar;
        
        public void ShowLines(string _text)
        {
            Lines.text = _text;
            // if (!gameObject.activeSelf) gameObject.SetActive(true);
        }
        
        // public void Inactive() => gameObject.SetActive(false);
    }
}
