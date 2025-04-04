using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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

    [Header("Debug Settings")]
    public bool showSpawnArea = true;

    private float nextSpawnTime;
    private List<GameObject> objectPool = new List<GameObject>();
    private float rightScreenEdge;

    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        CalculateScreenEdges();
        InitializePool();
        SetNextSpawnTime();
    }

    void CalculateScreenEdges()
    {
        rightScreenEdge = Camera.main.ViewportToWorldPoint(Vector3.right).x;
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
        // Create shuffled pool with equal distribution
        int poolSizePerPrefab = Mathf.Max(3, Mathf.CeilToInt(10f / bgObjectPrefabs.Count));
        
        for (int i = 0; i < poolSizePerPrefab; i++)
        {
            foreach (GameObject prefab in bgObjectPrefabs.OrderBy(x => Random.value))
            {
                GameObject obj = CreatePooledObject(prefab);
                PositionOffscreen(obj);
            }
        }
        
        // Shuffle final pool
        objectPool = objectPool.OrderBy(x => Random.value).ToList();
    }

    GameObject CreatePooledObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        obj.GetComponent<LoopingBGObject>().SetSpawnLocations(spawnLocations.ToArray());
        obj.SetActive(false);
        objectPool.Add(obj);
        return obj;
    }

    void TrySpawnObject()
    {
        if (ShouldSpawn() && GetAvailableObject(out GameObject obj))
        {
            PositionForSpawn(obj);
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
        // Get all inactive objects
        var candidates = objectPool.Where(obj => !obj.activeInHierarchy).ToList();
        
        if (candidates.Count > 0)
        {
            availableObj = candidates[Random.Range(0, candidates.Count)];
            return true;
        }
        
        // Create new object with random prefab if pool is empty
        availableObj = CreateNewPooledObject();
        return availableObj != null;
    }

    GameObject CreateNewPooledObject()
    {
        GameObject prefab = bgObjectPrefabs[Random.Range(0, bgObjectPrefabs.Count)];
        return CreatePooledObject(prefab);
    }

    void PositionForSpawn(GameObject obj)
    {
        // Get random spawn location
        Transform spawnPoint = spawnLocations[Random.Range(0, spawnLocations.Count)];
        
        Vector3 spawnPos = spawnPoint.position + 
                         new Vector3(0, Random.Range(-maxVerticalOffset, maxVerticalOffset), 0);
        
        obj.transform.position = spawnPos;
        obj.transform.rotation = obj.GetComponent<LoopingBGObject>().transform.rotation;
        obj.SetActive(true);
    }

    void PositionOffscreen(GameObject obj)
    {
        obj.transform.position = new Vector3(
            rightScreenEdge * 2,
            obj.transform.position.y,
            obj.transform.position.z
        );
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void OnDrawGizmos()
    {
        if (showSpawnArea && spawnLocations.Count > 0)
        {
            Gizmos.color = Color.green;
            foreach (Transform loc in spawnLocations)
            {
                Gizmos.DrawWireCube(loc.position, new Vector3(0.5f, 2f, 0));
            }
        }
    }
}