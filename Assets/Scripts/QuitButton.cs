using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class QuitButton : MonoBehaviour
    {
        private void Start()
            => GetComponent<Button>().onClick.AddListener(QuitApp);
        

        private void QuitApp()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        private void OnDestroy()
            => GetComponent<Button>().onClick.RemoveAllListeners();
        
    }
}