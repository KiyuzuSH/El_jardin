using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace KiyuzuDev.ITGWDO.View
{
    public class TransitionBlackView : MonoBehaviour
    {
        const string UssFade = "fade";

        private VisualElement rootVE;
        private VisualElement transitionBlackImg;

        private WaitUntil waitUntilSceneLoaded;

        public static event System.Action ShowLoadingScreen;

        private void Awake()
        {
            rootVE = GetComponent<UIDocument>().rootVisualElement;
            transitionBlackImg = rootVE.Q("BlackImage");
            waitUntilSceneLoaded = new WaitUntil(() => Core.SceneLoader.IsSceneLoaded);

            Core.SceneLoader.LoadingStarted += BlackFadeOut;
            Core.SceneLoader.LoadingAccomplished += BlackFadeIn;
        }
        
        void BlackFadeOut()
        {
            transitionBlackImg.AddToClassList(UssFade);
            transitionBlackImg.RegisterCallback<TransitionEndEvent>(OnFadeOutEnd);
        }

        void BlackFadeIn()
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
