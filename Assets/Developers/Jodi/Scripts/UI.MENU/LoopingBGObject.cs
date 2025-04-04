using UnityEngine;

public class LoopingBGObject : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] spawnLocations;
    private float repositionThreshold;

    void Start()
    {
        // Calculate left edge threshold
        repositionThreshold = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x - 2f;
    }

    public void Initialize(Transform[] locations)
    {
        spawnLocations = locations;
    }

    void Update()
    {
        // Move object left
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

        // Reposition when past threshold
        if (transform.position.x <= repositionThreshold && spawnLocations.Length > 0)
        {
            // Get random spawn location
            Transform newLocation = spawnLocations[Random.Range(0, spawnLocations.Length)];
            transform.position = newLocation.position;
        }
    }
}