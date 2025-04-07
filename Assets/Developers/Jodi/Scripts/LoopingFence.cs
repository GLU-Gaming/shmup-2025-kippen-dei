using UnityEngine;
using System.Collections.Generic;

public class LoopingFence : MonoBehaviour
{
    public List<Transform> fenceSegments;
    public float speed = 5f;
    public float leftBoundary = -15f;
    
    private float segmentLength;

    void Start()
    {
        if (fenceSegments.Count == 0)
        {
            Debug.LogError("No fence segments assigned!");
            return;
        }

        // Calculate segment length based on the first segment's renderer bounds
        Renderer rend = fenceSegments[0].GetComponent<Renderer>();
        if (rend != null)
        {
            segmentLength = rend.bounds.size.x;
        }
        else
        {
            segmentLength = 10f;
            Debug.LogWarning("Renderer not found. Using default segment length.");
        }
    }

    void Update()
    {
        if (fenceSegments.Count == 0) return;

        foreach (Transform segment in fenceSegments)
        {
            // Move segment left in world space
            segment.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

            // Check if segment passed the boundary
            if (segment.position.x <= leftBoundary)
            {
                // Find the current rightmost segment
                float rightMostX = Mathf.NegativeInfinity;
                foreach (Transform s in fenceSegments)
                {
                    if (s.position.x > rightMostX)
                    {
                        rightMostX = s.position.x;
                    }
                }

                // Position the segment after the rightmost one
                Vector3 newPos = segment.position;
                newPos.x = rightMostX + segmentLength;
                segment.position = newPos;
            }
        }
    }
}