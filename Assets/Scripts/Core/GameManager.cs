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

		#region ºÚÆÁ
		[SerializeField] private UnityEngine.UI.MaskableGraphic blackScreen;

        private float BlackScreenOpacity {
            get => blackScreen.color.a;
            set {
                var color = Color.black;
                color.a = value;
                blackScreen.color = color;
            }
        }

        public Coroutine FadeBlackScreenOpacity(float opacity, float duration = .5f) {
            return StartCoroutine(FadeBlackScreenOpacityCoroutine(opacity, duration));
		}

        private IEnumerator FadeBlackScreenOpacityCoroutine(float opacity, float duration = .5f) {
            float startOpacity = BlackScreenOpacity;
            for(float startTime = Time.time, t; (t = (Time.time - startTime) / duration) < 1; ) {
                BlackScreenOpacity = Mathf.Lerp(startOpacity, opacity, t);
                Debug.Log(BlackScreenOpacity);
                yield return new WaitForEndOfFrame();
			}
            BlackScreenOpacity = opacity;

		}
		#endregion
	}
}
