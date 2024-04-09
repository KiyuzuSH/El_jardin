using System;
using TMPro;
using UnityEngine;

namespace Game
{
    public class CheckerManager : MonoBehaviour
    {
        public static CheckerManager Instance { get; private set; }
        
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
        
        public GameObject infoPanel;
        
        public TMP_Text totalAmount;
        public TMP_Text typeAmountL;
        public TMP_Text typeAmountR;
        
        public GameObject warningPanel;
        
        public Transform wineSpawnPoint;

        private void Start()
        {
            infoPanel.SetActive(false);
            warningPanel.SetActive(false);
        }

        private void CheckThings()
        {
            typeAmountL.text = "";
            typeAmountR.text = "";
            if (WineManager.wine.lstGin != null)
                typeAmountL.text += "金酒 "+ WineManager.wine.lstGin.Length * 5 +" mL\n";
            if (WineManager.wine.lstWhisky != null)
                typeAmountL.text += "威士忌 "+ WineManager.wine.lstWhisky.Length * 5 +" mL\n";
            if (WineManager.wine.lstTequila != null)
                typeAmountL.text += "龙舌兰 "+ WineManager.wine.lstTequila.Length * 5 +" mL\n";
            if (WineManager.wine.lstRum != null)
                typeAmountL.text += "朗姆酒 "+ WineManager.wine.lstRum.Length * 5 +" mL\n";
            if (WineManager.wine.lstVodka != null)
                typeAmountL.text += "伏特加 "+ WineManager.wine.lstVodka.Length * 5 +" mL\n";
            if (WineManager.wine.iceGO)
                typeAmountL.text += "加冰调制";
            else
                typeAmountL.text += "去冰调制";
            if (WineManager.wine.lstHoney != null)
                typeAmountR.text += "蜂蜜 "+ WineManager.wine.lstHoney.Length +" mL\n";
            if (WineManager.wine.lstRose != null)
                typeAmountR.text += "玫瑰精油 "+ WineManager.wine.lstRose.Length +" mL\n";
            if (WineManager.wine.lstCitrus != null)
                typeAmountR.text += "柑橘精油 "+ WineManager.wine.lstCitrus.Length +" mL\n";
            if (WineManager.wine.lstSpice != null)
                typeAmountR.text += "香料粉 "+ WineManager.wine.lstSpice.Length +" g\n";
            if (WineManager.wine.lstSalt != null)
                typeAmountR.text += "盐 "+ WineManager.wine.lstSalt.Length +" g\n";
            if (WineManager.wine.lemonAdded)
                typeAmountR.text += "柠檬装饰";
            if (WineManager.wine.berryAdded)
                typeAmountR.text += " 树莓装饰";
        }
        
        private void OnMouseEnter()
        {
            totalAmount.text = WineManager.Instance.TotalVol.ToString();
            CheckThings();
            if (Mathf.Approximately(Time.timeScale, 1.0f) 
                && !infoPanel.activeSelf) 
                infoPanel.SetActive(true);
        }

        private void OnMouseExit()
        {
            totalAmount.text = WineManager.Instance.TotalVol.ToString();
            CheckThings();
            infoPanel.SetActive(false);
        }
        
        public void ShowWarning(IngrType _type)
        {
            if (warningPanel.activeInHierarchy)
            {
                CancelInvoke();
                warningPanel.SetActive(false);
            }
            switch (_type)
            {
                case IngrType.Lemon:
                    if (WineManager.wine.lemonAdded)
                    {
                        WineManager.wine.lemonAdded = false;
                        warningPanel.GetComponentInChildren<TMP_Text>().text = "去除柠檬装饰";
                    }
                    else
                    {
                        WineManager.wine.lemonAdded = true;
                        warningPanel.GetComponentInChildren<TMP_Text>().text = "添加柠檬装饰";
                    }
                    break;
                case IngrType.Berry:
                    if(WineManager.wine.berryAdded)
                    {
                        WineManager.wine.berryAdded = false;
                        warningPanel.GetComponentInChildren<TMP_Text>().text = "去除树莓装饰";
                    }
                    else
                    {
                        WineManager.wine.berryAdded = true;
                        warningPanel.GetComponentInChildren<TMP_Text>().text = "添加树莓装饰";
                    }
                    break;
            }
            warningPanel.SetActive(true);
            Invoke(nameof(DisableWarning),2);
        }
        
        private void DisableWarning() => warningPanel.SetActive(false);
    }
}