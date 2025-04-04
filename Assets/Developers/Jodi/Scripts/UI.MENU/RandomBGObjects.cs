using UnityEngine;
using System.Collections.Generic;

public class RandomBGObjects : MonoBehaviour
{
    public List<GameObject> bgObjectPrefabs;
    public List<Transform> spawnLocations;
    public float minSpawnInterval = 3f;  // Increased minimum interval
    public float maxSpawnInterval = 8f;  // Increased maximum interval
    [Range(0, 1)] public float spawnProbability = 0.3f;  // Added probability factor

    private float nextSpawnTime;

    void Start()
    {
        SetNextSpawnTime();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            // Only spawn if probability check passes
            if (Random.value <= spawnProbability)
            {
                SpawnBGObject();
            }
            SetNextSpawnTime();
        }
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void SpawnBGObject()
    {
        if (bgObjectPrefabs.Count == 0 || spawnLocations.Count == 0) return;

        GameObject prefab = bgObjectPrefabs[Random.Range(0, bgObjectPrefabs.Count)];
        Transform spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Count)];

        // Inherit prefab's rotation instead of using identity
        GameObject bgObject = Instantiate(
            prefab, 
            spawnLocation.position, 
            prefab.transform.rotation  // Changed to use prefab's rotation
        );

        LoopingBGObject loopingScript = bgObject.GetComponent<LoopingBGObject>();
        if (loopingScript != null)
        {
            loopingScript.Initialize(spawnLocations.ToArray());
        }
    }
}