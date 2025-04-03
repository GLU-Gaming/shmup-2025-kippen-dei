using UnityEngine;

public class Fish : EnemyBase
{
    [Header("Fish Movement Settings")]
    public float speed = 2f;
    public float rotationSpeed = 5f;
    private bool movingLeft = true;
    private Quaternion targetRotation;

    [Header("Shooting Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;
    private float fireTimer;


    void Start()
    {
        targetRotation = Quaternion.Euler(0, 0, 0);
    }


    void Update()
    {
        Move();
        Shoot();
        RotateSmoothly();
    }

    public override void Move()
    {
        // Move left or right based on the direction
        transform.Translate((movingLeft ? Vector3.left : Vector3.right) * (speed * Time.deltaTime), Space.World);

        Vector3 leftScreenEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
        Vector3 rightScreenEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));

        // If it reaches the left side, set target rotation and flip direction
        if (movingLeft && transform.position.x < leftScreenEdge.x - 9)
        {
            movingLeft = false;
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        if (!movingLeft && transform.position.x > rightScreenEdge.x + 10)
        {
            Destroy(gameObject);
        }
    }

    private void RotateSmoothly()
    {
        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
            GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            EnemyProjectile projectileComponent = newProjectile.GetComponent<EnemyProjectile>();
            if (projectileComponent != null)
            {
                projectileComponent.direction = movingLeft ? new Vector3(-1, 1, 0).normalized : new Vector3(1, 1, 0).normalized;

            }
        }
    }

}
