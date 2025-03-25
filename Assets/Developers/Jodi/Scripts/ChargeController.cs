using UnityEngine;

public class ChargeController : MonoBehaviour
{
    public float chargeTime = 2f; // Time to charge the laser
    public float maxScale = 2f; // How big the charge effect gets

    private Vector3 originalScale;
    private float currentCharge = 0f;

    void Start()
    {
        originalScale = transform.localScale;
        gameObject.SetActive(false); 
    }

    void Update()
    {
        // Scale up the charge effect over time
        if (currentCharge < chargeTime)
        {
            currentCharge += Time.deltaTime;
            float scale = Mathf.Lerp(0, maxScale, currentCharge / chargeTime);
            transform.localScale = originalScale * scale;
        }
    }

    public void StartCharge()
    {
        gameObject.SetActive(true);
        currentCharge = 0f;
    }

    public void StopCharge()
    {
        gameObject.SetActive(false);
        transform.localScale = originalScale;
    }
}