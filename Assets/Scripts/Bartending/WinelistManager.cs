using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KiyuzuDev.ITGWDO.Bartending
{
    // public class WinelistManager : MonoBehaviour
    // {
    //     public static WinelistManager Instance { get; private set; }
    //     
    //     private void Awake()
    //     {
    //         if (Instance == null) Instance = this;
    //         else if (Instance != this)
    //         {
    //             Destroy(gameObject);
    //             Instance = this;
    //         }
    //     }
    //     
    //     private void OnDestroy()
    //     {
    //         Destroy(Instance);
    //     }
    //
    //     private static TextAsset winelistasset;
    //     private static List<string[]> winelist;
    //     
    //     private int WineIndex { get; set; }
    //     
    //     private void Start()
    //     {
    //         winelistasset = Resources.Load<TextAsset>("winelist");
    //         winelist = new List<string[]>();
    //         WineIndex = 0;
    //         List<string> temp = winelistasset.text.Split('\n').ToList();
    //         foreach (var line in temp) 
    //             winelist.Add(line.Split(','));
    //     }
    //
    //     public string[] CheckWine(int _index) => winelist[WineIndex];
    // }
}