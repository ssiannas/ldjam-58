using System;
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

        [SerializeField] private PlayerWeapons _currentWeapon = PlayerWeapons.None;

        [Header("Souls Layer Settings")] [SerializeField]
        private LayerMask soulsLayer = 3;

        private Camera _mainCamera;

        private bool _holding = false;

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

            gameManagerChannel.OnChangePlayerWeaponRequested += SetWeapon;
            
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
            if(_currentWeapon is PlayerWeapons.Unalivatron && _holding)
            {
                HandleClick();
            }
        }


        private void OnEnable()
        {
            _clickAction.Enable();
            _clickAction.performed += OnClickPerformed;
            _clickAction.canceled += OnClickReleased;
        }

        private void OnDisable()
        {
            _clickAction.performed -= OnClickPerformed;
            _clickAction.canceled -= OnClickReleased;
            _clickAction.Disable();
        }

        private void OnClickPerformed(InputAction.CallbackContext ctx)
        {
         
            if (_currentWeapon is not PlayerWeapons.Unalivatron) { HandleClick();};
            _holding = true;


        }

        private void OnClickReleased(InputAction.CallbackContext ctx)
        {

            _holding = false;
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
                PlayerWeapons.Net => HandleWeaponNet(worldPosition),
                PlayerWeapons.Scythe => HandleWeaponScythe(worldPosition),
                PlayerWeapons.Unalivatron => Array.Empty<Collider2D>(),
            };
            
            CollectSouls(soulsHit);
        }
        
        public void SetWeapon(PlayerWeapons newWeapon)
        {
            _currentWeapon = newWeapon;
        }

        private Collider2D[] HandleWeaponNone(Vector2 worldPos)
        {
            return Physics2D.OverlapCircleAll(worldPos, 0.01f, soulsLayer);
        }

        private Collider2D[] HandleWeaponNet(Vector2 worldPos)
        {
            Vector2 xOffset = new Vector2(1.5f, 0.0f);
            Vector2 leftPonit = worldPos - xOffset;
            Vector2 rightPonit = worldPos + xOffset;
            float distance = Vector2.Distance(leftPonit, rightPonit);
            float angle = Mathf.Atan2(rightPonit.y - leftPonit.y, rightPonit.x - leftPonit.x) * Mathf.Rad2Deg;

            // Very small thickness → almost like a line
            Collider2D[] hit = Physics2D.OverlapCapsuleAll(worldPos, new Vector2(distance, 0.01f), CapsuleDirection2D.Horizontal, angle);

            return hit;
        }

        private Collider2D[] HandleWeaponScythe(Vector2 worldPos)
        {
            var hit = Physics2D.OverlapCircleAll(worldPos, 3.0f, soulsLayer);
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