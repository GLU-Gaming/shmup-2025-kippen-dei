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

    

    void Start()
    {
        currentHealth = maxHealth;
        updateHealthBar();
        attackTimer = timeBetweenAttacks;
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;
        updateHealthBar();
        
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
        if (Random.value > 0.8f)
        {
            DashAttack.Dash();
        }
        else
        {
            droneSpawner.SpawnDroneSwarm(3);
            laserShooter.FireLaser();
        }
       
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
        yield return new WaitForSeconds(1f);
        if(Random.value > 0.6f)
        {
            DashAttack.Dash();
        }
            
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

    void updateHealthBar()
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        updateHealthBar();
        
        Debug.Log($"Boss took {damage} damage! Remaining health: {currentHealth}");
        
        if (currentHealth <= 0)
        {
            laserShooter.AbortAttack();
            Destroy(gameObject); 
        }
    }
}