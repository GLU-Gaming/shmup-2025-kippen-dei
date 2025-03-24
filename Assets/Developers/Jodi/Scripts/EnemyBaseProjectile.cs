using UnityEngine;

public class EnemyBaseProjectile : Projectile
{
    private Rigidbody _rb;

    protected override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody>();

        // Set velocity to the left direction the projectile is facing
        _rb.linearVelocity = -transform.right * (speed * 2);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerComponent = other.GetComponent<Player>();
            if (playerComponent != null)
            {
                playerComponent.TakeDamage(damage);
            }

            // Trigger screen shake when hitting the player
            if (screenShake != null)
            {
                screenShake.Shake(0.05f, 0.3f); // Customize these values as needed
            }

            Destroy(gameObject); // Destroy the projectile
        }
    }
}