using UnityEngine;

public class Fish : EnemyBase
{
    [Header("Fish Movement Settings")]
    public float speed = 2f;
    private bool movingLeft = true;
    private Quaternion targetRotation;
    public float rotationSpeed = 5f; // Hoe snel de rotatie gebeurt

    [Header("Shooting Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;  // Hoe vaak wordt er geschoten?
    private float fireTimer;

    void Update()
    {
        Move();
        RotateSmoothly();
        Shoot();
    }

    public override void Move()
    {
        transform.Translate(Vector2.right * (movingLeft ? -speed : speed) * Time.deltaTime);

        if (transform.position.x < -10)
        {
            movingLeft = !movingLeft;
            targetRotation = Quaternion.Euler(0, -180, 0); // Kijk naar rechts
        }
        if (transform.position.x > 11)
        {
            Destroy(gameObject);
        }
    }

    private void RotateSmoothly()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            fireTimer = 0f; // Reset de timer
            FireProjectile();
        }
    }

    private void FireProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        }
    }
}
