using UnityEngine;
using UnityEngine.AddressableAssets;

namespace KiyuzuDev.ITGWDO.Core
{
    public class GameManager : MonoBehaviour
    {
        public const string ThisPrefabKey = "GameManager";
        private void Awake() => DontDestroyOnLoad(this);
        

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void InstantiateThis()
        {
            Addressables.InstantiateAsync(ThisPrefabKey).Completed 
                += 
                handle => 
                    DontDestroyOnLoad(handle.Result);
        }
    }
}
