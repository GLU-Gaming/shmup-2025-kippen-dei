using UnityEngine;

public class EnemyProjectile : Projectile
{
    public Vector3 direction; // Direction the enemy projectile will move in

    protected override void Start()
    {
        base.Start();
        if (direction != Vector3.zero)
        {
            initialDirection = direction.normalized;
        }
    }

    protected override void Move()
    {
        transform.Translate(initialDirection * (speed * Time.deltaTime), Space.World);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Apply damage to the player
            Player playerComponent = collision.collider.GetComponent<Player>();
            if (playerComponent != null)
            {
                // Player take damage(Not implemented)
                //playerComponent.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}