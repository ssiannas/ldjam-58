using UnityEngine;

namespace ldjam_58
{
    public abstract class Upgrade : ScriptableObject
    {
        public enum UpgradeType : int
        {
            None,
            SpawnRate,
            WeaponUpgrade,
            Minions,
            PassiveIncome,
            SpawnerUpgrade,
        }
        
        
        [SerializeField] private string upgradeName;
        public string Name
        {
            get => upgradeName;
            protected set => upgradeName = value;
        }
        
        [SerializeField] private string flavorText;
        public string FlavorText
        {
            get => flavorText;
            protected set => flavorText = value;
        }
        
        [SerializeField] private uint upgradeCost;
        public uint UpgradeCost
        {
            get => upgradeCost;
            protected set => upgradeCost = value;
        }
        
        [SerializeField] private uint upgradeTier;
        public uint UpgradeTier
        {
            get => upgradeTier;
            protected set => upgradeTier = value;
        }
        
        [SerializeField] private UpgradeType upgradeType;
        public UpgradeType Type
        {
            get => upgradeType;
            protected set => upgradeType = value;
        }
        
        public abstract void ApplyUpgrade();
    }
}
