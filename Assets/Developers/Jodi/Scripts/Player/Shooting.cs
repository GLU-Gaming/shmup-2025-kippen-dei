using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject defaultProjectile;
    public GameObject currentProjectile;
    public Transform shootPoint;
    public float baseCooldown = 0.5f;
    public float powerupDuration = 5f;
    public float warningTime = 2f; 

    private float nextShootTime = 0f;
    private Coroutine resetRoutine;
    private float powerupExpireTime;

    void Start()
    {
        currentProjectile = defaultProjectile;
    }

    public void ChangeProjectile(GameObject newProjectile)
    {
        if (resetRoutine != null)
        {
            StopCoroutine(resetRoutine);
        }
        
        currentProjectile = newProjectile;
        powerupExpireTime = Time.time + powerupDuration;
        resetRoutine = StartCoroutine(ResetProjectileAfterDelay(powerupDuration));
    }

    IEnumerator ResetProjectileAfterDelay(float delay)
    {
        yield return new WaitForSeconds(10f);
        ResetToDefault();
    }

    public void ResetToDefault()
    {
        currentProjectile = defaultProjectile;
        resetRoutine = null;
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
        
        // Add flicker effect if in warning period
        if (Time.time > powerupExpireTime - warningTime)
        {
            FlickerEffect flicker = newProjectile.GetComponent<FlickerEffect>();
            if (flicker != null)
            {
                flicker.StartFlicker(warningTime);
            }
        }

        Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
        float cooldown = projectileComponent != null ? projectileComponent.cooldownTime : baseCooldown;
        nextShootTime = Time.time + cooldown;
    }
}