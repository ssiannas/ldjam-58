using System;
using UnityEngine;
using UnityEngine.Events;

namespace ldjam_58
{
    [CreateAssetMenu(fileName = "WeaponUpgrade", menuName = "SO/Upgrades/WeaponUpgrade")]
    public class WeaponUpgrade : Upgrade
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private GameManagerChannel gameManagerChannel;
        
        private void OnEnable()
        {
            Type = UpgradeType.WeaponUpgrade;
        }

        public override void ApplyUpgrade()
        {
            gameManagerChannel.OnChangePlayerWeaponRequested(weapon);
        }
    }
}
