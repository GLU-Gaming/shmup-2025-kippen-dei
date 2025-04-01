using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 1000;
    float currentHealth;
    public Image healthBarFill;

    [Header("Attack Settings")]
    public float timeBetweenAttacks = 5f;
    float attackTimer;

    [Header("Phase Settings")]
    [SerializeField] int currentPhase = 1;

    [Header("Attack Systems")]
    public SpawnDrones droneSpawner;
    public LaserShoot laserShooter;
    public LaserShootAbove laserShooterAbove; 
    public LaserShootAbove2 laserShooterAbove2; 
    public DashAttack DashAttack;

    bool isAttacking = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        attackTimer = timeBetweenAttacks;
    }

    void Update()
    {
        if (!isAttacking)
            attackTimer -= Time.deltaTime;

        UpdateHealthBar();

        if (attackTimer <= 0 && !isAttacking)
        {
            attackTimer = timeBetweenAttacks;
            StartCoroutine(PerformAttack());
        }

        CheckPhase();
    }

    IEnumerator PerformAttack()
    {
        isAttacking = true;
        yield return StartCoroutine(ChooseAttack());
        isAttacking = false;
    }

    IEnumerator ChooseAttack()
    {
        switch(currentPhase)
        {
            case 1:
                Phase1Attacks();
                break;
            case 2:
                yield return StartCoroutine(Phase2Attacks());
                break;
            case 3:
                yield return StartCoroutine(Phase3Attacks());
                break;
        }
    }

    void CheckPhase()
    {
        if (currentHealth < maxHealth * 0.75f && currentPhase == 1)
        {
            currentPhase = 2;
            Debug.Log("Entering Phase 2!");
        }
        else if (currentHealth < maxHealth * 0.5f && currentPhase == 2)
        {
            currentPhase = 3;
            timeBetweenAttacks *= 0.5f;

            // Phase 3 laser speed modifications
            float phase3ChargeTime = 2.0f; // 50% faster charging
            UpdateLaserChargeTime(laserShooter, phase3ChargeTime);
            UpdateLaserChargeTime(laserShooterAbove, phase3ChargeTime);
            UpdateLaserChargeTime(laserShooterAbove2, phase3ChargeTime);

            Debug.Log("Final Phase!");
        }
    }

    // Laser system updates
    void UpdateLaserChargeTime(LaserShoot laserSystem, float newChargeTime)
    {
        if (laserSystem != null)
        {
            laserSystem.chargeTime = newChargeTime;
            foreach (ChargeController chargeEffect in laserSystem.chargeEffects)
            {
                chargeEffect.chargeTime = newChargeTime;
            }
        }
    }

    void UpdateLaserChargeTime(LaserShootAbove laserSystem, float newChargeTime)
    {
        if (laserSystem != null)
        {
            laserSystem.chargeTime = newChargeTime;
            foreach (ChargeController chargeEffect in laserSystem.chargeEffects)
            {
                chargeEffect.chargeTime = newChargeTime;
            }
        }
    }

    void UpdateLaserChargeTime(LaserShootAbove2 laserSystem, float newChargeTime)
    {
        if (laserSystem != null)
        {
            laserSystem.chargeTime = newChargeTime;
            foreach (ChargeController chargeEffect in laserSystem.chargeEffects)
            {
                chargeEffect.chargeTime = newChargeTime;
            }
        }
    }

    void Phase1Attacks()
    {
        if (Random.value > 0.5f)
            droneSpawner.SpawnSingleDrone();
        else
            laserShooter.FireLaser();
    }
    
    IEnumerator Phase2Attacks()
    {
        if (Random.value > 0.7f)
        {
            laserShooter.AbortAttack();
            yield return StartCoroutine(DashAttack.Dash());
            attackTimer = timeBetweenAttacks * 0.5f;
        }
        else if (Random.value > 0.7f)
        {
            laserShooterAbove.FireLaser();
        }
        else if (Random.value > 0.7f)
        {
            laserShooterAbove2.FireLaser();
        }
        else
        {
            droneSpawner.SpawnDroneSwarm(3);
            laserShooter.FireLaser();
        }
    }

    IEnumerator Phase3Attacks()
    {
        yield return StartCoroutine(FinalPhaseAttack());
    }

    IEnumerator FinalPhaseAttack()
    {
        float rand = Random.value;
    
        if (rand > 0.7f)
        {
            laserShooter.AbortAttack();
            laserShooterAbove.AbortAttack();
            yield return StartCoroutine(DashAttack.Dash());
            attackTimer = timeBetweenAttacks * 0.4f; 
        }
        else if (rand > 0.5f)
        {
            laserShooter.FireLaser();
        }
        else if (rand > 0.3f)
        {
            laserShooterAbove.FireLaser();
        }
        else
        {
            droneSpawner.SpawnDroneSwarm(5);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Projectile projectile = collision.collider.GetComponent<Projectile>();
            if (projectile != null)
                TakeDamage(projectile.damage);
        }
    }

    void UpdateHealthBar()
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();
        Debug.Log($"Boss took {damage} damage! Remaining health: {currentHealth}");

        if (currentHealth <= 0)
        {
            laserShooter.AbortAttack();
            laserShooterAbove.AbortAttack();
            Destroy(gameObject);
            SceneManager.LoadScene("Credits");
        }
    }
}