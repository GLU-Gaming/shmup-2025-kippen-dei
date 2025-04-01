using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserShoot : MonoBehaviour
{
    [Header("Laser Settings")]
    public List<GameObject> laserBeams;
    public List<ChargeController> chargeEffects;
    public float chargeTime = 2f;
    public float laserDuration = 1f;

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
        foreach (var chargeEffect in chargeEffects)
        {
            chargeEffect.StartCharge();
        }
        yield return new WaitForSeconds(chargeTime);

        foreach (var chargeEffect in chargeEffects)
        {
            chargeEffect.StopCharge();
        }
        isCharging = false;
        isFiring = true;

        foreach (var laserBeam in laserBeams)
        {
            laserBeam.SetActive(true);
        }
        yield return new WaitForSeconds(laserDuration);

        foreach (var laserBeam in laserBeams)
        {
            laserBeam.SetActive(false);
        }
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
        foreach (var laserBeam in laserBeams)
        {
            laserBeam.SetActive(false);
        }
        foreach (var chargeEffect in chargeEffects)
        {
            chargeEffect.StopCharge();
        }
        isCharging = false;
        isFiring = false;
    }
}