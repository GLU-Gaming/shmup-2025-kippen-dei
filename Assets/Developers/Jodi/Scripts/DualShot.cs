using UnityEngine;

public class DualShotProjectile : Projectile
{
    [Header("Dual Shot Settings")]
    public float spreadAngle = 30f;
    public GameObject projectilePrefab;

    protected override void Start()
    {
        base.Start();
        FireDiagonalShots();
        Destroy(gameObject); // Remove this object
    }

    void FireDiagonalShots()
    {
        SpawnProjectileAtAngle(spreadAngle);  // Up-right
        SpawnProjectileAtAngle(-spreadAngle); // Down-right
    }

    void SpawnProjectileAtAngle(float angle)
    {
        if (projectilePrefab == null) return;

        // Use Quaternion.Euler directly to set the angle (not relative to parent)
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, rotation);

        // Copy properties to the new projectile
        Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
        if (projectileComponent != null)
        {
            projectileComponent.speed = speed;
            projectileComponent.damage = damage;
        }
    }
}