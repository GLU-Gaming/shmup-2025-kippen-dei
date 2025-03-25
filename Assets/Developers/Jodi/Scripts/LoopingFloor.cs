using UnityEngine;

public class LoopingFloor : MonoBehaviour
{
    public float speed = 2f;
    public float offset = 0.1f;
    public Transform[] floorSegments;
    private float floorWidth;

    void Start()
    {
        // Get the width of one floor segment
        floorWidth = floorSegments[0].GetComponent<MeshRenderer>().bounds.size.x;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Transform floor in floorSegments)
        {
            float threshold = -floorWidth * 3f - offset;
            Gizmos.DrawLine(new Vector3(threshold, floor.position.y, floor.position.z), 
                new Vector3(threshold, floor.position.y + 1f, floor.position.z));
        }
    }
    void Update()
    {
        foreach (Transform floor in floorSegments)
        {
            // Move left in world space
            floor.Translate(Vector3.left * (speed * Time.deltaTime), Space.World);

          
            float loopThreshold = -floorWidth * 1.2f - offset;
            
            if (floor.position.x <= loopThreshold)
            {
                // Find the rightmost floor segment
                Transform rightMost = floorSegments[0];
                foreach (Transform other in floorSegments)
                {
                    if (other.position.x > rightMost.position.x)
                        rightMost = other;
                }

                // Reposition to the right of the rightmost segment
                floor.position = new Vector3(
                    rightMost.position.x + floorWidth - offset,
                    floor.position.y,
                    floor.position.z
                );
            }
        }
    }
}