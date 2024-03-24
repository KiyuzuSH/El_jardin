using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ContinueButton : MonoBehaviour
    {
        private void Start()
            => GetComponent<Button>().onClick.AddListener(ContinueGame);
        

        private void ContinueGame()
        {
            if (transform.parent.gameObject.activeSelf)
            {
                transform.parent.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
        private void OnDestroy()
            => GetComponent<Button>().onClick.RemoveAllListeners();
        
    }
}