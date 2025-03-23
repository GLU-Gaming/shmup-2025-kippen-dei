using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Vector3 originalPos;
    private Camera mainCamera;

    public float shakeAmount = 0.05f;  
    public float shakeDuration = 0.3f;

    private float shakeTimeRemaining;

    void Start()
    {
        mainCamera = Camera.main;
        originalPos = mainCamera.transform.localPosition;
    }

    void Update()
    {
        if (shakeTimeRemaining > 0)
        {
            // Shake the camera position by a small amount
            mainCamera.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeTimeRemaining -= Time.deltaTime;
        }
        else
        {
            // Reset the camera position once shaking is done
            mainCamera.transform.localPosition = originalPos;
        }
    }
    
    public void Shake(float amount, float duration)
    {
        shakeAmount = amount;  
        shakeDuration = duration;
        shakeTimeRemaining = duration;
    }
}