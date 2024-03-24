using UnityEngine;
using UnityEngine.UI;

namespace Game
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
            btnList.GetComponent<Button>().onClick.AddListener(OnListButtonClick);
            AtLeft = true;
            btnGin.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Gin); });
            btnWhisky.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Whisky); });
            btnTequila.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Tequila); });
            btnRum.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Rum); });
            btnVodka.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Vodka); });
            btnIce.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Ice); });
            // btnLemon.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Lemon); });
            btnHoney.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Honey); });
            // btnBerry.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Berry); });
            btnSpice.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Spice); });
            btnSalt.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Salt); });
            btnRoseEssentialOil.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Rose); });
            btnCitrusEssentialOil.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Citrus); });
        }
        
        private void OnDestroy()
        {
            btnGin.GetComponent<Button>().onClick.RemoveAllListeners();
            btnWhisky.GetComponent<Button>().onClick.RemoveAllListeners();
            btnTequila.GetComponent<Button>().onClick.RemoveAllListeners();
            btnRum.GetComponent<Button>().onClick.RemoveAllListeners();
            btnVodka.GetComponent<Button>().onClick.RemoveAllListeners();
            btnIce.GetComponent<Button>().onClick.RemoveAllListeners();
            // btnLemon.GetComponent<Button>().onClick.RemoveAllListeners();
            btnHoney.GetComponent<Button>().onClick.RemoveAllListeners();
            // btnBerry.GetComponent<Button>().onClick.RemoveAllListeners();
            btnSpice.GetComponent<Button>().onClick.RemoveAllListeners();
            btnSalt.GetComponent<Button>().onClick.RemoveAllListeners();
            btnRoseEssentialOil.GetComponent<Button>().onClick.RemoveAllListeners();
            btnCitrusEssentialOil.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(Instance);
        }

        public GameObject btnList;
        public Transform wineList;
        private bool AtLeft { get; set; }

        public Transform wineSpawnPoint;
        
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
        public GameObject btnRoseEssentialOil;
        public GameObject btnCitrusEssentialOil;

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

        private void OnIngredientClick(IngrType _type)
        {
            GameObject item = Resources.Load<GameObject>("Prefabs/WineIngredients/" + _type.ToString());
            Instantiate(item, wineSpawnPoint);
            CupManager.Instance.AddType(_type);
        }
    }
}
