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
            leftAnchor.GetComponent<SpriteRenderer>().sprite = null;
            centerAnchor.GetComponent<SpriteRenderer>().sprite = null;
            rightAnchor.GetComponent<SpriteRenderer>().sprite = null;
            imageDic.Add("Person",Resources.Load<Sprite>("Sprites/Characters/Person"));
            imageDic.Add("Person2",Resources.Load<Sprite>("Sprites/Characters/Person2"));
            imageDic.Add("Person3",Resources.Load<Sprite>("Sprites/Characters/Person3"));
        }

        private void OnDestroy()
        {
            Destroy(Instance);
        }

        [SerializeField]
        public Dictionary<string, Sprite> imageDic = new();
        
        public GameObject leftAnchor;
        public GameObject centerAnchor;
        public GameObject rightAnchor;

        public SpriteRenderer background;
        
        [SerializeField]
        public List<Picture> PicList;

        public void UpdateManPic(string type, string _name, string _style, string _pos)
        {
            bool setbool;
            if (type == "TRUE")
            {
                if (_name == "Clear")
                {
                    leftAnchor.GetComponent<SpriteRenderer>().sprite = null;
                    centerAnchor.GetComponent<SpriteRenderer>().sprite = null;
                    rightAnchor.GetComponent<SpriteRenderer>().sprite = null;
                }
                else
                {
                    var pos = _pos.Replace("\r", "");
                    switch (pos)
                    {
                        case "Left":
                            leftAnchor.GetComponent<SpriteRenderer>().sprite = imageDic[_name];
                            break;
                        case "Center":
                            centerAnchor.GetComponent<SpriteRenderer>().sprite = imageDic[_name];
                            break;
                        case "Right":
                            rightAnchor.GetComponent<SpriteRenderer>().sprite = imageDic[_name];
                            break;
                    }
                }
            }
            else if (type == "FALSE")
            {
                
            }
            else Debug.LogWarning("Type Error");
        }
    }
}