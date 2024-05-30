using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace KiyuzuDev.ITGWDO.View
{
    public class TransitionBlackView : MonoBehaviour
    {
        // #region Singleton
        //
        // public static TransitionBlackView Instance { get; private set; }
        //
        // private void Awake()
        // {
        //     if (Instance == null) Instance = this;
        //     else if (Instance != this)
        //     {
        //         Destroy(gameObject);
        //         Instance = this;
        //     }
        // }
        //
        // private void OnDestroy()
        // {
        //     Destroy(Instance);
        // }
        //
        // #endregion
        //
        // const string UssFade = "fade";
        //
        // private VisualElement rootVE;
        // private VisualElement transitionBlackImg;
        //
        // private WaitUntil waitUntilSceneLoaded;
        //
        // public static event System.Action ShowLoadingScreen;
        //
        // private void OnEnable()
        // {
        //     rootVE = GetComponent<UIDocument>().rootVisualElement;
        //     transitionBlackImg = rootVE.Q("BlackImage");
        //     waitUntilSceneLoaded = new WaitUntil(() => Core.SceneLoader.IsSceneLoaded);
        //
        //     Core.SceneLoader.LoadingStarted += BlackFadeOut;
        //     Core.SceneLoader.LoadingAccomplished += BlackFadeIn;
        // }
        //
        // void BlackFadeOut()
        // {
        //     transitionBlackImg.AddToClassList(UssFade);
        //     transitionBlackImg.RegisterCallback<TransitionEndEvent>(OnFadeOutEnd);
        // }
        //
        // void BlackFadeIn()
        // {
        //     transitionBlackImg.RemoveFromClassList(UssFade);
        // }
        //
        // IEnumerator ActivateLoadedSceneCoroutine()
        // {
        //     yield return waitUntilSceneLoaded;
        //     
        //     Core.SceneLoader.ActivateLoadedScene();
        // }
        //
        // private void OnFadeOutEnd(TransitionEndEvent evt)
        // {
        //     transitionBlackImg.UnregisterCallback<TransitionEndEvent>(OnFadeOutEnd);
        //     if (Core.SceneLoader.ShowLoadingScreen)
        //     {
        //         ShowLoadingScreen?.Invoke();
        //     }
        //     else
        //     {
        //         StartCoroutine(ActivateLoadedSceneCoroutine());
        //     }
        // }
    }
}
