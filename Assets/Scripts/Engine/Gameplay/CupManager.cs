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
        private GameObject[] lstRoseOil;
        private GameObject[] lstCitrusOil;

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
            lstRoseOil = GameObject.FindGameObjectsWithTag("RoseOil");
            lstCitrusOil = GameObject.FindGameObjectsWithTag("CitrusOil");
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
            foreach (GameObject _item in lstRoseOil) Destroy(_item);
            foreach (GameObject _item in lstCitrusOil) Destroy(_item);
        }

        private void CupInit()
        {
            RefreshWholeList();
            ClearAllItem();
        }
    }
}