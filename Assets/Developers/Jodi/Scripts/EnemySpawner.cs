using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Flying Enemies")]
    public List<GameObject> flyingEnemyPrefabs;     //list of flying enemy prefabs
    public List<Transform> flyingSpawnLocations;    //list of flying spawn locations
    public float flyingSpawnProbability = 0.5f; // Probability of spawning a flying enemy

    [Header("Ground Enemies")]
    public List<GameObject> groundEnemyPrefabs;  //list of ground enemy prefabs
    public Transform groundSpawnLocation;  //ground spawn location

    [Header("Spawn Settings")]
    public float minSpawnInterval = 3f;  //minimum spawn interval
    public float maxSpawnInterval = 7f; //maximum spawn interval

    private float nextSpawnTime;

    void Start()
    {
        SetNextSpawnTime(); // Set the first spawn time
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();  //spawn an enemy
            SetNextSpawnTime();
        }
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void SpawnEnemy()
    {
        // Spawn flying enemy with a certain probability
        if (flyingEnemyPrefabs.Count > 0 && flyingSpawnLocations.Count > 0 && Random.value < flyingSpawnProbability)
        {
            SpawnRandomEnemy(flyingEnemyPrefabs, flyingSpawnLocations);
        }
        // Otherwise, spawn a ground enemy
        else if (groundEnemyPrefabs.Count > 0 && groundSpawnLocation != null)
        {
            SpawnRandomEnemy(groundEnemyPrefabs, groundSpawnLocation);
        }
    }

    void SpawnRandomEnemy(List<GameObject> enemyPrefabs, List<Transform> spawnLocations)  //spawn enemy from list of prefabs at random location
    {
        int randomPrefabIndex = Random.Range(0, enemyPrefabs.Count);
        int randomLocationIndex = Random.Range(0, spawnLocations.Count);
        Instantiate(enemyPrefabs[randomPrefabIndex], spawnLocations[randomLocationIndex].position, spawnLocations[randomLocationIndex].rotation);
    }

    void SpawnRandomEnemy(List<GameObject> enemyPrefabs, Transform spawnLocation)  //spawn enemy from list of prefabs at specific location  
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Count);
        Instantiate(enemyPrefabs[randomIndex], spawnLocation.position, spawnLocation.rotation);
    }
}