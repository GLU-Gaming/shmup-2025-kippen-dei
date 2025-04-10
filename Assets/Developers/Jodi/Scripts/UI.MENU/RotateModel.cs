using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public float rotationSpeed = 500f; // Increased speed to mimic a propeller

    void Update()
    {
        // Rotate around the Z-axis
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}