using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public int Damage = 1;
    public abstract void Move();

    protected void DamagePlayer()
    {
        // Logica om damage te doen
    }

    protected void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
