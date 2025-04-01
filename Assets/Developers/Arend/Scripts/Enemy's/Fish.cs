using UnityEngine;

public class Fish : EnemyBase
{
    [Header("Fish Movement Settings")]
    public float speed = 2f;
    public float rotationSpeed = 5f;
    private bool movingLeft = true;

    [Header("Shooting Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;
    private float fireTimer;


    void Update()
    {
        Move();
        Shoot();
        CheckPosition();
    }

    public override void Move()
    {
        // Move left or right based on the direction
        transform.Translate((movingLeft ? Vector3.left : Vector3.right) * (speed * Time.deltaTime), Space.World);
    }

    private void Shoot()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;
            FireProjectile();
        }
    }

    private void FireProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        }
    }

     private void CheckPosition()
    {
        if (transform.position.x <= -18f)
        {
            Destroy(gameObject);
        }
    }
}
