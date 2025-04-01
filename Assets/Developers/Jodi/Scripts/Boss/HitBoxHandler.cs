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
                boss.TriggerFlicker(boss.weakPointFlickerColor, boss.weakPointFlickerDuration);

                // Add particle for weak point hit
                if (collision.contacts.Length > 0)
                {
                    boss.PlayHitParticle(boss.weakPointHitParticle, collision.contacts[0].point);
                }
            }
        }
    }
}