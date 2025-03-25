using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("Movement Settings")]
    public float speed = 10f;
    public float maxX = 5f, maxY = 3f;

    [Header("Player Health Settings")]
    public float maxHealth = 9f;
    public float playerHp = 9f;
    public float invincibilityDuration = 1f; 
    private bool isInvincible = false; 

    private MeshRenderer[] meshRenderers; 
    private Color originalColor;  
    
    public ScreenShake screenShake;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        if (meshRenderers.Length > 0)
        {
            originalColor = meshRenderers[0].material.color;
        }
    }

    void Start()
    {
        screenShake = Camera.main.GetComponent<ScreenShake>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, moveY, 0) * (speed * Time.deltaTime);
        transform.position += move;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -maxX, maxX),
            Mathf.Clamp(transform.position.y, -maxY * 0.5f, maxY), 
            transform.position.z
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isInvincible && other.CompareTag("Enemy"))
        {
            TakeDamage(1f);
            if (screenShake != null)
            {
                screenShake.Shake();  
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible) return;

        playerHp -= damage;
        if (playerHp <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityFrames());
        }
    }

    void Die()
    {
        Debug.Log("Player has died.");
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }

    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        
        float flashEndTime = Time.time + invincibilityDuration;
        while (Time.time < flashEndTime)
        {
            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.material.color = Color.red;
            }
            yield return new WaitForSeconds(0.1f);

            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.material.color = originalColor;
            }
            yield return new WaitForSeconds(0.1f);
        }

        isInvincible = false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BossBattle")
        {
            transform.position = Vector3.zero;
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}