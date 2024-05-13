using UnityEngine;
using UnityEngine.UI;

namespace KiyuzuDev.ITGWDO
{
    public class BartendingButtonManager : MonoBehaviour
    {
        public static BartendingButtonManager Instance { get; private set; }
        
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
            btnSubmit.onClick.AddListener(OnSubmit);
        
            btnList.GetComponent<Button>().onClick.AddListener(OnListButtonClick);
            AtLeft = true;
            btnGin.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Gin); });
            btnWhisky.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Whisky); });
            btnTequila.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Tequila); });
            btnRum.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Rum); });
            btnVodka.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Vodka); });
            btnIce.GetComponent<Button>().onClick.AddListener(OnIceAdded);
            btnLemon.GetComponent<Button>().onClick.AddListener(delegate { OnAfterAdded(IngrType.Lemon); });
            btnHoney.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Honey); });
            btnBerry.GetComponent<Button>().onClick.AddListener(delegate { OnAfterAdded(IngrType.Berry); });
            btnSpice.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Spice); });
            btnSalt.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Salt); });
            btnRoseOil.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Rose); });
            btnCitrusOil.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Citrus); });
        }
        
        private void OnDestroy()
        {
            btnSubmit.onClick.RemoveAllListeners();
            btnGin.GetComponent<Button>().onClick.RemoveAllListeners();
            btnWhisky.GetComponent<Button>().onClick.RemoveAllListeners();
            btnTequila.GetComponent<Button>().onClick.RemoveAllListeners();
            btnRum.GetComponent<Button>().onClick.RemoveAllListeners();
            btnVodka.GetComponent<Button>().onClick.RemoveAllListeners();
            btnIce.GetComponent<Button>().onClick.RemoveAllListeners();
            btnLemon.GetComponent<Button>().onClick.RemoveAllListeners();
            btnHoney.GetComponent<Button>().onClick.RemoveAllListeners();
            btnBerry.GetComponent<Button>().onClick.RemoveAllListeners();
            btnSpice.GetComponent<Button>().onClick.RemoveAllListeners();
            btnSalt.GetComponent<Button>().onClick.RemoveAllListeners();
            btnRoseOil.GetComponent<Button>().onClick.RemoveAllListeners();
            btnCitrusOil.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(Instance);
        }

        public GameObject btnList;
        public Transform wineList;
        private bool AtLeft { get; set; }

        
        public GameObject btnGin;
        public GameObject btnWhisky;
        public GameObject btnTequila;
        public GameObject btnRum;
        public GameObject btnVodka;
        public GameObject btnIce;
        public GameObject btnLemon;
        public GameObject btnHoney;
        public GameObject btnBerry;
        public GameObject btnSpice;
        public GameObject btnSalt;
        public GameObject btnRoseOil;
        public GameObject btnCitrusOil;

        private void OnListButtonClick()
        {
            if (AtLeft) MoveRight();
            else MoveLeft();
        }

        // Animation
        private void MoveRight()
        {
            wineList.localPosition += new Vector3(1000, 0, 0);
            AtLeft = false;
        }

        private void MoveLeft()
        {
            wineList.localPosition += new Vector3(-1000, 0, 0);
            AtLeft = true;
        }

        private void OnIngredientClick(IngrType _type) => WineManager.Instance.AddType(_type);
        

        private void OnIceAdded() => WineManager.Instance.AddType(IngrType.Ice);

        private void OnAfterAdded(IngrType _type) => WineManager.Instance.AddType(_type);
        
        public Button btnSubmit;

        private void OnSubmit()
        {
            //TODO: wine data
            // SystemSwitchManager.Instance.ShakeMode();
        }
    }
}
