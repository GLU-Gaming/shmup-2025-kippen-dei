using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public float rotationSpeed = 15f;

    void Update()
    {
        transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
    }
}
