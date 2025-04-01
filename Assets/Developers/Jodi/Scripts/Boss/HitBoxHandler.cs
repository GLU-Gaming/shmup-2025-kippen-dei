using UnityEngine;

public class HitboxHandler : MonoBehaviour
{
    private BossController boss;

    void Start()
    {
        boss = GetComponentInParent<BossController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile != null && boss != null)
            {
     
                float calculatedDamage = projectile.damage * boss.weakPointDamageMultiplier;
                boss.TakeDamage(calculatedDamage);
            }
        }
    }
}