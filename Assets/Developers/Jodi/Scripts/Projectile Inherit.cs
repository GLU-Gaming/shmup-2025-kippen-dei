using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float damage = 1f;
    public float speed = 10f;
    public float lifeTime = 5f;
    public float cooldownTime = 0.3f;

    public Vector3 initialDirection;
    private Quaternion initialRotation;

    protected virtual void Start()
    {
        Destroy(gameObject, lifeTime);
        initialDirection = transform.right; // Store the initial direction
        initialRotation = transform.rotation; // Store the initial rotation
    }

    void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.rotation = initialRotation; // Ensure rotation remains constant
        transform.Translate(initialDirection * (speed * Time.deltaTime), Space.World);
    }

    // Destroy the projectile when it collides with an enemy
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}