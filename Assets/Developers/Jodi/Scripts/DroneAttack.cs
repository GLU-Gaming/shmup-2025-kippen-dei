using UnityEngine;

public class DroneAttack : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float minShootDelay = 0.5f;
    public float maxShootDelay = 2f;

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float spreadAngle = 45f;
    public float damage = 1f;

    [Header("Y Position Settings")]
    [Tooltip("Maximum distance below starting position the drone can move to")]
    public float maxDownwardDistance = -2f;
    [Tooltip("Minimum distance below starting position the drone can move to")]
    public float minDownwardDistance = -5f;

    private bool hasShot = false;
    private float shootTimer = 0f;
    private float shootDelay;
    private Vector3 initialPosition;
    private float targetYPosition;

    void Start()
    {
        initialPosition = transform.position;
        
        // Ensure valid Y range
        minDownwardDistance = Mathf.Min(minDownwardDistance, maxDownwardDistance);
        targetYPosition = initialPosition.y + Random.Range(minDownwardDistance, maxDownwardDistance);
        
        shootDelay = Random.Range(minShootDelay, maxShootDelay);
    }

    void Update()
    {
        if (!hasShot)
        {
            // Move downward
            transform.position += Vector3.down * (speed * Time.deltaTime);

            // Check if reached target depth
            if (transform.position.y <= targetYPosition)
            {
                shootTimer += Time.deltaTime;
                if (shootTimer >= shootDelay)
                {
                    ShootTripleShot();
                    hasShot = true;
                }
            }
        }
        else
        {
            // Return upward
            transform.position += Vector3.up * (speed * Time.deltaTime);

            // Destroy when back to starting height
            if (transform.position.y >= initialPosition.y)
            {
                Destroy(gameObject);
            }
        }
    }

    void ShootTripleShot()
    {
        if (projectilePrefab != null && shootPoint != null)
        {
            SpawnProjectileAtAngle(180 - spreadAngle);  // Upper-left trajectory
            SpawnProjectileAtAngle(180);               // Direct left
            SpawnProjectileAtAngle(180 + spreadAngle); // Lower-left trajectory
        }
    }

    void SpawnProjectileAtAngle(float angle)
    {
        if (projectilePrefab == null || shootPoint == null) return;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, rotation);

        if (projectile.TryGetComponent(out Projectile projectileComponent))
        {
            projectileComponent.speed = speed;
            projectileComponent.damage = damage;
        }
    }
}