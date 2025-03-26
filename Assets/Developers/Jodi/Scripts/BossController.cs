using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
            Debug.Log("Final Phase!");
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
        laserShooter.FireContinuousBeam(3f);
        yield return new WaitForSeconds(1f);
        droneSpawner.SpawnDroneSwarm(5);
        yield return new WaitForSeconds(1f);
        if (Random.value > 0.5f)
        {
            laserShooter.AbortAttack();
            yield return StartCoroutine(DashAttack.Dash());
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
            Destroy(gameObject);
        }
    }
}