using UnityEngine;

namespace KiyuzuDev.ITGWDO.Bartending
{
    public struct WineIngr
    {
        public GameObject[] lstGin;
        public GameObject[] lstWhisky;
        public GameObject[] lstTequila;
        public GameObject[] lstRum;
        public GameObject[] lstVodka;
        public GameObject iceGO;
        public GameObject[] lstHoney;
        public GameObject[] lstSpice;
        public GameObject[] lstSalt;
        public GameObject[] lstRose;
        public GameObject[] lstCitrus;
        public bool lemonAdded;
        public bool berryAdded;
    }

    public enum IngrType
    {
        Gin = 0,
        Whisky = 1,
        Tequila = 2,
        Rum = 3,
        Vodka = 4,
        Ice = -1,
        Lemon = -2,
        Honey = 5,
        Berry = -3,
        Spice = 16,
        Salt = 17,
        Rose = 6,
        Citrus = 7,
    }
}