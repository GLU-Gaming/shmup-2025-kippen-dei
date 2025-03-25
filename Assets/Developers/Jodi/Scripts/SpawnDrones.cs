using UnityEngine;
using System.Collections.Generic;

public class SpawnDrones : MonoBehaviour
{
    [Header("DroneSpawns")]
    public List<GameObject> flyingEnemyPrefabs; // List of flying enemy prefabs
    public List<Transform> flyingSpawnLocations; // List of flying spawn locations

    [Header("Spawn Settings")]
    public float minSpawnInterval = 1f; // Minimum spawn interval
    public float maxSpawnInterval = 5f; // Maximum spawn interval

    [Header("Spawn Cooldown Settings")]
    public float spawnCooldown = 3f; // Cooldown time during which a spawn location is less likely
    public float penalizedWeight = 0.2f; // Weight multiplier when a spawn location is on cooldown
    public float normalWeight = 1f;    // Normal weight for spawn locations

    private float nextSpawnTime;
    private Dictionary<Transform, float> spawnLocationLastUsed = new Dictionary<Transform, float>();

    void Start()
    {
        foreach (Transform spawn in flyingSpawnLocations)
        {
            spawnLocationLastUsed[spawn] = -spawnCooldown; 
        }
        SetNextSpawnTime(); 
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy(); 
            SetNextSpawnTime();
        }
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void SpawnEnemy()
    {
        // Always spawn a flying enemy if available.
        if (flyingEnemyPrefabs.Count > 0 && flyingSpawnLocations.Count > 0)
        {
            SpawnRandomEnemy(flyingEnemyPrefabs, flyingSpawnLocations);
        }
    }

    void SpawnRandomEnemy(List<GameObject> enemyPrefabs, List<Transform> spawnLocations)
    {
        // Weighted random selection for spawn locations.
        float totalWeight = 0f;
        List<float> weights = new List<float>();

        foreach (Transform spawn in spawnLocations)
        {
            float timeSinceLastUsed = Time.time - spawnLocationLastUsed[spawn];
            float weight = (timeSinceLastUsed < spawnCooldown) ? penalizedWeight : normalWeight;
            weights.Add(weight);
            totalWeight += weight;
        }

        // Generate a random value in the range [0, totalWeight)
        float randomValue = Random.Range(0, totalWeight);
        int chosenIndex = 0;
        for (int i = 0; i < weights.Count; i++)
        {
            randomValue -= weights[i];
            if (randomValue <= 0f)
            {
                chosenIndex = i;
                break;
            }
        }

        Transform spawnLocation = spawnLocations[chosenIndex];
        // Update the last used time for the chosen spawn location.
        spawnLocationLastUsed[spawnLocation] = Time.time;

        // Choose a random enemy prefab.
        int randomPrefabIndex = Random.Range(0, enemyPrefabs.Count);
        GameObject enemyPrefab = enemyPrefabs[randomPrefabIndex];

        // Spawn the enemy.
        Instantiate(enemyPrefab, spawnLocation.position, enemyPrefab.transform.rotation);
    }
}
