using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace ldjam_58
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Input Settings")] [SerializeField]
        private InputActionAsset playerControls;
        
        private InputAction _clickAction;
        private InputActionMap _playerActionMap;
        [SerializeField] private Weapon _currentWeapon;
        [SerializeField] private CursorController _cursorController;
        
        [Header("Souls Layer Settings")] [SerializeField]
        private LayerMask soulsLayer;
        private Camera _mainCamera;
        private bool _holding = false;

        private readonly Collider2D[] _soulsHit = new Collider2D[500];
        private ContactFilter2D _soulsContactFilter = ContactFilter2D.noFilter;

        [SerializeField] private GameManagerChannel gameManagerChannel;
        [SerializeField] private GameState gameState;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Awake()
        {
            _mainCamera = Camera.main;
            _cursorController = GetComponent<CursorController>();
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

            if (_currentWeapon is null)
            {
                throw new MissingComponentException("Current Weapon is not assigned in the inspector. Add default weapon" +
                                                    "option");
            }
            soulsLayer = LayerMask.NameToLayer("Souls");
            _soulsContactFilter.SetLayerMask(1 << soulsLayer);
        }

        // Update is called once per frame
        void Update()
        {
            if(_currentWeapon.WeaponType is PlayerWeapons.Unalivatron && _holding)
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

            _cursorController.OnClick();
            if (_currentWeapon.WeaponType is not PlayerWeapons.Unalivatron) { HandleClick();};
            _holding = true;
        }

        private void OnClickReleased(InputAction.CallbackContext ctx)
        {
            _cursorController.OnRelease();
            _holding = false;
        }


        private void HandleClick()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            
            // Convert mouse position to world position for 2D
            var mousePosition = Mouse.current.position.ReadValue();
            var worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0;

            if (_currentWeapon.SwooshPrefab != null)
            {
                Instantiate(_currentWeapon.SwooshPrefab, worldPosition, Quaternion.identity);
            }
            
            var soulsHit = _currentWeapon.WeaponType switch
            {
                PlayerWeapons.None => HandleWeaponNone(worldPosition),
                PlayerWeapons.Net => HandleWeaponNet(worldPosition),
                PlayerWeapons.Scythe => HandleWeaponScythe(worldPosition),
                PlayerWeapons.Unalivatron => HandleWeaponUnalivatron(worldPosition),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            CollectSouls(soulsHit);
        }
        
        public void SetWeapon(Weapon newWeapon)
        {
            _cursorController.SetCursor(newWeapon.WeaponType);
            _currentWeapon = newWeapon;
        }

        private int HandleWeaponNone(Vector2 worldPos)
        {
            return Physics2D.OverlapCircle(worldPos, 0.3f, _soulsContactFilter, _soulsHit ); 
        }
        
        private int HandleWeaponNet(Vector2 worldPos)
        {
            Vector2 xOffset = new Vector2(1.5f, 0.0f);
            Vector2 leftPonit = worldPos - xOffset;
            Vector2 rightPonit = worldPos + xOffset;
            float distance = Vector2.Distance(leftPonit, rightPonit);
            float angle = Mathf.Atan2(rightPonit.y - leftPonit.y, rightPonit.x - leftPonit.x) * Mathf.Rad2Deg;

            // Very small thickness → almost like a line
            return  Physics2D.OverlapCapsule(worldPos, new Vector2(distance, 0.01f), CapsuleDirection2D.Horizontal, angle, _soulsContactFilter, _soulsHit);
        }

        private int HandleWeaponScythe(Vector2 worldPos)
        {
            return Physics2D.OverlapCircle(worldPos, 3.0f, _soulsContactFilter, _soulsHit);
        }

        private int HandleWeaponUnalivatron(Vector2 worldPos)
        {
            return Physics2D.OverlapCircle(worldPos, 0.1f, _soulsContactFilter, _soulsHit);
        }

        private void CollectSouls(int numHits)
        {
            for (var index = 0; index < numHits; index++)
            {
                var soulComponent = _soulsHit[index].GetComponent<SoulController>();
                soulComponent?.OnCollected();
            }
        }
    }
}