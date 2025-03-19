using UnityEngine;

public class BaseProjectile : Projectile // Inherits from the base class
{
    protected override void Move()
    {
        // Use local direction with doubled speed
        transform.Translate(Vector3.right * (speed * 2 * Time.deltaTime), Space.Self);
    }
}