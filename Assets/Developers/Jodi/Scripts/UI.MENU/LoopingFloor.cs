using UnityEngine;

public class LoopingFloor : MonoBehaviour
{
    public float speed = 2f;
    public float offset = 0.1f;
    public float despawnX = -15f;  // New despawn X coordinate
    public Transform[] floorSegments;
    private float floorWidth;

    void Start()
    {
        floorWidth = floorSegments[0].GetComponent<MeshRenderer>().bounds.size.x;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Transform floor in floorSegments)
        {
            // Draw gizmo at despawnX position
            Gizmos.DrawLine(new Vector3(despawnX, floor.position.y, floor.position.z), 
                new Vector3(despawnX, floor.position.y + 1f, floor.position.z));
        }
    }

    void Update()
    {
        foreach (Transform floor in floorSegments)
        {
            // Move left in world space
            floor.Translate(Vector3.left * (speed * Time.deltaTime), Space.World);

            // Check if the segment has passed the despawn X coordinate
            if (floor.position.x <= despawnX)
            {
                // Find the rightmost floor segment
                Transform rightMost = floorSegments[0];
                foreach (Transform other in floorSegments)
                {
                    if (other.position.x > rightMost.position.x)
                        rightMost = other;
                }

                // Reposition to the right of the rightmost segment with offset
                floor.position = new Vector3(
                    rightMost.position.x + floorWidth - offset,
                    floor.position.y,
                    floor.position.z
                );
            }
        }
    }
}