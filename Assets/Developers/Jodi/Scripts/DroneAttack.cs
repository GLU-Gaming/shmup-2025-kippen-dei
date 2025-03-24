using UnityEngine;

public class DroneAttack : MonoBehaviour
{
    public float speed = 5f;
    public GameObject projectilePrefab;
    public float shootDelay = 1f;

    private bool hasShot = false;
    private float shootTimer = 0f;
    private Vector3 initialPosition;
    private bool isMovingUp = false;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (!hasShot)
        {
            // Move down
            transform.position += Vector3.down * (speed * Time.deltaTime);

            // Shoot after delay
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootDelay)
            {
                Shoot();
                hasShot = true;
            }
        }
        else
        {
            // Move up
            transform.position += Vector3.up * (speed * Time.deltaTime);

            // Despawn when back to initial position
            if (transform.position.y >= initialPosition.y)
            {
                Destroy(gameObject);
            }
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
    }
}
