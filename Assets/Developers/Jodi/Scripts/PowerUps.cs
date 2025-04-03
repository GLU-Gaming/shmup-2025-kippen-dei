using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class PowerUpPickup : MonoBehaviour
{
    [Header("Settings")]
    public string powerUpName = "Power-Up";
    public GameObject projectilePrefab;
    public float moveSpeed = 2f;
    public float flickerSpeed = 5f;
    public Vector3 textOffset = new Vector3(0, 0.5f, 0);

    [Header("References")]
    [SerializeField] private TextMeshProUGUI powerUpText;
    private MeshRenderer meshRenderer;
    private Rigidbody rb;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        
        // Set up text
        if (powerUpText != null)
        {
            powerUpText.text = powerUpName;
            // Position text relative to parent
            powerUpText.transform.localPosition = textOffset;
        }

        // Configure physics
        rb.useGravity = false;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    void Update()
    {
        // Flicker effect for the pickup object
        if (meshRenderer != null)
        {
            float alpha = Mathf.Abs(Mathf.Sin(Time.time * flickerSpeed));
            Color newColor = meshRenderer.material.color;
            newColor.a = alpha;
            meshRenderer.material.color = newColor;
        }

        // Destroy when below camera view
        if (transform.position.y < Camera.main.transform.position.y - 10f)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        // Move downward
        rb.MovePosition(transform.position + Vector3.down * (moveSpeed * Time.fixedDeltaTime));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Shooting shooting = other.GetComponent<Shooting>();
            if (shooting != null)
            {
                shooting.ChangeProjectile(projectilePrefab);
            }
            Destroy(gameObject);
        }
    }
}