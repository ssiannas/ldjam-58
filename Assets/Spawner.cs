using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [Header("Prefabs to Spawn")]
    public GameObject[] prefabs;

    [Header("Spawn Settings")]
    public Transform spawnPoint;  // Optional: where to spawn
    public float spawnInterval = 2f;  // How often to spawn

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
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
