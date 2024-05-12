using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace KiyuzuDev.ITGWDO.UI
{
    public class GameStartView : MonoBehaviour
    {
        [SerializeField] private AssetReference GameMainScene;
        [SerializeField] private bool debugMode = true;
        private VisualElement rootVE;
        
        private Button newGameButton;
        
        private void Awake()
        {
            rootVE = GetComponent<UIDocument>().rootVisualElement;
            newGameButton = rootVE.Q<Button>("StartButton");
        }

        private void OnEnable()
        {
            newGameButton.clicked += StartGame;
        }

        private void OnDisable()
        {
            newGameButton.clicked -= StartGame;
        }

        private void StartGame()
        {
            // TODO: disable all player's input
            if (debugMode)
            {
                Core.SceneLoader.LoadAddressableScene(Core.SceneLoader.TestSceneKey);
            }
            else
            {
                // TODO: Initialization the Game
                Core.SceneLoader.LoadAddressableScene(GameMainScene);
            }
        }
    }
}
