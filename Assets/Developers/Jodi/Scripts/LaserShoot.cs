using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject laserBeam; 
    public float chargeTime = 2f; 
    public float laserDuration = 1f; 
    public ChargeController chargeEffect; 

    private bool isCharging = false;
    private bool isFiring = false;

    void Update()
    {
        if (!isCharging && !isFiring)
        {
            StartCoroutine(ShootLaserRoutine());
        }
    }

    System.Collections.IEnumerator ShootLaserRoutine()
    {
        isCharging = true;
        
        // Start charging effect
        chargeEffect.StartCharge();

        // Wait for charge time
        yield return new WaitForSeconds(chargeTime);

        // Stop charging and fire the laser
        chargeEffect.StopCharge();
        isCharging = false;
        isFiring = true;

        // Turn on the Line Renderer
        laserBeam.SetActive(true);

        // Keep the laser active for a duration
        yield return new WaitForSeconds(laserDuration);

        // Turn off the laser
        laserBeam.SetActive(false);
        isFiring = false;
    }
}