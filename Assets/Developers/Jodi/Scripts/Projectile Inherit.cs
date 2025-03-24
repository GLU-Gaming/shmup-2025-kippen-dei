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

    public ScreenShake screenShake; 

    protected virtual void Start()
    {
        Destroy(gameObject, lifeTime);
        initialDirection = transform.right; // Store the initial direction
        initialRotation = transform.rotation; // Store the initial rotation

        
        if (screenShake == null)
        {
            screenShake = FindObjectOfType<ScreenShake>();
        }
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

    // Trigger screen shake when the projectile collides with an enemy
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            // Trigger screen shake when hitting an enemy
            if (screenShake != null)
            {
                screenShake.Shake(0.05f, 0.3f);  // Customize these values as needed
            }

            Destroy(gameObject); // Destroy the projectile
        }
    }
}