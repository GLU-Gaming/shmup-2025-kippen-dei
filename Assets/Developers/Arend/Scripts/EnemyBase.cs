using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{

    [Header("Health Settings")]
    public float hp = 100f; // Health points

    

    public abstract void Move();

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
        FindObjectOfType<ScoreManager>().AddScore(10);
    }
}
