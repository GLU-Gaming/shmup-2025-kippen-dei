using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Vector3 originalPos;
    private Camera mainCamera;

    [Header("Default Shake Settings")]
    public float defaultShakeAmount = 0.05f;  
    public float defaultShakeDuration = 0.3f;

    private float shakeTimeRemaining;
    private float shakeAmount;

    void Start()
    {
        mainCamera = Camera.main;
        originalPos = mainCamera.transform.localPosition;
    }

    void Update()
    {
        if (shakeTimeRemaining > 0)
        {
            mainCamera.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeTimeRemaining -= Time.deltaTime;
        }
        else
        {
            mainCamera.transform.localPosition = originalPos;
        }
    }
    
    public void Shake()
    {
        Shake(defaultShakeAmount, defaultShakeDuration);
    }

    // Custom Shake
    public void Shake(float amount, float duration)
    {
        shakeAmount = amount;  
        shakeTimeRemaining = duration;
    }
}