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

		#region ����
        /// <summary>The UI canvas group for showing the black screen.</summary>
		[SerializeField] private CanvasGroup blackScreen;

        /// <summary>The internal interface for the black screen UI's opacity.</summary>
        /// <remarks>Should not be exposed as public.</remarks>
        private float BlackScreenOpacity {
            get => blackScreen.alpha;
            set {
                blackScreen.alpha = value;
            }
        }

        /// <summary>
        /// Make the game screen fade to a certain alpha of black.
        /// </summary>
        /// <returns>The coroutine performing the fading.</returns>
        public Coroutine FadeBlackScreenOpacity(float opacity, float duration = .5f) {
            return StartCoroutine(FadeBlackScreenOpacityCoroutine(opacity, duration));
		}

        /// <summary>
        /// The internal coroutine for fading the black screen's opacity.
        /// </summary>
        private IEnumerator FadeBlackScreenOpacityCoroutine(float opacity, float duration = .5f) {
            float startOpacity = BlackScreenOpacity;
            for(float startTime = Time.time, t; (t = (Time.time - startTime) / duration) < 1; ) {
                BlackScreenOpacity = Mathf.Lerp(startOpacity, opacity, t);
                yield return new WaitForEndOfFrame();
			}
            BlackScreenOpacity = opacity;

            blackScreen.alpha = opacity;
        }

        #endregion
	}
}
