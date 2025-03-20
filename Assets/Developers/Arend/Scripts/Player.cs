using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 10f; // Movement speed
    public float maxX = 5f, maxY = 3f; // Movement boundaries

    [Header("Player Health Settings")]
    public float playerHp = 9f;

    void Update()
    {
        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Move the plane directly
        Vector3 move = new Vector3(moveX, moveY, 0) * speed * Time.deltaTime;
        transform.position += move;

        // Clamp position to stay within boundaries
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -maxX, maxX),
            Mathf.Clamp(transform.position.y, -maxY, maxY),
            transform.position.z
        );
    }

    public void TakeDamage(float damage)
    {
        playerHp -= damage;
        if (playerHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle player death logic here
        Debug.Log("Player has died.");
        // Add scene reload or game over logic
    }
}
