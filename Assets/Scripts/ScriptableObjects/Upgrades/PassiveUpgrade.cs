using UnityEngine;

namespace ldjam_58
{
    [CreateAssetMenu(fileName = "PassiveUpgrade", menuName = "SO/Upgrades/PassiveUpgrade")]
    public class PassiveUpgrade : Upgrade
    {
        [SerializeField] private float passiveModifier;
        [SerializeField] private GameManagerChannel gameManagerChannel;

        private void OnEnable()
        {
            Type = UpgradeType.PassiveIncome;
        }

        public override void ApplyUpgrade()
        {
            gameManagerChannel.OnChangePassiveIncomeRequested(passiveModifier);
        }
    }
}
