using UnityEngine;

public class SingleShotProjectile : Projectile
{
    [Header("Single Shot Settings")]
    public GameObject projectilePrefab;

    protected override void Start()
    {
        base.Start();
        FireSingleShot();
        Destroy(gameObject); // Remove this object after firing
    }

    void FireSingleShot()
    {
        SpawnProjectileAtAngle(0); // Fire straight forward
    }

    void SpawnProjectileAtAngle(float angle)
    {
        if (projectilePrefab == null) return;

        // Use Quaternion.Euler to set the angle (not relative to parent)
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