using UnityEngine;

public class TripleShotProjectile : Projectile
{
    [Header("Triple Shot Settings")]
    public float spreadAngle = 30f;
    public GameObject projectilePrefab;

    protected override void Start()
    {
        base.Start();
        FireTripleShots();
        Destroy(gameObject); // Remove this object
    }

    void FireTripleShots()
    {
        SpawnProjectileAtAngle(spreadAngle);  // Up-right
        SpawnProjectileAtAngle(0);            // Forward
        SpawnProjectileAtAngle(-spreadAngle); // Down-right
    }

void SpawnProjectileAtAngle(float angle)
{
    if (projectilePrefab == null) return;

    // Create a new rotation based on the angle (not compounding with parent)
    Quaternion newRotation = Quaternion.Euler(0, 0, angle);
    GameObject newProjectile = Instantiate(
        projectilePrefab, 
        transform.position, 
        newRotation // Use the angle directly, not relative to parent
    );

    // Copy properties (speed, damage) to the new projectile
    Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
    if (projectileComponent != null)
    {
        projectileComponent.speed = speed;
        projectileComponent.damage = damage;
    }
}
}