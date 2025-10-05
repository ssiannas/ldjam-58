using UnityEngine;

namespace ldjam_58
{
    [CreateAssetMenu(fileName = "SpawnRateUpgrade", menuName = "SO/Upgrades/SpawnRateUpgrade")]
    public class SpawnRateUpgrade : Upgrade
    {
        [SerializeField] private float spawnRateIncrease = 0f;
        [SerializeField] private GameState gameState;
        
        private void OnEnable()
        {
            Type = UpgradeType.SpawnRate;
        }

        public override void ApplyUpgrade()
        {
            var newSpawnrate = (1 + spawnRateIncrease) * gameState.CurrentSpawnRate;
            gameState.CurrentSpawnRate = newSpawnrate;
        } 
    }
}
