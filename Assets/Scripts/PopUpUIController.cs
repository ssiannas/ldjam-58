using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ldjam_58
{

    public class PopUpUIController : MonoBehaviour
    {
        [SerializeField] private GameObject popupPanel;
        [SerializeField] private Button openButton;
        [SerializeField] private Button closeButton;

        void Awake()
        {
            if (popupPanel) popupPanel.SetActive(false);
            if (openButton) openButton.onClick.AddListener(Show);
            if (closeButton) closeButton.onClick.AddListener(Hide);
        }

        public void Show() => popupPanel?.SetActive(true);
        public void Hide() => popupPanel?.SetActive(false);

        void Update()
        {
            if (popupPanel && popupPanel.activeSelf && Keyboard.current.escapeKey.wasPressedThisFrame)
                Hide();
        }
    }

}