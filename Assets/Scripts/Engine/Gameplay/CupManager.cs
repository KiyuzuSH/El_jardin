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
            infoPanel.SetActive(false);
            TotalVol = 0;
            CupInit();
        }

        private void OnDestroy()
        {
            Destroy(Instance);
        }

        private GameObject[] lstGin;
        private GameObject[] lstWhisky;
        private GameObject[] lstTequila;
        private GameObject[] lstRum;
        private GameObject[] lstVodka;
        private GameObject[] lstHoney;
        private GameObject[] lstSpice;
        private GameObject[] lstSalt;
        private GameObject[] lstRose;
        private GameObject[] lstCitrus;

        public int TotalVol { get; set; }

        private void RefreshWholeList()
        {
            lstGin = GameObject.FindGameObjectsWithTag("Gin");
            lstWhisky = GameObject.FindGameObjectsWithTag("Whisky");
            lstTequila = GameObject.FindGameObjectsWithTag("Tequila");
            lstRum = GameObject.FindGameObjectsWithTag("Rum");
            lstVodka = GameObject.FindGameObjectsWithTag("Vodka");
            lstHoney = GameObject.FindGameObjectsWithTag("Honey");
            lstSpice = GameObject.FindGameObjectsWithTag("Spice");
            lstSalt = GameObject.FindGameObjectsWithTag("Salt");
            lstRose = GameObject.FindGameObjectsWithTag("Rose");
            lstCitrus = GameObject.FindGameObjectsWithTag("Citrus");
        }

        private void ClearAllItem()
        {
            if (lstGin != null)
                foreach (GameObject _item in lstGin)
                    Destroy(_item);
            if (lstWhisky != null)
                foreach (GameObject _item in lstWhisky)
                    Destroy(_item);
            if (lstTequila != null)
                foreach (GameObject _item in lstTequila)
                    Destroy(_item);
            if (lstRum != null)
                foreach (GameObject _item in lstRum)
                    Destroy(_item);
            if (lstVodka != null)
                foreach (GameObject _item in lstVodka)
                    Destroy(_item);
            if (lstHoney != null)
                foreach (GameObject _item in lstHoney)
                    Destroy(_item);
            if (lstSpice != null)
                foreach (GameObject _item in lstSpice)
                    Destroy(_item);
            if (lstSalt != null)
                foreach (GameObject _item in lstSalt)
                    Destroy(_item);
            if (lstRose != null)
                foreach (GameObject _item in lstRose)
                    Destroy(_item);
            if (lstCitrus != null)
                foreach (GameObject _item in lstCitrus)
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
                    lstGin = GameObject.FindGameObjectsWithTag("Gin");
                    TotalVol += 10;
                    break;
                case IngrType.Whisky:
                    lstWhisky = GameObject.FindGameObjectsWithTag("Whisky");
                    TotalVol += 10;
                    break;
                case IngrType.Tequila:
                    lstTequila = GameObject.FindGameObjectsWithTag("Tequila");
                    TotalVol += 10;
                    break;
                case IngrType.Rum:
                    lstRum = GameObject.FindGameObjectsWithTag("Rum");
                    TotalVol += 10;
                    break;
                case IngrType.Vodka:
                    lstVodka = GameObject.FindGameObjectsWithTag("Vodka");
                    TotalVol += 10;
                    break;
                case IngrType.Ice:
                    // TODO: Deal with the ICE
                    break;
                case IngrType.Honey:
                    lstHoney = GameObject.FindGameObjectsWithTag("Honey");
                    TotalVol++;
                    break;
                case IngrType.Spice:
                    lstSpice = GameObject.FindGameObjectsWithTag("Spice");
                    break;
                case IngrType.Salt:
                    lstSalt = GameObject.FindGameObjectsWithTag("Salt");
                    break;
                case IngrType.Rose:
                    lstRose = GameObject.FindGameObjectsWithTag("Rose");
                    TotalVol++;
                    break;
                case IngrType.Citrus:
                    lstCitrus = GameObject.FindGameObjectsWithTag("Citrus");
                    TotalVol++;
                    break;
            }
        }
        
        public GameObject infoPanel;
        public TMP_Text totalAmount;
        public TMP_Text typeAmountL;
        public TMP_Text typeAmountR;

        private void CheckThings()
        {
            typeAmountL.text = "";
            typeAmountR.text = "";
            // if (lstIce != null) typeAmount.text += "加冰调制\n"; else typeAmount.text += "去冰调制\n";
            if (lstGin != null) typeAmountL.text += "金酒 "+ lstGin.Length * 5 +" mL\n";
            if (lstWhisky != null) typeAmountL.text += "威士忌 "+ lstWhisky.Length * 5 +" mL\n";
            if (lstTequila != null) typeAmountL.text += "龙舌兰 "+ lstTequila.Length * 5 +" mL\n";
            if (lstRum != null) typeAmountL.text += "朗姆酒 "+ lstRum.Length * 5 +" mL\n";
            if (lstVodka != null) typeAmountL.text += "伏特加 "+ lstVodka.Length * 5 +" mL";
            if (lstHoney != null) typeAmountR.text += "蜂蜜 "+ lstHoney.Length +" mL\n";
            if (lstRose != null) typeAmountR.text += "玫瑰精油 "+ lstRose.Length +" mL\n";
            if (lstCitrus != null) typeAmountR.text += "柑橘精油 "+ lstCitrus.Length +" mL\n";
            if (lstSpice != null) typeAmountR.text += "香料粉 "+ lstSpice.Length +" g\n";
            if (lstSalt != null) typeAmountR.text += "盐 "+ lstSalt.Length +" g";
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