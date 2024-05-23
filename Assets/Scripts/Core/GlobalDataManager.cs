using System;
using UnityEngine;

namespace KiyuzuDev.ITGWDO
{
    public enum WorldStyle
    {
        Modern = 1,
        RPG = 2,
        Utopia = 3,
    }
    
    public class GlobalDataManager : MonoBehaviour
    {
        #region Singleton

        public static GlobalDataManager Instance { get; private set; }

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
        
        public WorldStyle PresentWorldStyle { get; private set; }
        public bool JalousieShutDown { get; private set; }
        
        private void Start()
        {
            JalousieShutDown = true;
            SetStyle(WorldStyle.Utopia);
        }

        public void SetStyle(WorldStyle _style)
        {
            switch (_style)
            {
                case WorldStyle.Modern:
                    // interior.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_in");
                    // interior.color = Color.white;
                    // outside.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_out");
                    // if(JalousieShutDown) 
                    //     jalousie.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_jalousie_shutten");
                    // else
                    //     jalousie.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_jalousie_fullopen");
                    // shelf.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_shelf");
                    // wineListImg.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_winelist");
                    // wineListBtn.sprite = Resources.Load<Sprite>("Sprites/Theme/modern/modern_wineui");
                    break;
                case WorldStyle.RPG:
                    // interior.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_in");
                    // interior.color = Color.white;
                    // outside.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_out");
                    // if(JalousieShutDown) 
                    //     jalousie.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_jalousie_shutten");
                    // else
                    //     jalousie.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_jalousie_fullopen");
                    // shelf.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_shelf");
                    // wineListImg.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_winelist");
                    // wineListBtn.sprite = Resources.Load<Sprite>("Sprites/Theme/rpg/rpg_wineui");
                    break;
                case WorldStyle.Utopia:
                    // interior.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_in");
                    // interior.color = Color.white;
                    // outside.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_out");
                    // if(JalousieShutDown) 
                    //     jalousie.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_jalousie_shutten");
                    // else
                    //     jalousie.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_jalousie_fullopen");
                    // shelf.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_shelf");
                    // wineListImg.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_winelist");
                    // wineListBtn.sprite = Resources.Load<Sprite>("Sprites/Theme/utopia/utopia_wineui");
                    break;
            }
            PresentWorldStyle = _style;
        }
    }
}
