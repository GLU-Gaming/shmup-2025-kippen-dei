using UnityEngine;

public class Fish : EnemyBase
{
    [Header("Fish Settings")]
    public float speed = 2f;
    private bool movingLeft = true;

    void Update()
    {
        Move();
    }

    public override void Move()
    {
        transform.Translate(Vector2.right * (movingLeft ? -speed : speed) * Time.deltaTime);

        if (transform.position.x < -10)
        {
            movingLeft = !movingLeft;
        }
        if(transform.position.x > 11)
        {
            Destroy(gameObject);
        }
    }
}