using UnityEngine;

public class BGObjectPool : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 2f;
    public float despawnX = -15f;
    
    private Vector3 spawnPosition;
    private Transform objectPoolParent;

    void Update()
    {
        // Move object left at constant speed
        transform.Translate(Vector3.left * (moveSpeed * Time.deltaTime), Space.World);

        // Check despawn condition
        if (transform.position.x <= despawnX)
        {
            ReturnToPool();
        }
    }

    public void Initialize(Vector3 spawnPos, Transform poolParent)
    {
        spawnPosition = spawnPos;
        objectPoolParent = poolParent;
    }

    void ReturnToPool()
    {
        // Reset and return to pool
        transform.position = spawnPosition;
        transform.SetParent(objectPoolParent);
        gameObject.SetActive(false);
    }
}