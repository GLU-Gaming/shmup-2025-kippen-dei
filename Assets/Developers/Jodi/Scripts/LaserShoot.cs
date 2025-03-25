using UnityEngine;
using System.Collections;

public class LaserShoot : MonoBehaviour
{
    [Header("Laser Settings")]
    public GameObject laserBeam;
    public float chargeTime = 2f;
    public float laserDuration = 1f;
    public ChargeController chargeEffect;

    private bool isCharging = false;
    private bool isFiring = false;

    public void FireLaser()
    {
        if(!isCharging && !isFiring)
        {
            StartCoroutine(ShootLaserRoutine());
        }
    }

    public void FireContinuousBeam(float duration)
    {
        StartCoroutine(ContinuousBeam(duration));
    }

    IEnumerator ShootLaserRoutine()
    {
        isCharging = true;
        chargeEffect.StartCharge();
        yield return new WaitForSeconds(chargeTime);

        chargeEffect.StopCharge();
        isCharging = false;
        isFiring = true;

        laserBeam.SetActive(true);
        yield return new WaitForSeconds(laserDuration);

        laserBeam.SetActive(false);
        isFiring = false;
    }

    IEnumerator ContinuousBeam(float duration)
    {
        float endTime = Time.time + duration;
        while(Time.time < endTime)
        {
            FireLaser();
            yield return new WaitForSeconds(laserDuration + chargeTime);
        }
    }
    public void AbortAttack()
    {
        StopAllCoroutines();
        laserBeam.SetActive(false);
        chargeEffect.StopCharge();
        isCharging = false;
        isFiring = false;
    }

}