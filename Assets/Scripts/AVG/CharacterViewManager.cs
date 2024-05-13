using UnityEngine;

namespace KiyuzuDev.ITGWDO.AVG
{
    public class CharacterViewManager : MonoBehaviour
    {
        public static CharacterViewManager Instance { get; private set; }

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
            Clear();
            MoveOK = false;
        }

        private void OnDestroy()
        {
            Destroy(Instance);
        }
        
        public GameObject leftAnchor;
        public GameObject centerAnchor;
        public GameObject rightAnchor;

        // public void UpdateManPic(string type, string _name, string _style, string _pos)
        // {
        //     bool setbool;
        //     if (type == "TRUE")
        //     {
        //         if (_name == "Clear")
        //         {
        //             leftAnchor.GetComponent<SpriteRenderer>().sprite = null;
        //             centerAnchor.GetComponent<SpriteRenderer>().sprite = null;
        //             rightAnchor.GetComponent<SpriteRenderer>().sprite = null;
        //         }
        //         else
        //         {
        //             var pos = _pos.Replace("\r", "");
        //             switch (pos)
        //             {
        //                 case "Left":
        //                     leftAnchor.GetComponent<SpriteRenderer>().sprite = imageDic[_name];
        //                     break;
        //                 case "Center":
        //                     centerAnchor.GetComponent<SpriteRenderer>().sprite = imageDic[_name];
        //                     break;
        //                 case "Right":
        //                     rightAnchor.GetComponent<SpriteRenderer>().sprite = imageDic[_name];
        //                     break;
        //             }
        //         }
        //     }
        //     else if (type == "FALSE")
        //     {
        //         
        //     }
        //     else Debug.LogWarning("Type Error");
        // }

        public void Clear()
        {
            leftAnchor.GetComponent<SpriteRenderer>().sprite = null;
            centerAnchor.GetComponent<SpriteRenderer>().sprite = null;
            rightAnchor.GetComponent<SpriteRenderer>().sprite = null;
        }
        
        public void SetPolicePic() => centerAnchor.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("Sprites/Characters/NPC/utopia_police");
        
        public void GetWuTi()=>centerAnchor.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("Sprites/Characters/Main/WuTi");
        
        public void GetDaydream()=>centerAnchor.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("Sprites/Characters/Main/Daydream");

        public void SecondSet()
        {
            centerAnchor.GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("Sprites/Characters/Main/Dorothy");
            rightAnchor.GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("Sprites/Characters/Main/Augur");
        }

        public void MoveD()
        {
            MoveOK = true;
        }

        private bool MoveOK;
        
        public void Update()
        {
            if(MoveOK)
            {
                var targetPos = leftAnchor.transform.position;
                centerAnchor.transform.position = Vector3.Lerp(centerAnchor.transform.position, targetPos, 0.01f);
            }
        }

        public void SetCup()
        {
            rightAnchor.GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("Sprites/Wine/Empty");
            //TODO: Set Pos
            rightAnchor.transform.localPosition = new Vector3(2.7f, -4.08f, 0);
            rightAnchor.transform.localScale = new Vector3(3f, 3f, 1);
        }
    }
}