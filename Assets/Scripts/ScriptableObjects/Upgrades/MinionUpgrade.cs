using UnityEngine;

namespace ldjam_58
{
    [CreateAssetMenu(fileName = "MinionUpgrade", menuName = "SO/Upgrades/MinionUpgrade")]
    public class MinionUpgrade : Upgrade
    {
        [SerializeField] private GameManagerChannel gameManagerChannel;
        
        private void OnEnable()
        {
            Type = UpgradeType.Minions;
        }
        
        override public void ApplyUpgrade()
        {
            gameManagerChannel.OnSpawnMinionRequested();
        }
    }
}
