using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ldjam_58
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CursorController : MonoBehaviour
    {
        [Header("Cursor Textures")]
        [SerializeField] private Sprite skellyHandDefault;
        [SerializeField] private Sprite skellyHandClicked;
        [SerializeField] private Sprite netHandDefault;
        [SerializeField] private Sprite netHandClicked;
        [SerializeField] private Sprite scytheHandDefault;
        [SerializeField] private Sprite scytheHandClicked;
        [SerializeField] private Sprite unalivatronHandDefault;
        [SerializeField] private Sprite unalivatronHandClicked;
        
        private Sprite cursorTexture;
        private Sprite cursorTextureClicked;
        
        private SpriteRenderer _spriteRenderer;
        private Camera _cursorCamera;
        
        public Vector2 hotspot = Vector2.zero;
        private Camera mainCamera;
        private Image cursorUI;
        
        [SerializeField] private UIChannel uiChannel;

        private float _scaleFactor = 0.55f;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (skellyHandDefault is null || 
                skellyHandClicked is null || 
                netHandDefault is null || 
                netHandClicked is null || 
                scytheHandDefault is null || 
                scytheHandClicked is null || 
                unalivatronHandDefault is null || 
                unalivatronHandClicked is null)
            {
                throw new MissingComponentException("One or more cursor textures are not assigned in the inspector");
            }
            if (uiChannel is null)
            {
                throw new MissingComponentException("UI Channel is not assigned in the inspector");
            }
            
            cursorTexture = skellyHandDefault;
            cursorTextureClicked = skellyHandClicked;
            _spriteRenderer.sprite = cursorTexture;
            Cursor.visible = false;
        }
        
        void Awake()
        {
            transform.localScale = new Vector3(0.2f, 0.2f, 1f);
            mainCamera = Camera.main;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.enabled = false;
            uiChannel.OnForceUpdateCursor += ScaleCursorUI;
            CreateCursorUI(); 
        }
        void CreateCursorUI()
        {
            // Create canvas
            GameObject canvasObj = new GameObject("CursorCanvas");
            Canvas canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 10000;
        
            // Create cursor
            GameObject cursorObj = new GameObject("Cursor");
            cursorObj.transform.SetParent(canvasObj.transform);
            cursorUI = cursorObj.AddComponent<Image>();
            cursorUI.raycastTarget = false;
        
            // Initial sprite
            if (_spriteRenderer != null)
            {
                cursorUI.sprite = _spriteRenderer.sprite;
            }
            UpdatePivotAndSizeFromSprite(_spriteRenderer.sprite);
            ScaleCursorUI(uiChannel.GetCanvasScaleFactor());
        } 
  
        private void ScaleCursorUI(float canvasScaleFactor)
        {
            if (canvasScaleFactor < 1) return;
            var newScaleFactor = _scaleFactor / canvasScaleFactor;
            cursorUI.transform.localScale = newScaleFactor * (Vector3.one / canvasScaleFactor);
        }

        
        public void OnClick()
        {
            _spriteRenderer.sprite = cursorTextureClicked;
        }
        
        public void OnRelease()
        {
            _spriteRenderer.sprite = cursorTexture;
        }

        private void Update()
        {
            cursorUI.sprite = _spriteRenderer.sprite;
            UpdatePivotAndSizeFromSprite(_spriteRenderer.sprite);
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            mousePosition.z = 10.0f; // Set this to be the distance you want the cursor to be from the camera
            var cursorPosition = mainCamera ? mainCamera.ScreenToWorldPoint(mousePosition) : Vector3.zero;
            cursorUI.transform.position = mousePosition;
            transform.position = cursorPosition;
        }
        void UpdatePivotAndSizeFromSprite(Sprite sprite)
        {
            if (sprite is null) return;
        
            // Get normalized pivot from sprite (0-1 range)
            var normalizedPivot = sprite.pivot / sprite.rect.size;
            cursorUI.rectTransform.pivot = normalizedPivot;
        
            // Calculate size based on sprite size and SpriteRenderer scale
            var spriteSize = sprite.rect.size;
            var scaledSize = new Vector2(
                spriteSize.x * transform.localScale.x,
                spriteSize.y * transform.localScale.y
            );
        
            cursorUI.rectTransform.sizeDelta = scaledSize;
        } 
        
        public void SetCursor(PlayerWeapons weapon)
        {
            switch (weapon)
            {
                case PlayerWeapons.None:
                    cursorTexture = skellyHandDefault;
                    cursorTextureClicked = skellyHandClicked;
                    break;
                case PlayerWeapons.Net:
                    cursorTexture = netHandDefault;
                    cursorTextureClicked = netHandClicked;
                    break;
                case PlayerWeapons.Scythe:
                    cursorTexture = scytheHandDefault;
                    cursorTextureClicked = scytheHandClicked;
                    break;
                case PlayerWeapons.Unalivatron:
                    cursorTexture = unalivatronHandDefault;
                    cursorTextureClicked = unalivatronHandClicked;
                    break;
                default:
                    cursorTexture = skellyHandDefault;
                    cursorTextureClicked = skellyHandClicked;
                    break;
            }
            _spriteRenderer.sprite = cursorTexture;
        }
    }
}