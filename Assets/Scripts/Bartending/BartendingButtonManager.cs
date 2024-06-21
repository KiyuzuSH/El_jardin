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
            btnClear.onClick.AddListener(OnClearCup);
        
            btnList.GetComponent<Button>().onClick.AddListener(OnListButtonClick);
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
            btnClear.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(Instance);
        }

        [SerializeField] private GameObject btnList;
        [SerializeField] private GameObject btnCollection;
        [SerializeField] private CanvasGroup ListBox;
        [SerializeField] private GameObject btnGin;
        [SerializeField] private GameObject btnWhisky;
        [SerializeField] private GameObject btnTequila;
        [SerializeField] private GameObject btnRum;
        [SerializeField] private GameObject btnVodka;
        [SerializeField] private GameObject btnIce;
        [SerializeField] private GameObject btnLemon;
        [SerializeField] private GameObject btnHoney;
        [SerializeField] private GameObject btnBerry;
        [SerializeField] private GameObject btnSpice;
        [SerializeField] private GameObject btnSalt;
        [SerializeField] private GameObject btnRoseOil;
        [SerializeField] private GameObject btnCitrusOil;


        private void OnListButtonClick()
            => ListBox.alpha = 1 - ListBox.alpha;

        private void OnIngredientClick(IngrType _type) => WineManager.Instance.AddType(_type);
        

        private void OnIceAdded() => WineManager.Instance.AddType(IngrType.Ice);

        private void OnAfterAdded(IngrType _type) => WineManager.Instance.AddType(_type);
        
        [SerializeField] private Button btnSubmit;
        [SerializeField] private Button btnClear;

        private void OnSubmit()
        {
            // TODO: Wine DATA
            BartendingManager.Instance.SwitchToShake();
        }

        private void OnClearCup()
        {
            CheckerManager.Instance.ClearTxt();
            WineManager.Instance.ClearAllItem();
            CheckerManager.Instance.CheckThings();
        }
    }
}
