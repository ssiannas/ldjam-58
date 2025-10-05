using System;
using UnityEngine;

namespace ldjam_58
{
    [CreateAssetMenu(fileName = "SpawnerUpgrade", menuName = "SO/Upgrades/SpawnerUpgrade")]
    public class SpawnerUpgrade : Upgrade
    {
        [SerializeField] private GameManagerChannel gmChannel;
        private readonly uint _maxTier = 4;
        [SerializeField] private uint _baseCost = 500;
        private void Reset()
        {
            UpgradeCost = _baseCost;
            UpgradeTier = 1;
        }
        private void OnEnable()
        {
            Type = UpgradeType.SpawnerUpgrade;
            UpgradeCost = _baseCost;
            gmChannel.OnUpgradeResetRequested += Reset;
        }
        
        public override void ApplyUpgrade()
        {
            gmChannel.UpgradeSpawner(UpgradeTier);
            UpgradeTier = Math.Min(UpgradeTier + 1, _maxTier);
            UpgradeCost = (uint)(UpgradeCost * 5.5f);
        }
    }
}
