using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace KiyuzuDev.ITGWDO.Core
{
    public class SceneLoader : MonoBehaviour
    {
        public const string StartSceneKey = "StartScene";
        public const string TestSceneKey = "TestScene";

        
        public static void LoadAddressableScene(string sceneKey, 
            bool loadSceneAdditively = false,
            bool activateOnLoad = true)
        {
            LoadSceneMode loadSceneMode = 
                loadSceneAdditively ? 
                    LoadSceneMode.Additive 
                    : LoadSceneMode.Single;
            Addressables.LoadSceneAsync(sceneKey, loadSceneMode, activateOnLoad);
        }
        
        public static void LoadAddressableScene(AssetReference sceneReference, 
            bool loadSceneAdditively = false,
            bool activateOnLoad = true)
        {
            LoadSceneMode loadSceneMode = 
                loadSceneAdditively ? 
                    LoadSceneMode.Additive 
                    : LoadSceneMode.Single;
            Addressables.LoadSceneAsync(sceneReference, loadSceneMode, activateOnLoad);
        }
        
    }
}
