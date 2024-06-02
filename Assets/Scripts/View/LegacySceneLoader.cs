using System.Collections;
using KiyuzuDev.ITGWDO.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KiyuzuDev.ITGWDO.View {
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

        public void LoadScene(string sceneKey)
        {
            Instance.StartCoroutine(LoadSceneCoroutine(sceneKey));
        }
        
        IEnumerator LoadSceneCoroutine(string sceneKey)
        {
            var loadingOperation = SceneManager.LoadSceneAsync(sceneKey);

            loadingOperation.allowSceneActivation = false;
			yield return Core.GameManager.Instance.FadeBlackScreenOpacity(1);

            loadingOperation.allowSceneActivation = true;
			yield return Core.GameManager.Instance.FadeBlackScreenOpacity(0);
            if(GlobalDataManager.Instance.NextLineID > 1001)
                GlobalDataManager.Instance.LoadLineOfDialogue(GlobalDataManager.Instance.NextLineID);
		}
    }
}
