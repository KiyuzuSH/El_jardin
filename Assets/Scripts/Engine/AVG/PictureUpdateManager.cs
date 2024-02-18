using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PictureUpdateManager : MonoBehaviour
    {
        public static PictureUpdateManager Instance { get; private set; }

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

        public Image tachie;
        public Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();
    }
}