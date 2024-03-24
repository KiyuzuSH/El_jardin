using System.Collections.Generic;
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
        private GameObject[] lstIce;
        private GameObject[] lstHoney;
        private GameObject[] lstSpice;
        private GameObject[] lstSalt;
        private GameObject[] lstRose;
        private GameObject[] lstCitrus;

        private void RefreshWholeList()
        {
            lstGin = GameObject.FindGameObjectsWithTag("Gin");
            lstWhisky = GameObject.FindGameObjectsWithTag("Whisky");
            lstTequila = GameObject.FindGameObjectsWithTag("Tequila");
            lstRum = GameObject.FindGameObjectsWithTag("Rum");
            lstVodka = GameObject.FindGameObjectsWithTag("Vodka");
            lstIce = GameObject.FindGameObjectsWithTag("Ice");
            lstHoney = GameObject.FindGameObjectsWithTag("Honey");
            lstSpice = GameObject.FindGameObjectsWithTag("Spice");
            lstSalt = GameObject.FindGameObjectsWithTag("Salt");
            lstRose = GameObject.FindGameObjectsWithTag("Rose");
            lstCitrus = GameObject.FindGameObjectsWithTag("Citrus");
        }

        private void ClearAllItem()
        {
            foreach (GameObject _item in lstGin) Destroy(_item);
            foreach (GameObject _item in lstWhisky) Destroy(_item);
            foreach (GameObject _item in lstTequila) Destroy(_item);
            foreach (GameObject _item in lstRum) Destroy(_item);
            foreach (GameObject _item in lstVodka) Destroy(_item);
            foreach (GameObject _item in lstIce) Destroy(_item);
            foreach (GameObject _item in lstHoney) Destroy(_item);
            foreach (GameObject _item in lstSpice) Destroy(_item);
            foreach (GameObject _item in lstSalt) Destroy(_item);
            foreach (GameObject _item in lstRose) Destroy(_item);
            foreach (GameObject _item in lstCitrus) Destroy(_item);
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
                    break;
                case IngrType.Whisky:
                    lstWhisky = GameObject.FindGameObjectsWithTag("Whisky");
                    break;
                case IngrType.Tequila:
                    lstTequila = GameObject.FindGameObjectsWithTag("Tequila");
                    break;
                case IngrType.Rum:
                    lstRum = GameObject.FindGameObjectsWithTag("Rum");
                    break;
                case IngrType.Vodka:
                    lstVodka = GameObject.FindGameObjectsWithTag("Vodka");
                    break;
                case IngrType.Ice:
                    lstIce = GameObject.FindGameObjectsWithTag("Ice");
                    break;
                case IngrType.Honey:
                    foreach (GameObject _item in lstHoney) Destroy(_item);
                    break;
                case IngrType.Spice:
                    lstSpice = GameObject.FindGameObjectsWithTag("Spice");
                    break;
                case IngrType.Salt:
                    lstSalt = GameObject.FindGameObjectsWithTag("Salt");
                    break;
                case IngrType.Rose:
                    lstRose = GameObject.FindGameObjectsWithTag("Rose");
                    break;
                case IngrType.Citrus:
                    lstCitrus = GameObject.FindGameObjectsWithTag("Citrus");
                    break;
            }
        }
    }
}