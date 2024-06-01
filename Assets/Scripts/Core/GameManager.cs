using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections;

namespace KiyuzuDev.ITGWDO.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        const string ThisPrefabKey = "GameManager";
        private void Awake() {
			DontDestroyOnLoad(this);
			Instance = this;
		}
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void InstantiateThis()
        {
            Addressables.InstantiateAsync(ThisPrefabKey).Completed 
                += 
                handle => 
                    DontDestroyOnLoad(handle.Result);
        }

		#region 黑屏
        
		[SerializeField] private CanvasGroup blackScreen;

        public Coroutine FadeBlackScreenOpacity(float opacity, float duration = .5f) {
            return StartCoroutine(FadeBlackScreenOpacityCoroutine(opacity, duration));
		}

        private IEnumerator FadeBlackScreenOpacityCoroutine(float opacity, float duration = .5f)
        {
            float startOpacity = blackScreen.alpha;
            for (float startTime = Time.unscaledTime, t; (t = (Time.unscaledTime - startTime) / duration) < 1;)
            {
                blackScreen.alpha = Mathf.Lerp(startOpacity, opacity, t);
                yield return null;
            }

            blackScreen.alpha = opacity;
        }

        #endregion
	}
}
