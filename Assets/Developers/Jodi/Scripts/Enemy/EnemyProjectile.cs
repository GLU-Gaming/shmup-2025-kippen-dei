using UnityEngine;

public class EnemyProjectile : Projectile
{
    public Vector3 direction;

    protected override void Start()
    {
        base.Start();

        if (direction != Vector3.zero)
        {
            initialDirection = direction.normalized;
        }
        else
        {
            initialDirection = Vector3.right;
        }
    }

    protected override void Move()
    {
        transform.Translate(initialDirection * (speed * Time.deltaTime), Space.World);
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