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
        private RectTransform _tooltipRect;
        
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
            _tooltipRect = GetComponent<RectTransform>();
            HideTooltip();
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
            var mousePosition = Mouse.current.position.value;
            var screenMiddle = new Vector2(Screen.width / 2f, Screen.height / 2f);
            _tooltipRect.pivot = mousePosition.x > screenMiddle.x ? 
                new Vector2(1, _tooltipRect.pivot.y) :
                new Vector2(0, _tooltipRect.pivot.y);
            _tooltipRect.position = mousePosition + new Vector2(0, 18f);
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
