using UnityEngine;

public class LaserDamage : MonoBehaviour

{
    public float damage = 1f;
    public ScreenShake screenShake; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerComponent = other.GetComponent<Player>();
            if (playerComponent != null)
            {
                playerComponent.TakeDamage(damage);
            }
            
            // Trigger screen shake when the projectile hits the player
            if (screenShake != null)
            {
                screenShake.Shake();  
            }
        }
    }
}