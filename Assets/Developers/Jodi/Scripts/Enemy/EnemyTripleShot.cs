using UnityEngine;

public class EnemyTripleShotProjectile : Projectile
{
    [Header("Triple Shot Settings")]
    public float spreadAngle = 180f;
    public GameObject projectilePrefab;

    protected override void Start()
    {
        base.Start();
        FireTripleShots();
        Destroy(gameObject); // Remove this object
    }

    void FireTripleShots()
    {
        SpawnProjectileAtAngle(spreadAngle + 180);  // Up-left
        SpawnProjectileAtAngle(180);                // Left
        SpawnProjectileAtAngle(-spreadAngle + 180); // Down-left
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerComponent = other.GetComponent<Player>();
            if (playerComponent != null)
            {
                playerComponent.TakeDamage(damage);
            }
            
            // Trigger screen shake when the projectile hits the player
            if (screenShake != null)
            {
                screenShake.Shake(0.05f, 0.3f);  
            }
            
            Destroy(gameObject); 
        }
    }
}