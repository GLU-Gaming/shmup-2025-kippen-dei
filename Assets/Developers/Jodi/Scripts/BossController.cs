using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 1000;
    float currentHealth;

    [Header("Attack Settings")]
    public float timeBetweenAttacks = 5f;
    float attackTimer;

    [Header("Phase Settings")]
    [SerializeField] int currentPhase = 1;

    [Header("Attack Systems")]
    public SpawnDrones droneSpawner;
    public LaserShoot laserShooter;

    void Start()
    {
        currentHealth = maxHealth;
        attackTimer = timeBetweenAttacks;
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;
        
        if(attackTimer <= 0)
        {
            ChooseAttack();
            attackTimer = timeBetweenAttacks;
        }
        
        CheckPhase();
    }
    

    void ChooseAttack()
    {
        switch(currentPhase)
        {
            case 1:
                Phase1Attacks();
                break;
            case 2:
                Phase2Attacks();
                break;
            case 3:
                Phase3Attacks();
                break;
        }
    }

    void CheckPhase()
    {
        if(currentHealth < maxHealth * 0.75f && currentPhase == 1)
        {
            currentPhase = 2;
            Debug.Log("Entering Phase 2!");
        }
        else if(currentHealth < maxHealth * 0.5f && currentPhase == 2)
        {
            currentPhase = 3;
            Debug.Log("Final Phase!");
        }
    }

    void Phase1Attacks()
    {
        if(Random.value > 0.5f)
        {
            droneSpawner.SpawnSingleDrone();
        }
        else
        {
            laserShooter.FireLaser();
        }
    }

    void Phase2Attacks()
    {
        droneSpawner.SpawnDroneSwarm(3);
        laserShooter.FireLaser();
    }

    void Phase3Attacks()
    {
        StartCoroutine(FinalPhaseAttack());
    }

    IEnumerator FinalPhaseAttack()
    {
        laserShooter.FireContinuousBeam(3f);
        yield return new WaitForSeconds(1f);
        droneSpawner.SpawnDroneSwarm(5);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Projectile projectile = collision.collider.GetComponent<Projectile>();
            if (projectile != null)
            {
                TakeDamage(projectile.damage);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage; 
        
        Debug.Log($"Boss took {damage} damage! Remaining health: {currentHealth}");
        
        if (currentHealth <= 0)
        {
            laserShooter.AbortAttack();
            Destroy(gameObject); 
        }
    }
}