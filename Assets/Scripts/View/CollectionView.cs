using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace KiyuzuDev.ITGWDO.View
{
    public class CollectionView : MonoBehaviour
    {
        [SerializeField] private InputActionAsset actions;
        private InputActionMap mapGlobal;
        private Canvas thisCanvas;
        [SerializeField] private Image CollectionPanel;
        [SerializeField] private Image PauseBoard;

        private void Awake()
        {
            mapGlobal = actions.FindActionMap("Global");
            mapGlobal.FindAction("CollectionPanel").performed += OnCollectionPanel;
            thisCanvas = GetComponent<Canvas>();
        }

        private void OnEnable()
        {
            mapGlobal.Enable();
        }

        private void OnDisable()
        {
            mapGlobal.Disable();
        }

        void OnCollectionPanel(InputAction.CallbackContext context)
        {
            // Debug.Log(context);
            thisCanvas.enabled = !thisCanvas.enabled;
        }
    }
}
