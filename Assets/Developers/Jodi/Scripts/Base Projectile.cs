using UnityEngine;

public class BaseProjectile : Projectile
{
    protected override void Move()
    {
        transform.Translate(Vector3.right * (speed * 2 * Time.deltaTime)); //move right.
    }
}