using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ldjam_58
{
    public class SpawnerMasterController : MonoBehaviour
    {

        [SerializeField] private GameObject[] walkerSpawnPoints;
        [SerializeField] private GameObject[] flyerSpawnPoints;
        [SerializeField] private List<SpawnerController> spawnerControllers;
        
        [SerializeField] private GameObject spawnerPrefab;
        [SerializeField] private SpawnerUpgrade spawnerUpgradeSO;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            spawnerUpgradeSO.MaxTier = (uint)(walkerSpawnPoints.Length + flyerSpawnPoints.Length) - 1;
            // First the walker spawners
            foreach (var spawner in walkerSpawnPoints)
            {
                var spawnerInstance = Instantiate(spawnerPrefab, spawner.transform.position, Quaternion.identity);
                var spawnerController = spawnerInstance.GetComponent<SpawnerController>();
                spawnerController.EnemyType = SoulController.SoulType.Walker;
                spawnerControllers.Add(spawnerController);
                spawnerController.DeactivateSpawner();
            }
            
            // Then the flyer spawners
            foreach (var spawner in flyerSpawnPoints)
            {
                var spawnerInstance = Instantiate(spawnerPrefab, spawner.transform.position, Quaternion.identity);
                var spawnerController = spawnerInstance.GetComponent<SpawnerController>();
                spawnerController.EnemyType = SoulController.SoulType.Flyer;
                spawnerControllers.Add(spawnerController);
                spawnerController.DeactivateSpawner();
            }
            
            // Activate the first spawner
            if (spawnerControllers.Count > 0)
            {
                spawnerControllers[0].ActivateSpawner();
            }
        }

        public void UnlockNextSpawner(uint newTier)
        {
            if (newTier >= spawnerControllers.Count) { return; }
            spawnerControllers[(int)newTier].ActivateSpawner();
        }
    }
}
