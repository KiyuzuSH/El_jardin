using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace KiyuzuDev.ITGWDO.View
{
    public class LegacySceneLoader : MonoBehaviour
    {
        #region Singleton

        public static LegacySceneLoader Instance { get; private set; }

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

        [SerializeField] private CanvasGroup blackImg;

        public void LoadScene(string sceneKey)
        {
            Instance.StartCoroutine(LoadSceneCoroutine(sceneKey));
        }
        
        IEnumerator LoadSceneCoroutine(string sceneKey)
        {
            var loadingOperation = SceneManager.LoadSceneAsync(sceneKey);
            loadingOperation.allowSceneActivation = false;
            
            blackImg.alpha = 0f;

            while (blackImg.alpha < 1f)
            {
                blackImg.alpha = Mathf.Clamp01(blackImg.alpha + Time.unscaledDeltaTime / 2f);

                yield return null;
            }

            blackImg.alpha = 1f;
            loadingOperation.allowSceneActivation = true;
            
            while (blackImg.alpha > 0f)
            {
                blackImg.alpha = Mathf.Clamp01(blackImg.alpha - Time.unscaledDeltaTime / 2f);

                yield return null;
            }
            
            blackImg.alpha = 0f;

        }
    }
}
