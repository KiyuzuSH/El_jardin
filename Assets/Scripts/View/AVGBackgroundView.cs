using KiyuzuDev.ITGWDO.Core;
using UnityEngine;
using UnityEngine.UI;

namespace KiyuzuDev.ITGWDO.View
{
    public class AVGBackgroundView : MonoBehaviour
    {
        #region Singleton

        public static AVGBackgroundView Instance { get; private set; }

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

        #region 风格改变

        private const string pathModernSprites = "Sprites/Theme/modern/";
        private const string pathRPGSprites = "Sprites/Theme/rpg/";
        private const string pathUtopiaSprites = "Sprites/Theme/utopia/";
        
        [SerializeField] private Image outsidePic;
        [SerializeField] private Image interiorPic;
        [SerializeField] private GameObject jalousiePic;
        
        public void ChangeToStyleView(WorldStyle _style)
        {
            switch (_style)
            {
                case WorldStyle.Modern:
                    interiorPic.sprite = Resources.Load<Sprite>(pathModernSprites + "deco/modern_in");
                    outsidePic.sprite = Resources.Load<Sprite>(pathModernSprites + "deco/modern_out");
                    jalousiePic.GetComponent<Image>().sprite =
                        Resources.Load<Sprite>(pathModernSprites + 
                                               (GlobalDataManager.Instance.JalousieShutDown ?
                                                   "deco/modern_jalousie_shutten" :
                                                   "deco/modern_jalousie_fullopen"));
                    break;
                case WorldStyle.RPG:
                    interiorPic.sprite = Resources.Load<Sprite>(pathRPGSprites + "deco/rpg_in");
                    outsidePic.sprite = Resources.Load<Sprite>(pathRPGSprites + "deco/rpg_out");
                    jalousiePic.GetComponent<Image>().sprite =
                        Resources.Load<Sprite>(pathRPGSprites + 
                                               (GlobalDataManager.Instance.JalousieShutDown ?
                                                   "deco/rpg_jalousie_shutten" :
                                                   "deco/rpg_jalousie_fullopen"));
                    break;
                case WorldStyle.Utopia:
                    interiorPic.sprite = Resources.Load<Sprite>(pathUtopiaSprites + "deco/utopia_in");
                    outsidePic.sprite = Resources.Load<Sprite>(pathUtopiaSprites + "deco/utopia_out");
                    jalousiePic.GetComponent<Image>().sprite =
                        Resources.Load<Sprite>(pathUtopiaSprites + 
                                               (GlobalDataManager.Instance.JalousieShutDown ?
                                                   "deco/utopia_jalousie_shutten" :
                                                   "deco/utopia_jalousie_fullopen"));

                    break;
            }
        }

        #endregion
        
        #region 大小CG显隐

        [SerializeField] private GameObject fullCG;
        [SerializeField] private GameObject smallCG;

        public void FullCGOn(Sprite img)
        {
            fullCG.GetComponent<Image>().sprite = img;
            fullCG.GetComponent<CanvasGroup>().alpha = 1;
        }

        public void FullCGOff()
        {
            fullCG.GetComponent<Image>().sprite = null;
            fullCG.GetComponent<CanvasGroup>().alpha = 0;
        }
        public void PartCGOn(Sprite img)
        {
            var tmp = smallCG.GetComponent<Image>();
            tmp.sprite = img;
            tmp.SetNativeSize();
            smallCG.GetComponent<CanvasGroup>().alpha = 1;
        }

        public void PartCGOff()
        {
            smallCG.GetComponent<Image>().sprite = null;
            smallCG.GetComponent<CanvasGroup>().alpha = 0;
        }

        #endregion

        #region 人物立绘放置

        [SerializeField] private Transform ManPicPivot;
        [SerializeField] private GameObject ManPicPrefab;

        public void PlaceManPic(Sprite _sprite, float xPos)
        {
            var pic = Instantiate(ManPicPrefab, ManPicPivot);
            pic.GetComponent<Image>().sprite = _sprite;
            pic.GetComponent<Image>().SetNativeSize();
            pic.transform.position = new Vector3(xPos, 0, 0);
        }

        public void RemoveManPic(int id)
            => Destroy(ManPicPivot.GetChild(id).gameObject);
        

        public void RemoveAllMan()
        {
            for (int i = 0; i < ManPicPivot.childCount; i++)
                Destroy(ManPicPivot.GetChild(i).gameObject);
        }

        #endregion
    }
}