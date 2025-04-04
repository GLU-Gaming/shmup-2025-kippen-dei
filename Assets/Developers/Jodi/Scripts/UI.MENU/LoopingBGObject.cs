using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class LoopingBGObject : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    
    [Header("Reposition Settings")]
    public float screenEdgeBuffer = 1f;

    private Transform[] spawnLocations;
    private float objectRightEdge;
    private float leftScreenEdge;
    private Renderer objectRenderer;

    void Start()
    {
        InitializeComponents();
        CalculateEdges();
    }

    void InitializeComponents()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    void CalculateEdges()
    {
        Vector3 objectRight = transform.TransformPoint(objectRenderer.bounds.max);
        objectRightEdge = objectRight.x;
        leftScreenEdge = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
    }

    void Update()
    {
        MoveObject();
        UpdateObjectEdges();
        CheckReposition();
    }

    void MoveObject()
    {
        transform.Translate(Vector3.left * (speed * Time.deltaTime), Space.World);
    }

    void UpdateObjectEdges()
    {
        Vector3 objectRight = transform.TransformPoint(objectRenderer.bounds.max);
        objectRightEdge = objectRight.x;
    }

    void CheckReposition()
    {
        float repositionThreshold = leftScreenEdge - screenEdgeBuffer;
        if (objectRightEdge <= repositionThreshold && spawnLocations.Length > 0)
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
        UpdateObjectEdges();
    }

    public void SetSpawnLocations(Transform[] locations)
    {
        spawnLocations = locations;
    }

    void OnDrawGizmosSelected()
    {
        if (objectRenderer != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(
                new Vector3(objectRightEdge, transform.position.y - 1, transform.position.z),
                new Vector3(objectRightEdge, transform.position.y + 1, transform.position.z)
            );
        }
    }
}