using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class LoopingBGObject : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Movement speed in units/second")]
    public float speed = 2f;
    
    [Header("Reposition Settings")]
    [Tooltip("Extra space past screen edge before repositioning")]
    public float screenEdgeBuffer = 1f;

    private Transform[] spawnLocations;
    private float repositionThreshold;
    private float objectWidth;

    void Start()
    {
        InitializeObject();
    }

    void InitializeObject()
    {
        // Calculate object width using renderer bounds
        Renderer rend = GetComponent<Renderer>();
        objectWidth = rend != null ? rend.bounds.size.x : 2f;

        // Calculate reposition threshold
        float cameraLeftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        repositionThreshold = cameraLeftEdge - objectWidth - screenEdgeBuffer;
    }

    void Update()
    {
        MoveObject();
        CheckReposition();
    }

    void MoveObject()
    {
        transform.Translate(Vector3.left * (speed * Time.deltaTime), Space.World);
    }

    void CheckReposition()
    {
        if (transform.position.x <= repositionThreshold && spawnLocations.Length > 0)
        {
            RepositionObject();
        }
    }

    void RepositionObject()
    {
        Transform newLocation = spawnLocations[Random.Range(0, spawnLocations.Length)];
        transform.position = new Vector3(
            newLocation.position.x,
            transform.position.y,
            transform.position.z
        );
    }

    public void SetSpawnLocations(Transform[] locations)
    {
        spawnLocations = locations;
    }
}