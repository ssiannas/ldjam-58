using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ldjam_58
{
    public class UpgradeButtonController : MonoBehaviour
    {

        [SerializeField] private Upgrade upgradeData;
        [SerializeField] private GameState gameState;
        [SerializeField] private GameManagerChannel gameManagerChannel;
        
        // Button 
        private Button _upgradeButton;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _upgradeButton = GetComponent<Button>();
            _upgradeButton.interactable = false;
            var btnText = GetComponentInChildren<TextMeshProUGUI>();
            btnText.text = $"{upgradeData.Name}\nCost: {upgradeData.UpgradeCost}\n";
            if (upgradeData is null)
            {
                throw new MissingComponentException("Upgrade data is not assigned in the inspector");
            }

            if (gameState is null)
            {
                throw new MissingComponentException("GameState is not assigned in the inspector");
            }
            
            if (gameManagerChannel is null)
            {
                throw new MissingComponentException("GameManagerChannel is not assigned in the inspector");
            }
        }

        // Update is called once per frame
        void Update()
        {
            ShouldEnableButton();
        }
        
        void ShouldEnableButton()
        {
            if (upgradeData.UpgradeCost > gameState.CurrentSouls)
            {
                return;
            }

            if (gameState.CurrentUpgradeTier[upgradeData.Type] != upgradeData.UpgradeTier - 1)
            {
                return;
            }
            _upgradeButton.interactable = true;
        }

        public void OnPurchase()
        {
            upgradeData.ApplyUpgrade();
            gameManagerChannel.RemoveScore(upgradeData.UpgradeCost);
            gameState.CurrentUpgradeTier[upgradeData.Type] += 1;
            _upgradeButton.interactable = false;
        }
    }
}
