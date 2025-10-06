using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ldjam_58
{
    public class SpawnerController : MonoBehaviour
    {
        [SerializeField] private GameState gameState;
        [Header("Prefabs to Spawn")] public GameObject[] prefabs;
        [Header("Spawn Settings")] public Transform spawnPoint;
        private float timer;
        
        public SoulController.SoulType EnemyType
        {
            get => _soulType; 
            set => _soulType = value;
        }
        private SoulController.SoulType _soulType;

        void Awake()
        {
            if (gameState is null)
            {
                throw new MissingComponentException("GameState is not assigned in the inspector");
            }
        }
        
        void Update()
        {
            timer += Time.deltaTime;
            var timeToSpawn = 1 / gameState.CurrentSpawnRate;
            if (timer >= timeToSpawn)
            {
                StartCoroutine(Spawn());
                timer = 0;
            }
        }
        
        IEnumerator Spawn() 
        {
            float delay = Random.Range(0f, 0.3f);
            yield return new WaitForSeconds(delay);
            SpawnPrefab();
        }

        void SpawnPrefab()
        {
            if (prefabs == null || prefabs.Length == 0)
            {
                return;
            }

            Vector3 position = spawnPoint ? spawnPoint.position : transform.position;
            Quaternion rotation = spawnPoint ? spawnPoint.rotation : Quaternion.identity;

            int index = Random.Range(0, prefabs.Length);
            GameObject prefabToSpawn = prefabs[index];
            Instantiate(prefabToSpawn, position, rotation);
        }

        public void ActivateSpawner()
        {
           gameObject.SetActive(true);
        }
        
        public void DeactivateSpawner()
        {
            gameObject.SetActive(false);
        }
    }
}