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

    [Header("Movement Settings")]
    public float verticalSpeed = 2f;
    public float verticalAmplitude = 2f;
    public float dashReturnSpeed = 10f;
    private float initialY;
    private float phaseOffset;
    private const float TARGET_X_POSITION = 8f;
    private bool isDashing = false;

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
    public DashAttack dashAttack;
    
    [Header("Damage Multipliers")]
    public float mainBodyDamageMultiplier = 0.1f;
    public float weakPointDamageMultiplier = 1.2f;


    private Transform player;
    private bool isAttacking;

    void Start()
    {
        currentHealth = maxHealth;
        initialY = transform.position.y;
        attackTimer = timeBetweenAttacks;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        phaseOffset = 0f;
        LockXPosition();
    }

    void Update()
    {
        HandleMovement();
        HandleAttacks();
        UpdateHealthBar();
        CheckPhase();
    }

    void LockXPosition()
    {
        transform.position = new Vector3(TARGET_X_POSITION, transform.position.y, transform.position.z);
    }

    void HandleMovement()
    {
        phaseOffset += verticalSpeed * Time.deltaTime;
        float newY = initialY + Mathf.Sin(phaseOffset) * verticalAmplitude;

        if (!isDashing)
        {
            // Maintain X position at 8 with smooth return
            float currentX = Mathf.MoveTowards(transform.position.x, TARGET_X_POSITION, dashReturnSpeed * Time.deltaTime);
            transform.position = new Vector3(currentX, newY, transform.position.z);
        }
        else
        {
            // Only update Y position during dash
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        // Face player direction
        if(player != null)
        {
            // Changed the order of subtraction to fix facing direction
            float xScale = Mathf.Abs(transform.localScale.x) * Mathf.Sign(transform.position.x - player.position.x);
            transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
        }
    }

    void HandleAttacks()
    {
        if (!isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                StartCoroutine(AttackRoutine());
                attackTimer = timeBetweenAttacks;
            }
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        
        switch(currentPhase)
        {
            case 1:
                Phase1Attack();
                break;
            case 2:
                yield return StartCoroutine(Phase2Attack());
                break;
            case 3:
                yield return StartCoroutine(Phase3Attack());
                break;
        }

        isAttacking = false;
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
            timeBetweenAttacks *= 0.7f;
            UpdateLaserChargeTimes(1f);
            Debug.Log("Final Phase!");
        }
    }

    public void SetDashing(bool dashingState)
    {
        isDashing = dashingState;
    }

    void UpdateLaserChargeTimes(float newChargeTime)
    {
        laserShooter.chargeTime = newChargeTime;
        laserShooterAbove.chargeTime = newChargeTime;
        laserShooterAbove2.chargeTime = newChargeTime;
        
        foreach (ChargeController cc in laserShooter.chargeEffects)
            cc.chargeTime = newChargeTime;
        foreach (ChargeController cc in laserShooterAbove.chargeEffects)
            cc.chargeTime = newChargeTime;
        foreach (ChargeController cc in laserShooterAbove2.chargeEffects)
            cc.chargeTime = newChargeTime;
    }

    void Phase1Attack()
    {
        if (Random.value > 0.5f)
            droneSpawner.SpawnSingleDrone();
        else
            laserShooter.FireLaser();
    }
    
    IEnumerator Phase2Attack()
    {
        if (Random.value > 0.7f)
        {
            yield return StartCoroutine(dashAttack.Dash());
        }
        else 
        {
            droneSpawner.SpawnDroneSwarm(3);
            laserShooter.FireLaser();
            laserShooterAbove.FireLaser();
        }
    }

    IEnumerator Phase3Attack()
    {
        if (Random.value > 0.5f)
        {
            laserShooter.FireContinuousBeam(3f);
            yield return new WaitForSeconds(3f);
        }
        else
        {
            droneSpawner.SpawnDroneSwarm(5);
            laserShooterAbove.FireLaser();
            laserShooterAbove2.FireLaser();
            yield return new WaitForSeconds(1f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile != null)
            {
                float calculatedDamage = projectile.damage * mainBodyDamageMultiplier;
                TakeDamage(calculatedDamage);
            }
        }
    }
    void UpdateHealthBar()
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Credits");
        }
    }
}