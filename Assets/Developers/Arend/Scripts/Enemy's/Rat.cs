using UnityEngine;

public class Rat : EnemyBase
{
    [Header("Rat Movement Settings")]
    public float speed = 5f;        // Snelheid van de vijand
    public float zigzagInterval = 1f; // Hoe vaak de richting verandert

    private Vector3 direction;
    private float timer;

    void Start()
    {
        // Begin met beweging naar beneden
        direction = Vector3.down;
    }

    void Update()
    {
        Move();
        CheckPosition();
    }

    private void CheckPosition()
    {
        // Vernietig de vijand als deze buiten het scherm is
        if (transform.position.x <= -18f)
        {
            Destroy(gameObject);
        }
    }

    public override void Move()
    {
        // Beweeg de vijand naar links en zigzag naar boven of beneden
        transform.position += new Vector3(-speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0f);

        // Timer updaten
        timer += Time.deltaTime;

        // Verander richting op basis van het interval
        if (timer >= zigzagInterval)
        {
            // Reset timer en verander richting
            timer = 0f;
            if (direction == Vector3.down)
            {
                direction = Vector3.up;  // Beweeg omhoog
            }
            else
            {
                direction = Vector3.down; // Beweeg omlaag
            }
        }
    }
}
