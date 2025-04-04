using UnityEngine;
using System.Collections.Generic;

public class RandomBGObjects : MonoBehaviour
{
    public List<GameObject> bgObjectPrefabs;
    public List<Transform> spawnLocations;
    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 5f;

    private float nextSpawnTime;

    void Start()
    {
        SetNextSpawnTime();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnBGObject();
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

        // Get random prefab and spawn location
        GameObject prefab = bgObjectPrefabs[Random.Range(0, bgObjectPrefabs.Count)];
        Transform spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Count)];

        // Instantiate and initialize
        GameObject bgObject = Instantiate(prefab, spawnLocation.position, Quaternion.identity);
        LoopingBGObject loopingScript = bgObject.GetComponent<LoopingBGObject>();
        if (loopingScript != null)
        {
            loopingScript.Initialize(spawnLocations.ToArray());
        }
    }
}