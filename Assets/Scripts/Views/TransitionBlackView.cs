using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace KiyuzuDev.ITGWDO.UI
{
    public class TransitionBlackView : MonoBehaviour
    {
        private const string UssFade = "fade";

        private VisualElement transitionBlackImg;

        private WaitUntil waitUntilSceneLoaded;

        public static event System.Action ShowLoadingScreen;

        private void Awake()
        {
            transitionBlackImg = GetComponent<UIDocument>()
                .rootVisualElement.Q("BlackImage");
            waitUntilSceneLoaded = new WaitUntil(() => Core.SceneLoader.IsSceneLoaded);

            Core.SceneLoader.LoadingStarted += FadeOut;
            Core.SceneLoader.LoadingAccomplished += FadeIn;
        }

        void FadeOut()
        {
            transitionBlackImg.AddToClassList(UssFade);
            transitionBlackImg.RegisterCallback<TransitionEndEvent>(OnFadeOutEnd);
        }

        void FadeIn()
        {
            transitionBlackImg.RemoveFromClassList(UssFade);
        }

        IEnumerator ActivateLoadedSceneCoroutine()
        {
            yield return waitUntilSceneLoaded;
            
            Core.SceneLoader.ActivateLoadedScene();
        }

        private void OnFadeOutEnd(TransitionEndEvent evt)
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
