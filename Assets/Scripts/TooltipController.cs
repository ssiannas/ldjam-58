using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

namespace ldjam_58
{
    public class TooltipController : MonoBehaviour
    {
        [Header("UI channel")]
        private UIManager _uiManager;
        [SerializeField] private UIChannel uiChannel;

        [Header("TMPro Data")]
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI flavorText;
        [SerializeField] private RectTransform tooltipRect;
        [SerializeField] private Vector2 _offsetFromCursor = new Vector2(5000, 1000); 
        [SerializeField] private Canvas _canvas;

        public struct TooltipData
        {
            public string title;
            public string description;
            public string flavourText;
        }

        private static TooltipController instance;

        void Awake()
        {
            instance = this;

            if (_canvas == null) _canvas = GetComponentInParent<Canvas>();
            HideTooltip();
            //gameObject.SetActive(false);
        }

        void Update()
        {
            if (gameObject.activeSelf)
            {
                FollowCursor();
            }
        }

        void OnEnable()
        {
            uiChannel.OnShowTooltip -= ShowTooltip;
            uiChannel.OnHideTooltip += HideTooltip;
        }

        void OnDisable()
        {
            uiChannel.OnShowTooltip += ShowTooltip;
            uiChannel.OnHideTooltip -= HideTooltip;
        }


        private void FollowCursor()
        {
            Vector2 mousePosition = Mouse.current.position.value;

            //RectTransformUtility.ScreenPointToLocalPointInRectangle(
            //    _canvas.transform as RectTransform,
            //    mousePosition,
            //    _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera,
            //    out Vector2 localPoint
            //);

            //Vector2 adjustedOffset = new Vector2(_offsetFromCursor.x, _offsetFromCursor.y);

            tooltipRect.position = (Vector3)(mousePosition);

           

            //ClampToScreen();
        }

        private void ClampToScreen()
        {
            Vector3[] corners = new Vector3[4];
            tooltipRect.GetWorldCorners(corners);

            Vector2 pos = tooltipRect.localPosition; // Use anchoredPosition, not localPosition

            // Get canvas rect for proper bounds
            RectTransform canvasRect = _canvas.transform as RectTransform;
            Vector2 canvasSize = canvasRect.rect.size;

            // Clamp to canvas bounds
            float tooltipWidth = tooltipRect.rect.width;
            float tooltipHeight = tooltipRect.rect.height;

            // Clamp X
            if (pos.x + tooltipWidth > canvasSize.x / 2)
                pos.x = canvasSize.x / 2 - tooltipWidth;
            if (pos.x < -canvasSize.x / 2)
                pos.x = -canvasSize.x / 2;

            // Clamp Y
            if (pos.y + tooltipHeight > canvasSize.y / 2)
                pos.y = canvasSize.y / 2 - tooltipHeight;
            if (pos.y < -canvasSize.y / 2)
                pos.y = -canvasSize.y / 2;

            tooltipRect.localPosition = pos;
        }


        public static void ShowTooltip(Upgrade data)
        {
            Debug.Log("Should show toolkit");
            instance.titleText.text = data.Name;
            instance.descriptionText.text = data.Description;
            instance.flavorText.text = data.FlavorText;
            instance.gameObject.SetActive(true);
        }

        public static void HideTooltip()
        {
            instance.gameObject.SetActive(false);
        }
    }
}
