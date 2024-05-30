using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace KiyuzuDev.ITGWDO.View
{
    public class GameStartView : MonoBehaviour
    {
        #region Singleton

        public static GameStartView Instance { get; private set; }
        
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
        
        [SerializeField] private Button newGameButton;
        
        private void OnEnable()
        {
            newGameButton.onClick.AddListener(StartGame);
        }

        private void OnDisable()
        {
            newGameButton.onClick.RemoveAllListeners();
        }
        
        [SerializeField] private AssetReference StartScene;
        [SerializeField] private bool debugMode = true;
        
        private void StartGame()
        {
            newGameButton.interactable = false;
            // TODO: disable all player's input
            if (debugMode)
            {
                LegacySceneLoader.Instance.LoadScene("TestScene");
            }
            else
            {
                // TODO: Initialization the Game
                // Core.SceneLoader.LoadAddressableScene(StartedScene);
                LegacySceneLoader.Instance.LoadScene("AVGScene");
            }
            Debug.Log("Triggered Start Button");
        }
    }
}
