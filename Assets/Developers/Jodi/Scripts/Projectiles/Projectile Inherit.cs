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
        initialDirection = transform.right;
        initialRotation = transform.rotation;

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
        transform.rotation = initialRotation;
        transform.Translate(initialDirection * (speed * Time.deltaTime), Space.World);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (screenShake != null)
            {
                screenShake.Shake();  
            }

            Destroy(gameObject);
        }
    }
}