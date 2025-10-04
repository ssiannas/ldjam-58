using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ldjam_58
{

    public class PopUpUIController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup popupPanel;
        [SerializeField] private Button openButton;
        [SerializeField] private Button closeButton;
        private bool _isVisible;
        [SerializeField] private UIChannel uiChannel;
        
        void Awake()
        {
            if (popupPanel) Hide();
            if (openButton) openButton.onClick.AddListener(Show);
            if (closeButton) closeButton.onClick.AddListener(Hide);
        }

        public void Show()
        {
            popupPanel.alpha = 1;
            popupPanel.blocksRaycasts = true;
            popupPanel.interactable = true;
            _isVisible = true;
            uiChannel.OnUpgradeReset();
        }

        public void Hide() 
        {
            popupPanel.alpha = 0;
            popupPanel.blocksRaycasts = false;
            popupPanel.interactable = false;
            _isVisible = false;
        }

        void Update()
        {
            if (popupPanel && _isVisible && Keyboard.current.escapeKey.wasPressedThisFrame)
                Hide();
        }
    }

}