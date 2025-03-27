using System.Collections;
using UnityEngine;

public class EnemyHeadAttack : MonoBehaviour
{
    public EnemyProjectile projectilePrefab; // Uses your EnemyProjectile class
    public float shootCooldown = 5f;        // Cooldown before next attack
    public float shootDelay = 1f;           // Delay before shooting
    public float projectileSpeed = 5f;      // Speed of projectiles
    public int projectileCount = 10;        // Number of projectiles
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            // Teleport head to the center of the screen
            transform.position = new Vector3(0, 0, originalPosition.z);

            // Wait before shooting
            yield return new WaitForSeconds(shootDelay);

            // Shoot projectiles in a circular pattern
            ShootInCircle();

            // Wait for cooldown
            yield return new WaitForSeconds(shootCooldown);

            // Move back to original position
            transform.position = originalPosition;
        }
    }

    private void ShootInCircle()
    {
        float angleStep = 360f / projectileCount;
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * angleStep;
            Vector3 shootDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);

            // Instantiate projectile
            EnemyProjectile newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Set its direction and speed
            newProjectile.direction = shootDirection;
            newProjectile.speed = projectileSpeed;
        }
    }
}
