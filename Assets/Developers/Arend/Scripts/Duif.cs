using UnityEngine;

public class Duif : EnemyBase
{
    [Header("Bird Settings")]
    public float speed = 3f;  

    [Header("Shooting Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1.8f;
    private float fireTimer;

    void Update()
    {
        Move();
        Shoot();
    }

    public override void Move()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
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
}
