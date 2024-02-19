using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Picture
    {
        public string Name { get; set; }
        public Sprite Pic { get; set; }
        public Transform Pos { get; set; }
    }
    
    public class PictureViewManager : MonoBehaviour
    {
        public static PictureViewManager Instance { get; private set; }

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

        public Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();
        public Transform leftAnchor;
        public Transform centerAnchor;
        public Transform rightAnchor;

        public Sprite background;
        
        public List<Picture> PicList;
        
        
    }
}