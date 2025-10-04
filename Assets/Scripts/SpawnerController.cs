using UnityEngine;

namespace ldjam_58
{
    public class SpawnerController : MonoBehaviour
    {
        [SerializeField] private GameState gameState;
        [Header("Prefabs to Spawn")] public GameObject[] prefabs;
        [Header("Spawn Settings")] public Transform spawnPoint;

        private float timer;

        void Awake()
        {
            if (gameState is null)
            {
                Debug.LogError("GameState ScriptableObject is not assigned in PrefabSpawner!", this);
                return;
            }
        }
        
        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= gameState.SpawnInterval)
            {
                SpawnPrefab();
                timer = 0f;
            }
        }

        void SpawnPrefab()
        {
            if (prefabs == null || prefabs.Length == 0)
            {
                Debug.LogWarning("No prefab assigned to PrefabSpawner!");
                return;
            }

            Vector3 position = spawnPoint ? spawnPoint.position : transform.position;
            Quaternion rotation = spawnPoint ? spawnPoint.rotation : Quaternion.identity;

            int index = Random.Range(0, prefabs.Length);
            GameObject prefabToSpawn = prefabs[index];

            Instantiate(prefabToSpawn, position, rotation);
        }
    }
}