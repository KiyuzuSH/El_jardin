using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace KiyuzuDev.ITGWDO.View
{
    public class TransitionBlackView : MonoBehaviour
    {
        #region Singleton

        public static TransitionBlackView Instance { get; private set; }

        private void OnDestroy()
        {
            Destroy(Instance);
        }

        #endregion
        
        const string UssFade = "fade";
        const string UssTransparent = "transparent";
        const string UssETrans = "easedtrans";

        private VisualElement rootVE;
        private VisualElement transitionBlackImg;
        private VisualElement collectionPanel;
        private VisualElement collectionGetPanel;

        private WaitUntil waitUntilSceneLoaded;

        public static event System.Action ShowLoadingScreen;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this)
            {
                Destroy(gameObject);
                Instance = this;
            }
            
            rootVE = GetComponent<UIDocument>().rootVisualElement;
            transitionBlackImg = rootVE.Q("BlackImage");
            collectionPanel = rootVE.Q("CollectionPic");
            collectionPanel.focusable = true;
            collectionPanel.RegisterCallback<KeyDownEvent>(OnCallingCollection);
            collectionGetPanel = rootVE.Q("CollectionGetContainer");
            waitUntilSceneLoaded = new WaitUntil(() => Core.SceneLoader.IsSceneLoaded);

            Core.SceneLoader.LoadingStarted += BlackFadeOut;
            Core.SceneLoader.LoadingAccomplished += BlackFadeIn;
        }

        void OnCallingCollection(KeyDownEvent keyDownEvent)
        {
            if (keyDownEvent.keyCode == KeyCode.E)
            {
                if (SceneManager.GetActiveScene().name == "AVGScene" ||
                    SceneManager.GetActiveScene().name == "CocktailScene")
                {
                    if (collectionPanel.ClassListContains(UssTransparent))
                        collectionPanel.RemoveFromClassList(UssTransparent);
                    else
                        collectionPanel.AddToClassList(UssTransparent);
                }
            }
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

        public void CollectionFadeOut()
        {
            collectionGetPanel.AddToClassList(UssETrans);
        }

        public void CollectionFadeIn()
        {
            collectionGetPanel.RemoveFromClassList(UssETrans);
        }
    }
}
