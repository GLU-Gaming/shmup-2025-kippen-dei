using UnityEngine;
using System.Collections.Generic;

public class RandomBGObjects : MonoBehaviour
{
    [Header("Spawn Settings")]
    public float minSpawnInterval = 4f;
    public float maxSpawnInterval = 8f;
    [Range(0, 1)] public float spawnProbability = 0.3f;

    [Header("Object Settings")]
    public List<GameObject> bgObjectPrefabs;
    public List<Transform> spawnLocations;
    public float maxVerticalOffset = 0.5f;

    private float nextSpawnTime;
    private List<GameObject> objectPool = new List<GameObject>();

    void Start()
    {
        InitializePool();
        SetNextSpawnTime();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            TrySpawnObject();
            SetNextSpawnTime();
        }
    }

    void InitializePool()
    {
        foreach (GameObject prefab in bgObjectPrefabs)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                obj.GetComponent<LoopingBGObject>().SetSpawnLocations(spawnLocations.ToArray());
                objectPool.Add(obj);
            }
        }
    }

    void TrySpawnObject()
    {
        if (ShouldSpawn() && GetAvailableObject(out GameObject obj))
        {
            SpawnObject(obj);
        }
    }

    bool ShouldSpawn()
    {
        return bgObjectPrefabs.Count > 0 && 
               spawnLocations.Count > 0 && 
               Random.value <= spawnProbability;
    }

    bool GetAvailableObject(out GameObject availableObj)
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                availableObj = obj;
                return true;
            }
        }
        availableObj = CreateNewPooledObject();
        return availableObj != null;
    }

    GameObject CreateNewPooledObject()
    {
        if (bgObjectPrefabs.Count == 0) return null;
        
        GameObject prefab = bgObjectPrefabs[Random.Range(0, bgObjectPrefabs.Count)];
        GameObject newObj = Instantiate(prefab);
        newObj.GetComponent<LoopingBGObject>().SetSpawnLocations(spawnLocations.ToArray());
        objectPool.Add(newObj);
        return newObj;
    }

    void SpawnObject(GameObject obj)
    {
        Transform spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Count)];
        Vector3 spawnPosition = spawnLocation.position + 
            new Vector3(0, Random.Range(-maxVerticalOffset, maxVerticalOffset), 0);

        obj.transform.position = spawnPosition;
        obj.transform.rotation = obj.GetComponent<LoopingBGObject>().transform.rotation;
        obj.SetActive(true);
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }
}