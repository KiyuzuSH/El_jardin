using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace KiyuzuDev.ITGWDO.UI
{
    public class TransitionBlackView : MonoBehaviour
    {
        private const string UssFade = "fade";

        private VisualElement transitionBlackImg;

        private WaitUntil waitUnitlSceneLoaded;

        private void Awake()
        {
            transitionBlackImg = GetComponent<UIDocument>().rootVisualElement.Q("BlackImage");
            // waitUnitlSceneLoaded = new WaitUntil(() => SceneLoader.IsSceneLoaded);
        }
    }
}
