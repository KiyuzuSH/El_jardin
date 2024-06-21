using UnityEngine;

namespace KiyuzuDev.ITGWDO
{
    public class WineManager : MonoBehaviour
    {
        public static WineManager Instance { get; private set; }
        
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
            TotalVol = 0;
            CupInit();
        }

        private void OnDestroy()
        {
            Destroy(Instance);
        }

        public static WineIngr wine;
        
        public Transform wineSpawnPoint;
        
        public int TotalVol { get; set; }

        public void ClearAllItem()
        {
            if (wine.lstGin != null)
                foreach (GameObject _item in wine.lstGin)
                    Destroy(_item);
            if (wine.lstWhisky != null)
                foreach (GameObject _item in wine.lstWhisky)
                    Destroy(_item);
            if (wine.lstTequila != null)
                foreach (GameObject _item in wine.lstTequila)
                    Destroy(_item);
            if (wine.lstRum != null)
                foreach (GameObject _item in wine.lstRum)
                    Destroy(_item);
            if (wine.lstVodka != null)
                foreach (GameObject _item in wine.lstVodka)
                    Destroy(_item);
            if (wine.iceGO)
                Destroy(wine.iceGO);
            if (wine.lstHoney != null)
                foreach (GameObject _item in wine.lstHoney)
                    Destroy(_item);
            if (wine.lstSpice != null)
                foreach (GameObject _item in wine.lstSpice)
                    Destroy(_item);
            if (wine.lstSalt != null)
                foreach (GameObject _item in wine.lstSalt)
                    Destroy(_item);
            if (wine.lstRose != null)
                foreach (GameObject _item in wine.lstRose)
                    Destroy(_item);
            if (wine.lstCitrus != null)
                foreach (GameObject _item in wine.lstCitrus)
                    Destroy(_item);
            TotalVol = 0;
        }

        private void CupInit()
        {
            ClearAllItem();
        }

        public void AddType(IngrType _type)
        {
            if (TotalVol - 150 > 0)
            {
                CheckerManager.Instance.FullWarning();
                return;
            }
            switch (_type)
            {
                case IngrType.Gin:
                    GenerateParticles(_type);
                    wine.lstGin = GameObject.FindGameObjectsWithTag("Gin");
                    TotalVol += 5;
                    break;
                case IngrType.Whisky:
                    GenerateParticles(_type);
                    wine.lstWhisky = GameObject.FindGameObjectsWithTag("Whisky");
                    TotalVol += 5;
                    break;
                case IngrType.Tequila:
                    GenerateParticles(_type);
                    wine.lstTequila = GameObject.FindGameObjectsWithTag("Tequila");
                    TotalVol += 5;
                    break;
                case IngrType.Rum:
                    GenerateParticles(_type);
                    wine.lstRum = GameObject.FindGameObjectsWithTag("Rum");
                    TotalVol += 5;
                    break;
                case IngrType.Vodka:
                    GenerateParticles(_type);
                    wine.lstVodka = GameObject.FindGameObjectsWithTag("Vodka");
                    TotalVol += 5;
                    break;
                case IngrType.Ice:
                    if (wine.iceGO) Destroy(wine.iceGO);
                    else
                        wine.iceGO = Instantiate(
                            Resources.Load<GameObject>(
                                "Prefabs/WineIngredients/Ice"
                            ), wineSpawnPoint
                        );
                    break;
                case IngrType.Honey:
                    GenerateParticles(_type);
                    wine.lstHoney = GameObject.FindGameObjectsWithTag("Honey");
                    TotalVol++;
                    break;
                case IngrType.Spice:
                    GenerateParticles(_type);
                    wine.lstSpice = GameObject.FindGameObjectsWithTag("Spice");
                    break;
                case IngrType.Salt:
                    GenerateParticles(_type);
                    wine.lstSalt = GameObject.FindGameObjectsWithTag("Salt");
                    break;
                case IngrType.Rose:
                    GenerateParticles(_type);
                    wine.lstRose = GameObject.FindGameObjectsWithTag("Rose");
                    TotalVol++;
                    break;
                case IngrType.Citrus:
                    GenerateParticles(_type);
                    wine.lstCitrus = GameObject.FindGameObjectsWithTag("Citrus");
                    TotalVol++;
                    break;
                case IngrType.Lemon:
                    CheckerManager.Instance.ShowWarning(_type);
                    break;
                case IngrType.Berry:
                    CheckerManager.Instance.ShowWarning(_type);
                    break;
            }
        }

        private void GenerateParticles(IngrType _type)
        {
            GameObject item = Resources.Load<GameObject>("Prefabs/WineIngredients/" + _type);
            Instantiate(item, wineSpawnPoint);
        }
    }
}