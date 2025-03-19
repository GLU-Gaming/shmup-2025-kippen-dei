using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected GameManagerA gamaManager;

    public abstract void Move();

    protected virtual void Start()
    {
        gamaManager = FindAnyObjectByType<GameManagerA>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("player"))
        {


            Destroy(gameObject);
        }
    }
}
