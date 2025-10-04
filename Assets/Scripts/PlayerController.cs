using UnityEngine;
using UnityEngine.InputSystem;

namespace ldjam_58
{
    
    public class PlayerController : MonoBehaviour
    {
        [Header("Input Settings")]
        [SerializeField] private InputActionAsset playerControls;
        private InputAction clickAction;
        private InputActionMap playerActionMap;

        [Header("Souls Layer Settings")]
        [SerializeField] private LayerMask soulsLayer = 3;

        private Camera mainCamera;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            mainCamera = Camera.main;
            if (playerControls != null)
            {
                playerActionMap = playerControls.FindActionMap("Player");
                if (playerActionMap != null)
                {
                    clickAction = playerActionMap.FindAction("Click");
                }
                else
                {
                    Debug.Log("Could not find 'Player' action map in Input Action Asset!");
                }
            }
            else
            {
                Debug.Log("Player Controls Input Action Asset is not assigned!");
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (clickAction.triggered)
            {
                HandleClick();
            }
        }

        private void HandleClick()
        {
            // Convert mouse position to world position for 2D
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0;

            // Cast a point to check what's at this position in 2D
            Collider2D hit = Physics2D.OverlapPoint(worldPosition, soulsLayer);
            Debug.Log($"Click at world position: {worldPosition}");

            if (hit != null)
            {
                // Check if we hit an NPC
                SoulController soul = hit.GetComponent<SoulController>();
                if (soul != null)
                {
                    Debug.Log("Soul Clicked!");
                }
                else
                {
                    Debug.Log($"Hit {hit.name} but it doesn't have NPCController component");
                }
            }
            else
            {
                Debug.Log("Click didn't hit any NPC");
            }
        }
    }
}
