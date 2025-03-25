using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnDrones : MonoBehaviour
{
    [Header("Drone Settings")]
    public List<GameObject> flyingEnemyPrefabs;
    public List<Transform> flyingSpawnLocations;

    [Header("Spawn Settings")]
    public float spawnCooldown = 3f;
    public float penalizedWeight = 0.2f;
    public float normalWeight = 1f;

    private Dictionary<Transform, float> spawnLocationLastUsed = new Dictionary<Transform, float>();

    void Start()
    {
        foreach (Transform spawn in flyingSpawnLocations)
        {
            spawnLocationLastUsed[spawn] = -spawnCooldown;
        }
    }

    public void SpawnSingleDrone()
    {
        if(flyingEnemyPrefabs.Count > 0 && flyingSpawnLocations.Count > 0)
        {
            SpawnRandomEnemy(flyingEnemyPrefabs, flyingSpawnLocations);
        }
    }

    public void SpawnDroneSwarm(int swarmSize)
    {
        StartCoroutine(SpawnSwarm(swarmSize));
    }

    IEnumerator SpawnSwarm(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            SpawnSingleDrone();
            yield return new WaitForSeconds(0.3f);
        }
    }

    void SpawnRandomEnemy(List<GameObject> enemyPrefabs, List<Transform> spawnLocations)
    {
        float totalWeight = 0f;
        List<float> weights = new List<float>();

        foreach (Transform spawn in spawnLocations)
        {
            float timeSinceLastUsed = Time.time - spawnLocationLastUsed[spawn];
            float weight = (timeSinceLastUsed < spawnCooldown) ? penalizedWeight : normalWeight;
            weights.Add(weight);
            totalWeight += weight;
        }

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
        spawnLocationLastUsed[spawnLocation] = Time.time;

        int randomPrefabIndex = Random.Range(0, enemyPrefabs.Count);
        Instantiate(enemyPrefabs[randomPrefabIndex], spawnLocation.position, Quaternion.identity);
    }
}
