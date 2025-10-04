using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ldjam_58
{
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
        [SerializeField] private UIChannel uiChannel;
        
        private int _availableUpgrades;
        
        
        void Awake()
        {
            if (timerText is null)
            {
                Debug.LogError("Timer Text is not assigned in the inspector", this);
                return;
            }

            if (scoreText is null)
            {
                Debug.LogError("Score Text is not assigned in the inspector", this);
                return;
            }

            if (uiChannel is null)
            {
                Debug.LogError("UI Channel is not assigned in the inspector", this);
                return;
            }

            uiChannel.OnUpgradeAvailable += DisplayAvailableUpgrades;
            uiChannel.OnUpdateScore += SetScoreText;
            uiChannel.OnUpgradeReset += ResetAvailableUpgrades;
            SetUpgradesText();
        }

        private void SetScoreText(string text)
        {
            if (scoreText is null) return;
            scoreText.text = text;
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
            _upgradeButtonText.text = _availableUpgrades > 0 ? $"Upgrades ({_availableUpgrades})" : "Upgrades";
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
        }
    }
}