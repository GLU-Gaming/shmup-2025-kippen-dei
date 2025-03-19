using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject projectile; // The projectile prefab
    public Transform ShootPoint; // The point where the projectile will be spawned

    private float nextShootTime = 0f; // Shootingcooldown

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextShootTime) // Shoot when space is pressed
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject newProjectile = Instantiate(projectile, ShootPoint.position, ShootPoint.rotation);
        Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
        if (projectileComponent != null)
        {
            nextShootTime = Time.time + projectileComponent.cooldownTime;
        }
    }
}