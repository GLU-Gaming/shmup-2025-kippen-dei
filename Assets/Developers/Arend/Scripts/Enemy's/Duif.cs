using UnityEngine;

public class Duif : EnemyBase
{
    [Header("Bird Settings")] public float speed = 3f;

    [Header("Shooting Settings")] public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1.8f;
    private float fireTimer;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 3f, transform.position.z);
    }

    void Update()
    {
        Move();
        Shoot();
        CheckPosition();
    }

    private void CheckPosition()
    {
        if (transform.position.x <= -18f)
        {
            Destroy(gameObject);
        }
    }

    public override void Move()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
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

            Instantiate(projectilePrefab, firePoint.position, projectilePrefab.transform.rotation);
        }
    }
}