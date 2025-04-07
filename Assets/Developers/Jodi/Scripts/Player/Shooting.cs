using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject defaultProjectile;
    public GameObject currentProjectile;
    public Transform shootPoint;
    public float baseCooldown = 0.5f;

    private float nextShootTime = 0f;

    void Start()
    {
        currentProjectile = defaultProjectile;
    }

    public void ChangeProjectile(GameObject newProjectile)
    {
        currentProjectile = newProjectile;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextShootTime)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject newProjectile = Instantiate(currentProjectile, shootPoint.position, shootPoint.rotation);
        Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
        
        float cooldown = projectileComponent != null ? projectileComponent.cooldownTime : baseCooldown;
        nextShootTime = Time.time + cooldown;
    }
}