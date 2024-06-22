using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KiyuzuDev.ITGWDO.Core
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

        public void LoadScene(int sceneKey)
        {
            Instance.StartCoroutine(LoadSceneCoroutine(sceneKey));
        }
        
        IEnumerator LoadSceneCoroutine(int sceneKey)
        {
            var loadingOperation = SceneManager.LoadSceneAsync(sceneKey);

            loadingOperation.allowSceneActivation = false;
			yield return GameManager.Instance.FadeBlackScreenOpacity(1);

            loadingOperation.allowSceneActivation = true;
			yield return GameManager.Instance.FadeBlackScreenOpacity(0);
            // if(GlobalDataManager.Instance.NextLineID > 1001)
            //     DialogueManager.Instance.SetLineOfDialogue(GlobalDataManager.Instance.NextLineID);
		}
    }
}
