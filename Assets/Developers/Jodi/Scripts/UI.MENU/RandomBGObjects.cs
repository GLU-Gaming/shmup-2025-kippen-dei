using UnityEngine;
using System.Collections.Generic;

public class BGObjectSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public List<GameObject> objectPrefabs;
    public Transform spawnPoint;
    public float minSpawnInterval = 0.5f;
    public float maxSpawnInterval = 2f;
    public int initialPoolSize = 20;
    public float despawnX = -15f;

    [Header("Movement Settings")]
    public float objectSpeed = 2f;

    private Queue<GameObject> objectPool = new Queue<GameObject>();
    private float timer;
    private float currentSpawnInterval;

    void Start()
    {
        InitializePool();
        SetNewSpawnInterval();
    }

    void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = CreatePooledObject();
            objectPool.Enqueue(obj);
        }
    }

    GameObject CreatePooledObject()
    {
        GameObject prefab = objectPrefabs[Random.Range(0, objectPrefabs.Count)];
        GameObject obj = Instantiate(prefab, spawnPoint.position, prefab.transform.rotation);
        obj.SetActive(false);
        
        BGObjectPool poolScript = obj.AddComponent<BGObjectPool>();
        poolScript.moveSpeed = objectSpeed;
        poolScript.despawnX = despawnX;
        poolScript.Initialize(spawnPoint.position, transform);
        
        return obj;
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= currentSpawnInterval)
        {
            SpawnObject();
            timer = 0f;
            SetNewSpawnInterval();
        }
    }

    void SetNewSpawnInterval()
    {
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void SpawnObject()
    {
        if (objectPool.Count == 0)
        {
            objectPool.Enqueue(CreatePooledObject());
        }

        GameObject obj = objectPool.Dequeue();
        obj.transform.position = spawnPoint.position;
        obj.SetActive(true);
        objectPool.Enqueue(obj);
    }
}