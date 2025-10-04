using UnityEngine;
using UnityEngine.InputSystem;

namespace ldjam_58
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Input Settings")] [SerializeField]
        private InputActionAsset playerControls;
        
        private InputAction _clickAction;
        private InputActionMap _playerActionMap;

        private PlayerWeapons _currentWeapon = PlayerWeapons.None;

        [Header("Souls Layer Settings")] [SerializeField]
        private LayerMask soulsLayer = 3;

        private Camera _mainCamera;
        
        [SerializeField] private GameManagerChannel gameManagerChannel;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Awake()
        {
            _mainCamera = Camera.main;

            if (playerControls is null)
            {
                throw new MissingComponentException("Input Action Asset is not assigned in the inspector");
            }
            
            if (gameManagerChannel is null)
            {
                throw new MissingComponentException("GameManagerChannel is not assigned in the inspector");
            }

            _playerActionMap = playerControls.FindActionMap("Player");
            if (_playerActionMap != null)
            {
                _clickAction = _playerActionMap.FindAction("Click");
            }
            else
            {
                Debug.Log("Could not find 'Player' action map in Input Action Asset!");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_clickAction.triggered)
            {
                HandleClick();
            }
        }

        private void HandleClick()
        {
            // Convert mouse position to world position for 2D
            var mousePosition = Mouse.current.position.ReadValue();
            var worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0;

            var soulsHit = _currentWeapon switch
            {
                PlayerWeapons.None => HandleWeaponNone(worldPosition),
                _ => null
            };
            
            CollectSouls(soulsHit);
        }

        private Collider2D[] HandleWeaponNone(Vector2 worldPos)
        {
            var hit = Physics2D.OverlapCircleAll(worldPos, 0.01f, soulsLayer);
            return hit;
        }

        private void CollectSouls(Collider2D[] soulsHit)
        {
            foreach (var soul in soulsHit)
            {
                var soulComponent = soul.GetComponent<SoulController>();
                soulComponent?.OnColelcted();
            }
            gameManagerChannel.AddScore((uint)soulsHit.Length);
        }
    }
}