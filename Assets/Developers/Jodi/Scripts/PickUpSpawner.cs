using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PickUpSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private List<GameObject> pickupPrefabs;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float initialDelay = 2f;
    [SerializeField] private int maxActivePickups = 3;
    [SerializeField] private bool spawnContinuously = true;
    [SerializeField] private int totalSpawns = 10;

    [Header("Spawn Area")]
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f);
    [SerializeField] private bool useSpawnPoints = false;
    [SerializeField] private List<Transform> spawnPoints;

    private List<GameObject> activePickups = new List<GameObject>();
    private int spawnCount = 0;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(initialDelay);

        while (ShouldContinueSpawning())
        {
            if (activePickups.Count < maxActivePickups)
            {
                SpawnPickup();
                spawnCount++;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    bool ShouldContinueSpawning()
    {
        return spawnContinuously || spawnCount < totalSpawns;
    }

    void SpawnPickup()
    {
        if (pickupPrefabs.Count == 0)
        {
            Debug.LogWarning("No pickup prefabs assigned!");
            return;
        }

        Vector3 spawnPosition = GetSpawnPosition();
        GameObject pickupToSpawn = pickupPrefabs[Random.Range(0, pickupPrefabs.Count)];
        
        GameObject newPickup = Instantiate(pickupToSpawn, spawnPosition, Quaternion.identity);
        activePickups.Add(newPickup);

        // Setup destruction tracking
        PowerUpPickup pickupScript = newPickup.GetComponent<PowerUpPickup>();
        if (pickupScript != null)
        {
            pickupScript.OnPickup += () => activePickups.Remove(newPickup);
        }
        else
        {
            Debug.LogWarning("Spawned pickup missing PowerUpPickup component!");
        }
    }

    Vector3 GetSpawnPosition()
    {
        if (useSpawnPoints && spawnPoints.Count > 0)
        {
            return spawnPoints[Random.Range(0, spawnPoints.Count)].position;
        }
        else
        {
            Vector3 center = transform.position;
            return new Vector3(
                center.x + Random.Range(-spawnAreaSize.x/2, spawnAreaSize.x/2),
                center.y + Random.Range(-spawnAreaSize.y/2, spawnAreaSize.y/2),
                center.z + Random.Range(-spawnAreaSize.z/2, spawnAreaSize.z/2)
            );
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}