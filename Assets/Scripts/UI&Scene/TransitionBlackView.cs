using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace KiyuzuDev.ITGWDO.UI
{
    public class TransitionBlackView : MonoBehaviour
    {
        private const string UssFade = "fade";

        private VisualElement transitionBlackImg;

        private WaitUntil waitUnitlSceneLoaded;

        public static event System.Action ShowLoadingScreen;

        private void Awake()
        {
            transitionBlackImg = GetComponent<UIDocument>()
                .rootVisualElement.Q("BlackImage");
            waitUnitlSceneLoaded = new WaitUntil(() => Core.SceneLoader.IsSceneLoaded);

            Core.SceneLoader.LoadingStarted += () =>
            {
                transitionBlackImg.AddToClassList(UssFade);
                transitionBlackImg.RegisterCallback<TransitionEndEvent>(OnFadeOutEnd);
            };
            Core.SceneLoader.LoadingCompleted += () =>
            {
                transitionBlackImg.RemoveFromClassList(UssFade);
            };
            
        }

        IEnumerator ActivateLoadedSceneCoroutine()
        {
            yield return waitUnitlSceneLoaded;
            Core.SceneLoader.ActivateLoadedScene();
        }

        private void OnFadeOutEnd(TransitionEndEvent e)
        {
            transitionBlackImg.UnregisterCallback<TransitionEndEvent>(OnFadeOutEnd);
            if (Core.SceneLoader.ShowLoadingScreen)
            {
                ShowLoadingScreen?.Invoke();
            }
            else
            {
                StartCoroutine(ActivateLoadedSceneCoroutine());
            }
        }
    }
}
