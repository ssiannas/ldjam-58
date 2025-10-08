using System;
using UnityEngine;

namespace ldjam_58
{
    [CreateAssetMenu(fileName = "SoulValueUpgrade", menuName = "SO/Upgrades/SoulValueUpgrade")]
    public class SoulValueUpgrade : Upgrade
    {
        [SerializeField] private GameManagerChannel gmChannel;
        private readonly uint _maxTier = 4;
        [SerializeField] private uint _baseCost = 1000;
        [SerializeField] private uint _baseSoulValue = 1;
        private void Reset()
        {
            UpgradeCost = _baseCost;
            UpgradeTier = 1;
        }
        private void OnEnable()
        {
            Type = UpgradeType.SoulValueUpgrade;
            UpgradeCost = _baseCost;
            gmChannel.OnUpgradeResetRequested += Reset;
        }
        
        public override void ApplyUpgrade()
        {
            gmChannel.UpgradeSoulValue(_baseSoulValue * 5);
            UpgradeTier = Math.Min(UpgradeTier + 1, _maxTier);
            UpgradeCost = (uint)(UpgradeCost * 5.5);
        }
    }
}
