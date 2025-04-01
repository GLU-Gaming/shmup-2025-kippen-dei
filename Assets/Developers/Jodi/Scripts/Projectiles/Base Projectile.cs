using UnityEngine;

public class BaseProjectile : Projectile
{
    private Rigidbody _rb;

    protected override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody>();
        
        // Set velocity in the direction the projectile is facing
        _rb.linearVelocity = transform.right * (speed * 2);
    }
}