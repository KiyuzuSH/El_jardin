using System.Collections.Generic;
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
            btnLemon.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Lemon); });
            btnHoney.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Honey); });
            btnBerry.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Berry); });
            btnSpice.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Spice); });
            btnSalt.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.Salt); });
            btnRoseEssentialOil.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.RoseOil); });
            btnCitrusEssentialOil.GetComponent<Button>().onClick.AddListener(delegate { OnIngredientClick(IngrType.CitrusOil); });
        }
        
        private void OnDestroy()
        {
            Destroy(Instance);
        }

        public GameObject btnList;
        public Transform wineList;
        private bool AtLeft { get; set; }

        public GameObject winePrefab;
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
            Instantiate(winePrefab, wineSpawnPoint);
        }
    }
}
