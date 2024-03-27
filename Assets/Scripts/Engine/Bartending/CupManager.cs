using TMPro;
using UnityEngine;

namespace Game
{
    public class CupManager : MonoBehaviour
    {
        public static CupManager Instance { get; private set; }

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
            warningPanel.SetActive(false);
            infoPanel.SetActive(false);
            TotalVol = 0;
            CupInit();
        }

        private void OnDestroy()
        {
            Destroy(Instance);
        }

        public static WineIngr wine;
        
        public int TotalVol { get; set; }

        private void RefreshWholeList()
        {
            wine.lstGin = GameObject.FindGameObjectsWithTag("Gin");
            wine.lstWhisky = GameObject.FindGameObjectsWithTag("Whisky");
            wine.lstTequila = GameObject.FindGameObjectsWithTag("Tequila");
            wine.lstRum = GameObject.FindGameObjectsWithTag("Rum");
            wine.lstVodka = GameObject.FindGameObjectsWithTag("Vodka");
            wine.iceGO = GameObject.FindGameObjectWithTag("Ice");
            wine.lstHoney = GameObject.FindGameObjectsWithTag("Honey");
            wine.lstSpice = GameObject.FindGameObjectsWithTag("Spice");
            wine.lstSalt = GameObject.FindGameObjectsWithTag("Salt");
            wine.lstRose = GameObject.FindGameObjectsWithTag("Rose");
            wine.lstCitrus = GameObject.FindGameObjectsWithTag("Citrus");
            wine.lemonAdded = false;
            wine.berryAdded = false;
        }

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
            switch (_type)
            {
                case IngrType.Gin:
                    wine.lstGin = GameObject.FindGameObjectsWithTag("Gin");
                    TotalVol += 10;
                    break;
                case IngrType.Whisky:
                    wine.lstWhisky = GameObject.FindGameObjectsWithTag("Whisky");
                    TotalVol += 10;
                    break;
                case IngrType.Tequila:
                    wine.lstTequila = GameObject.FindGameObjectsWithTag("Tequila");
                    TotalVol += 10;
                    break;
                case IngrType.Rum:
                    wine.lstRum = GameObject.FindGameObjectsWithTag("Rum");
                    TotalVol += 10;
                    break;
                case IngrType.Vodka:
                    wine.lstVodka = GameObject.FindGameObjectsWithTag("Vodka");
                    TotalVol += 10;
                    break;
                case IngrType.Ice:
                    if (wine.iceGO) ShowWarning(_type);
                    else
                        wine.iceGO = Instantiate(
                            Resources.Load<GameObject>(
                                "Prefabs/WineIngredients/Ice"
                            ),
                            BartendingButtonManager.Instance.wineSpawnPoint
                        );
                    break;
                case IngrType.Honey:
                    wine.lstHoney = GameObject.FindGameObjectsWithTag("Honey");
                    TotalVol++;
                    break;
                case IngrType.Spice:
                    wine.lstSpice = GameObject.FindGameObjectsWithTag("Spice");
                    break;
                case IngrType.Salt:
                    wine.lstSalt = GameObject.FindGameObjectsWithTag("Salt");
                    break;
                case IngrType.Rose:
                    wine.lstRose = GameObject.FindGameObjectsWithTag("Rose");
                    TotalVol++;
                    break;
                case IngrType.Citrus:
                    wine.lstCitrus = GameObject.FindGameObjectsWithTag("Citrus");
                    TotalVol++;
                    break;
                case IngrType.Lemon:
                    if (wine.lemonAdded) ShowWarning(_type);
                    else wine.lemonAdded = true;
                    break;
                case IngrType.Berry:
                    if (wine.berryAdded) ShowWarning(_type);
                    else wine.berryAdded = true;
                    break;
            }
        }

        private void ShowWarning(IngrType _type)
        {
            if (warningPanel.activeInHierarchy)
            {
                CancelInvoke();
                warningPanel.SetActive(false);
            }
            switch (_type)
            {
                case IngrType.Ice:
                    warningPanel.GetComponentInChildren<TMP_Text>().text = "冰块已加入";
                    break;
                case IngrType.Lemon:
                    warningPanel.GetComponentInChildren<TMP_Text>().text = "柠檬装饰已准备";
                    break;
                case IngrType.Berry:
                    warningPanel.GetComponentInChildren<TMP_Text>().text = "树莓装饰已准备";
                    break;
            }
            warningPanel.SetActive(true);
            Invoke(nameof(DisableWarning),2);
        }
        
        private void DisableWarning() => warningPanel.SetActive(false);
        
        public GameObject warningPanel;
        
        public GameObject infoPanel;
        public TMP_Text totalAmount;
        public TMP_Text typeAmountL;
        public TMP_Text typeAmountR;

        private void CheckThings()
        {
            typeAmountL.text = "";
            typeAmountR.text = "";
            if (wine.lstGin != null)
                typeAmountL.text += "金酒 "+ wine.lstGin.Length * 5 +" mL\n";
            if (wine.lstWhisky != null)
                typeAmountL.text += "威士忌 "+ wine.lstWhisky.Length * 5 +" mL\n";
            if (wine.lstTequila != null)
                typeAmountL.text += "龙舌兰 "+ wine.lstTequila.Length * 5 +" mL\n";
            if (wine.lstRum != null)
                typeAmountL.text += "朗姆酒 "+ wine.lstRum.Length * 5 +" mL\n";
            if (wine.lstVodka != null)
                typeAmountL.text += "伏特加 "+ wine.lstVodka.Length * 5 +" mL\n";
            if (wine.iceGO)
                typeAmountL.text += "加冰调制";
            else
                typeAmountL.text += "去冰调制";
            if (wine.lstHoney != null)
                typeAmountR.text += "蜂蜜 "+ wine.lstHoney.Length +" mL\n";
            if (wine.lstRose != null)
                typeAmountR.text += "玫瑰精油 "+ wine.lstRose.Length +" mL\n";
            if (wine.lstCitrus != null)
                typeAmountR.text += "柑橘精油 "+ wine.lstCitrus.Length +" mL\n";
            if (wine.lstSpice != null)
                typeAmountR.text += "香料粉 "+ wine.lstSpice.Length +" g\n";
            if (wine.lstSalt != null)
                typeAmountR.text += "盐 "+ wine.lstSalt.Length +" g\n";
            if (wine.lemonAdded)
                typeAmountR.text += "柠檬装饰";
            if (wine.berryAdded)
                typeAmountR.text += " 树莓装饰";
        }
        
        private void OnMouseEnter()
        {
            totalAmount.text = TotalVol.ToString();
            CheckThings();
            if (Mathf.Approximately(Time.timeScale, 1.0f) 
                && !infoPanel.activeSelf) 
                infoPanel.SetActive(true);
        }

        private void OnMouseExit()
        {
            totalAmount.text = TotalVol.ToString();
            CheckThings();
            infoPanel.SetActive(false);
        }
    }
}