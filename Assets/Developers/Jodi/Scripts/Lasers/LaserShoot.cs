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
    
    [Header("Laser Positioning")]
    public Vector2 laserDirection = Vector2.right;
    public float laserLength = 10f;
    public float xOffset = 0f; // Custom X position
    public float yOffset = 0f; // Custom Y position
    public float laserThickness = 0.2f;

    private bool isCharging = false;
    private bool isFiring = false;
    private List<LaserComponents> laserComponents = new List<LaserComponents>();

    private class LaserComponents
    {
        public LineRenderer lineRenderer;
        public BoxCollider collider;
        public GameObject gameObject;
    }

    void Start()
    {
        InitializeLasers();
    }

    void InitializeLasers()
    {
        foreach (GameObject beam in laserBeams)
        {
            LaserComponents lc = new LaserComponents
            {
                gameObject = beam,
                lineRenderer = beam.GetComponent<LineRenderer>(),
                collider = beam.GetComponent<BoxCollider>()
            };

            if (lc.lineRenderer != null)
            {
                lc.lineRenderer.positionCount = 2;
            }

            if (lc.collider == null)
            {
                lc.collider = beam.AddComponent<BoxCollider>();
                lc.collider.isTrigger = true;
            }

            laserComponents.Add(lc);
            beam.SetActive(false);
        }
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

        foreach (var lc in laserComponents)
        {
            lc.gameObject.SetActive(true);
        }

        float endTime = Time.time + laserDuration;
        while (Time.time < endTime)
        {
            UpdateLaserPositions();
            yield return null;
        }

        foreach (var lc in laserComponents)
        {
            lc.gameObject.SetActive(false);
        }
        isFiring = false;
    }

    void UpdateLaserPositions()
    {
        // Calculate base position with offsets
        Vector3 basePosition = transform.position + new Vector3(xOffset, yOffset, 0);
        Vector3 laserStart = basePosition;
        Vector3 laserEnd = basePosition + new Vector3(laserDirection.x, laserDirection.y, 0) * laserLength;

        foreach (var lc in laserComponents)
        {
            // Update LineRenderer
            if (lc.lineRenderer != null)
            {
                lc.lineRenderer.SetPosition(0, laserStart);
                lc.lineRenderer.SetPosition(1, laserEnd);
            }

            // Update Collider
            if (lc.collider != null)
            {
                Vector3 colliderCenter = (laserStart + laserEnd) / 2f;
                float colliderLength = Vector3.Distance(laserStart, laserEnd);
                float colliderAngle = Mathf.Atan2(laserDirection.y, laserDirection.x) * Mathf.Rad2Deg;

                lc.collider.center = lc.gameObject.transform.InverseTransformPoint(colliderCenter);
                lc.collider.size = new Vector3(colliderLength, laserThickness, 0.1f);
                lc.collider.transform.rotation = Quaternion.Euler(0, 0, colliderAngle);
            }
        }
    }
    
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
        foreach (var lc in laserComponents)
        {
            lc.gameObject.SetActive(false);
        }
        foreach (var chargeEffect in chargeEffects)
        {
            chargeEffect.StopCharge();
        }
        isCharging = false;
        isFiring = false;
    }
}