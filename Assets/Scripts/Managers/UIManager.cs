using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ldjam_58
{
    [RequireComponent(typeof(PopUpUIController))]
    public class UIManager : MonoBehaviour
    {

        private float _timer;
        
        [Header("Upgrade Button")]
        [SerializeField] private TextMeshProUGUI _upgradeButtonText;
        [SerializeField] private Image _upgradeButtonSprite;
        private Color _upgradeAvailableColor = Color.green;
        private Color _upgradeUnavailableColor = Color.gray;
        
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI timerText;
        
        [Header("Channels")]
        [SerializeField] private UIChannel uiChannel;
        [SerializeField] private GameManagerChannel gameManagerChannel;
        
        private Canvas _canvas;
        private int _availableUpgrades;
        [SerializeField] private PopUpUIController popUpUIController;

        private Vector2Int _cachedResolution;
        private DialogueBoxController _dialogBoxController;
        void Awake()
        {
            _dialogBoxController = GetComponentInChildren<DialogueBoxController>();
            
            CheckMissingComponents();
            uiChannel.OnUpgradeAvailable += DisplayAvailableUpgrades;
            uiChannel.OnUpdateScore += SetScoreText;
            uiChannel.OnUpgradeReset += ResetAvailableUpgrades;
            uiChannel.OnGetCanvasScaleFactor += GetCanvasScaleFactor;
            uiChannel.OnShowDialog += MaybeShowDialogue;
            
            popUpUIController = GetComponent<PopUpUIController>();
            _canvas = GetComponent<Canvas>();
            _cachedResolution = new Vector2Int(Screen.width, Screen.height);
            SetUpgradesText();
        }

        private void CheckMissingComponents()
        {
            if (_dialogBoxController is null)
            {
                throw new MissingComponentException("DialogueBoxController not found in children");
            }

            if (timerText is null)
            {
                throw new MissingComponentException("TimerText not found in children");
            }

            if (scoreText is null)
            {
                throw new MissingComponentException("ScoreText not found in children");
            }

            if (uiChannel is null)
            {
                throw new MissingComponentException("UIChannel is not assigned in the inspector");
            }
            
            if (gameManagerChannel is null)
            {
                throw new MissingComponentException("GameManagerChannel is not assigned in the inspector");
            }
        }

        private void SetScoreText(string text)
        {
            if (scoreText is null) return;
            scoreText.text = text;
        }

        private void MaybeShowDialogue(string id)
        {
            gameManagerChannel.PauseGame();
            _dialogBoxController?.ShowTextById(id);
        }
        
        public void DisplayAvailableUpgrades()
        {
            _availableUpgrades++;
            SetUpgradesText();
        }

        public void ResetAvailableUpgrades()
        {
            _availableUpgrades = 0;
            SetUpgradesText();
        }

        private void SetUpgradesText()
        {
            if (popUpUIController.IsVisible && _availableUpgrades > 0) return;
            _upgradeButtonText.text = _availableUpgrades > 0 ? $"Upgrades ({_availableUpgrades})" : "Upgrades";
            _upgradeButtonSprite.color = _availableUpgrades > 0 ? _upgradeAvailableColor : _upgradeUnavailableColor;
        }

        private void SetTimerText(string text)
        {
            if (timerText is null) return;
            timerText.text = text;
        }

        // Update is called once per frame
        void Update()
        {
            _timer += Time.deltaTime;
            var timeSpan = TimeSpan.FromSeconds(_timer);
            // Trim down to MM:SS
            timeSpan = new TimeSpan(0, timeSpan.Minutes, timeSpan.Seconds);
            SetTimerText(timeSpan.ToString(@"mm\:ss"));
            if (Screen.width != _cachedResolution.x || Screen.height != _cachedResolution.y)
            {
                _cachedResolution = new Vector2Int(Screen.width, Screen.height);
                uiChannel.ForceUpdateCursor(GetCanvasScaleFactor());
            }
        }

        private float GetCanvasScaleFactor()
        {
            Canvas.ForceUpdateCanvases();
            return _canvas.scaleFactor;
        }
    }
}