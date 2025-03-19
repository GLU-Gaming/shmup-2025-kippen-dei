using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject projectile; // Assign this in the Inspector with your projectile prefab
    public Transform shootPoint; // The spawn point for projectiles

    private float nextShootTime = 0f; // Cooldown tracker

    void Update()
    {
        // Shoot when Space is held down and cooldown has expired
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextShootTime)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Spawn the projectile at the shoot point
        GameObject newProjectile = Instantiate(projectile, shootPoint.position, shootPoint.rotation);
        
        // Get the Projectile component to read its cooldownTime
        Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
        if (projectileComponent != null)
        {
            // Set the cooldown based on the projectile's settings
            nextShootTime = Time.time + projectileComponent.cooldownTime;
        }
    }
}