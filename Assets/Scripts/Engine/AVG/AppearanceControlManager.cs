using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class AppearanceControlManager : MonoBehaviour
    {
        public static AppearanceControlManager Instance { get; private set; }
        
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

        public Button modernChangeButton;
        public Button rpgChangeButton;
        public Button utopiaChangeButton;
        public Button trainChangeButton;

        public SpriteRenderer interior;
        public SpriteRenderer outside;
        public SpriteRenderer jalousie;
        public Image shelf;
        public Image wineListImg;
        public Image wineListBtn;
        public bool JalousieShutdown { get; set; }
        public WorldStyle WorldStyle { get; private set; }

        private void Start()
        {
            JalousieShutdown = false;
            WorldStyle = WorldStyle.Modern;
            SetStyle(WorldStyle.Modern);
            modernChangeButton.onClick.AddListener(
                delegate { SetStyle(WorldStyle.Modern); });
            rpgChangeButton.onClick.AddListener(
                delegate { SetStyle(WorldStyle.RPG); });
            utopiaChangeButton.onClick.AddListener(
                delegate { SetStyle(WorldStyle.Utopia); });
            trainChangeButton.onClick.AddListener(
                delegate { SetAloneBackgroundPic(Resources.Load<Sprite>("Sprites/Background/train_bg")); });
        }

        public void SetStyle(WorldStyle _style)
        {
            switch (_style)
            {
                case WorldStyle.Modern:
                    interior.sprite = Resources.Load<Sprite>("Sprites/Stylized/modern/modern_bg_in_static");
                    outside.sprite = Resources.Load<Sprite>("Sprites/Stylized/modern/modern_bg_out_static");
                    if(!JalousieShutdown) 
                        jalousie.sprite = Resources.Load<Sprite>("Sprites/Stylized/modern/modern_jalousie");
                    // else
                        // jalousie.sprite = Resources.Load<Sprite>("Sprites/Stylized/modern/modern_jalousie");
                    shelf.sprite = Resources.Load<Sprite>("Sprites/Stylized/modern/modern_shelf");
                    wineListImg.sprite = Resources.Load<Sprite>("Sprites/Stylized/modern/modern_winelist");
                    wineListBtn.sprite = Resources.Load<Sprite>("Sprites/Stylized/modern/modern_wineui");
                    break;
                case WorldStyle.RPG:
                    interior.sprite = Resources.Load<Sprite>("Sprites/Stylized/rpg/rpg_bg_in_static");
                    outside.sprite = Resources.Load<Sprite>("Sprites/Stylized/rpg/rpg_bg_out_static");
                    if(!JalousieShutdown) 
                        jalousie.sprite = Resources.Load<Sprite>("Sprites/Stylized/rpg/rpg_jalousie");
                    // else
                    // jalousie.sprite = Resources.Load<Sprite>("Sprites/Stylized/rpg/rpg_jalousie");
                    shelf.sprite = Resources.Load<Sprite>("Sprites/Stylized/rpg/rpg_shelf");
                    wineListImg.sprite = Resources.Load<Sprite>("Sprites/Stylized/rpg/rpg_winelist");
                    wineListBtn.sprite = Resources.Load<Sprite>("Sprites/Stylized/rpg/rpg_wineui");
                    break;
                case WorldStyle.Utopia:
                    interior.sprite = Resources.Load<Sprite>("Sprites/Stylized/utopia/utopia_bg_in_static");
                    outside.sprite = Resources.Load<Sprite>("Sprites/Stylized/utopia/utopia_bg_out_static");
                    if(!JalousieShutdown) 
                        jalousie.sprite = Resources.Load<Sprite>("Sprites/Stylized/utopia/utopia_jalousie");
                    // else
                    // jalousie.sprite = Resources.Load<Sprite>("Sprites/Stylized/utopia/utopia_jalousie");
                    shelf.sprite = Resources.Load<Sprite>("Sprites/Stylized/utopia/utopia_shelf");
                    wineListImg.sprite = Resources.Load<Sprite>("Sprites/Stylized/utopia/utopia_winelist");
                    wineListBtn.sprite = Resources.Load<Sprite>("Sprites/Stylized/utopia/utopia_wineui");
                    break;
            }
        }

        public void SetAloneBackgroundPic(Sprite _picture)
        {
            interior.sprite = _picture;
            outside.sprite = null;
            jalousie.sprite = null;
        }

        private void RenderByType()
        {
            
        }

        public GameObject AVGPanel;
        public GameObject BartendingPanel;
        
        
    }
}