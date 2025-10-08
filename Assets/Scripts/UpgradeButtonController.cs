using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ldjam_58
{
    public class UpgradeButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] private Upgrade upgradeData;
        [SerializeField] private GameState gameState;
        [SerializeField] private GameManagerChannel gameManagerChannel;
        [SerializeField] private UIChannel uiChannel;
        
        // Button 
        private Button _upgradeButton;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _upgradeButton = GetComponent<Button>();
            _upgradeButton.interactable = false;
            MaybeUpdateBtnText();
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
            ShouldDisableButton();
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
            if (_upgradeButton.interactable) return;
            OnUpgradeAvailable();
        }


        void ShouldDisableButton()
        {
            if (upgradeData.UpgradeCost > gameState.CurrentSouls)
            {
                _upgradeButton.interactable = false;
                return;
            }
        }

        public void OnUpgradeAvailable()
        {
            _upgradeButton.interactable = true;
            uiChannel.NotifyUpgradeAvailable();
        }
        
        private void MaybeUpdateBtnText()
        {
            var btnText = GetComponentInChildren<TextMeshProUGUI>();
            btnText.text = $"{upgradeData.UpgradeName}\nCost: {upgradeData.UpgradeCost}\n";
        }

        public void OnPurchase()
        {
            upgradeData.ApplyUpgrade();
            gameManagerChannel.RemoveScore(upgradeData.UpgradeCost);
            gameState.CurrentUpgradeTier[upgradeData.Type] += 1;
            MaybeUpdateBtnText();
            _upgradeButton.interactable = false;
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            uiChannel.ShowTooltip(upgradeData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            uiChannel.HideTooltip();
        }
    }
}
