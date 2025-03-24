using UnityEngine;

public class BossTripleShot : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject projectile; // Assign this in the Inspector with your projectile prefab
    public Transform shootPoint; // The spawn point for projectiles
    public float fireTimer; // Cooldown tracker
    public float fireRate = 1.5f; // How often is the boss shooting?
    void Update()
    {
        Timer();
    }

    private void Timer()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            fireTimer = 0f; // Reset de timer
            Shoot();
        }
    }

    void Shoot()
    {
        // Spawn the projectile at the shoot point with a rotation to the left
        GameObject newProjectile = Instantiate(projectile, shootPoint.position, shootPoint.rotation * Quaternion.Euler(0, 180, 0));
    }
}
