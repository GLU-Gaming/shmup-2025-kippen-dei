using UnityEngine;

public class Damage : MonoBehaviour
{
    [Header("Health Settings")]
    public float hp = 100f; // Health points

    //When colliding with a bullet, take damage
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Projectile projectile = collision.collider.GetComponent<Projectile>();
            if (projectile != null)
            {
                TakeDamage(projectile.damage);
            }
        }
    }

    //when taking damage, subtract the damage from the health points
    void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    //when health points reach 0, destroy the object
    void Die()
    {
        Destroy(gameObject);
    }
}