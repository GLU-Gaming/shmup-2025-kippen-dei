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
    
    private class PooledObject
    {
        public GameObject instance;
        public int prefabIndex;

        public PooledObject(GameObject instance, int prefabIndex)
        {
            this.instance = instance;
            this.prefabIndex = prefabIndex;
        }
    }
    
    private Queue<PooledObject> objectPool = new Queue<PooledObject>();
    private float timer;
    private float currentSpawnInterval;

    private int lastSpawnedPrefabIndex = -1;

    void Start()
    {
        InitializePool();
        SetNewSpawnInterval();
    }
    
    void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            PooledObject pooledObj = CreatePooledObject();
            objectPool.Enqueue(pooledObj);
        }
    }
    
    PooledObject CreatePooledObject()
    {
        int prefabIndex = Random.Range(0, objectPrefabs.Count);
        GameObject prefab = objectPrefabs[prefabIndex];
        GameObject obj = Instantiate(prefab, spawnPoint.position, prefab.transform.rotation);
        obj.SetActive(false);
        return new PooledObject(obj, prefabIndex);
    }

    void Update()
    {
        timer += Time.deltaTime;
        MoveActiveObjects();

        if (timer >= currentSpawnInterval)
        {
            SpawnObject();
            timer = 0f;
            SetNewSpawnInterval();
        }
    }
    
    void MoveActiveObjects()
    {
       
        foreach (PooledObject pooledObj in objectPool)
        {
            if (!pooledObj.instance.activeSelf)
                continue;

            pooledObj.instance.transform.Translate(Vector3.left * objectSpeed * Time.deltaTime);

            if (pooledObj.instance.transform.position.x < despawnX)
            {
                pooledObj.instance.SetActive(false);
                pooledObj.instance.transform.position = spawnPoint.position;
            }
        }
    }
    
    void SetNewSpawnInterval()
    {
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }
    
    void SpawnObject()
    {
        PooledObject candidate = null;
        int poolCount = objectPool.Count;

        for (int i = 0; i < poolCount; i++)
        {
            PooledObject pooledObj = objectPool.Dequeue();
            if (pooledObj.prefabIndex == lastSpawnedPrefabIndex && Random.value < 0.5f)
            {
                objectPool.Enqueue(pooledObj);
                continue;
            }
            else
            {
                candidate = pooledObj;
                break;
            }
        }
        
        if (candidate == null)
        {
            candidate = objectPool.Dequeue();
        }
        
        candidate.instance.transform.position = spawnPoint.position;
        candidate.instance.SetActive(true);
        
        lastSpawnedPrefabIndex = candidate.prefabIndex;
        
        objectPool.Enqueue(candidate);
    }
}
