using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace KiyuzuDev.ITGWDO.Core
{
    public class SceneLoader : MonoBehaviour
    {
        #region Singleton

        public static SceneLoader Instance { get; private set; }
        
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

        #endregion
        
        public const string StartSceneKey = "StartScene";
        public const string TestSceneKey = "TestScene";
        public const string AVGSceneKey = "AVGScene";
        public const string CocktailSceneKey = "CocktailScene";

        public static event System.Action LoadingStarted;//S
        public static event System.Action<float> LoadingProcess;//P
        public static event System.Action LoadingCompleted;//C
        public static event System.Action LoadingAccomplished;//A
        
        public static bool ShowLoadingScreen { get; private set; }
        public static bool IsSceneLoaded { get; private set; }
        public static SceneInstance loadedSceneInstance;

        static IEnumerator LoadAddressableSceneCoroutine(object sceneKey, bool showLoadingScreen,
            bool loadSceneAdditively, bool activateOnLoad)
        {
            LoadSceneMode loadSceneMode = 
                loadSceneAdditively ? 
                    LoadSceneMode.Additive 
                    : LoadSceneMode.Single;
            var asyncOperationHandle = Addressables.LoadSceneAsync(sceneKey, loadSceneMode, activateOnLoad);
            LoadingStarted?.Invoke();
            ShowLoadingScreen = showLoadingScreen;
            while(asyncOperationHandle.Status != AsyncOperationStatus.Succeeded)
            {
                LoadingProcess?.Invoke(asyncOperationHandle.PercentComplete);
                yield return null;
            }
            if (activateOnLoad)
            {
                LoadingAccomplished?.Invoke();
                yield break;
            }
            LoadingCompleted?.Invoke();
            IsSceneLoaded = true;
            loadedSceneInstance = asyncOperationHandle.Result;
        }

        public static void ActivateLoadedScene()
        {
            loadedSceneInstance.ActivateAsync().completed += _ =>
            {
                IsSceneLoaded = false;
                loadedSceneInstance = default;
                LoadingAccomplished?.Invoke();
            };
        }
        
        /// <summary> 加载可寻址场景 </summary>
        /// <param name="sceneKey"> 场景名字或资产 </param>
        /// <param name="showLoadingScreen"> 是否显示加载界面 </param>
        /// <param name="loadSceneAdditively"> 是否不独占地加载场景 </param>
        /// <param name="activateOnLoad"> 加载完成后是否立刻激活 </param>
        public static void LoadAddressableScene(
            object sceneKey, 
            bool showLoadingScreen = false,
            bool loadSceneAdditively = false,
            bool activateOnLoad = false)
        {
            Instance.StartCoroutine(
                LoadAddressableSceneCoroutine(
                    sceneKey, 
                    showLoadingScreen, 
                    loadSceneAdditively,
                    activateOnLoad)
                );
        }
    }
}
