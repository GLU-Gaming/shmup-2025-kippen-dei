using UnityEngine;

public class EnemyBaseProjectile : Projectile
{
    protected override void Start()
    {
        base.Start();
        Destroy(GetComponent<Rigidbody>()); // Remove leftover Rigidbody
    }

    void Update()
    {
        // Move left based on the projectile's local rotation
        transform.Translate(Vector3.right * (speed * 2 * Time.deltaTime));
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

            if (screenShake != null)
            {
                screenShake.Shake(0.05f, 0.3f);
            }

            Destroy(gameObject);
        }
    }
}