using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile")]
    public float damage = 1f; // Damage of the projectile
    public float speed = 10f; // Speed of the projectile
    public float lifeTime = 5f; // Time before the projectile is destroyed
    public float cooldownTime = 0.3f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy the projectile after lifeTime seconds
    }
    
    void Update()
    {
        Move();
    }

    // Method to move the projectile
    protected virtual void Move()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));
    }
}