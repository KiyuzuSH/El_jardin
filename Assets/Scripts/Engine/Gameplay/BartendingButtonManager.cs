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
            btnGin.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick("Gin"); });
            btnWhisky.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick("Whisky"); });
            btnTequila.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick("Tequila"); });
            btnRum.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick("Rum"); });
            btnVodka.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick("Vodka"); });
            btnIce.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick("Ice"); });
            btnLemon.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick("Lemon"); });
            btnHoney.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick("Honey"); });
            btnBerry.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick("Berry"); });
            btnSult.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick("Sult"); });
            btnRosesult.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick("RoseSult"); });
            btnRoseessence.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick("RoseEssence"); });
            btnFloweressence.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick("FlowerEssence"); });
        }
        
        private void OnDestroy()
        {
            Destroy(Instance);
        }

        public GameObject btnList;
        public GameObject btnGin;
        public GameObject btnWhisky;
        public GameObject btnTequila;
        public GameObject btnRum;
        public GameObject btnVodka;
        public GameObject btnIce;
        public GameObject btnLemon;
        public GameObject btnHoney;
        public GameObject btnBerry;
        public GameObject btnSult;
        public GameObject btnRosesult;
        public GameObject btnRoseessence;
        public GameObject btnFloweressence;

        private void OnListButtonClick()
        {
            
        }

        private void OnIngredientClick(string _name)
        {
            
        }
    }
}
