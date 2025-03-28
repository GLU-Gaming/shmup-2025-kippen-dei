using UnityEngine;
using System.Collections;

public class DashAttack : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashDistance = 5f;
    public float dashSpeed = 15f;
    public Transform DashTransform;
    public float warningDuration = 1f;
    public float warningOffset = 0.5f; 
    public SpriteRenderer warningIndicator;

    [Header("Animation Settings")]
    public float pulseSpeed = 5f;
    public float minScale = 0.8f;
    public float maxScale = 1.2f;
    public Color startColor = Color.yellow;
    public Color endColor = Color.red;

    private Vector3 originalPosition;
    private bool isDashing = false;
    private Vector3 originalScale;
    private Color originalColor;

    void Start()
    {
        if (warningIndicator != null)
        {
            warningIndicator.enabled = false;
            originalScale = warningIndicator.transform.localScale;
            originalColor = warningIndicator.color;
        }
    }

    public IEnumerator Dash()
    {
        if (isDashing || DashTransform == null) yield break;

        isDashing = true;
        originalPosition = transform.position;

        // Calculate dash direction and target
        Vector3 dashDirection = (DashTransform.position - transform.position).normalized;
        Vector3 targetPosition = originalPosition + dashDirection * dashDistance;

        // Calculate warning position with right offset
        Vector3 warningPosition = targetPosition + 
            Vector3.Cross(dashDirection, Vector3.up) * warningOffset;

        // Show and animate warning
        ShowWarning(warningPosition, dashDirection);
        StartCoroutine(AnimateWarning());
        StartCoroutine(FlashWarning());
        yield return new WaitForSeconds(warningDuration);
        HideWarning();

        // Perform dash
        yield return StartCoroutine(MoveToPosition(targetPosition));
        yield return StartCoroutine(MoveToPosition(originalPosition));

        isDashing = false;
    }

    void ShowWarning(Vector3 targetPos, Vector3 dashDirection)
    {
        if (warningIndicator != null)
        {
            // Position with offset and rotation
            warningIndicator.transform.position = targetPos;
            warningIndicator.transform.rotation = Quaternion.LookRotation(
                Vector3.forward,
                dashDirection
            );
            warningIndicator.enabled = true;
        }
    }

    void HideWarning()
    {
        if (warningIndicator != null)
        {
            warningIndicator.enabled = false;
            warningIndicator.transform.localScale = originalScale;
            warningIndicator.color = originalColor;
        }
    }

    IEnumerator AnimateWarning()
    {
        float elapsed = 0f;
        while (elapsed < warningDuration)
        {
            // Pulse animation
            float scale = Mathf.Lerp(minScale, maxScale, 
                Mathf.PingPong(elapsed * pulseSpeed, 1f));
            warningIndicator.transform.localScale = originalScale * scale;

            // Color transition
            warningIndicator.color = Color.Lerp(startColor, endColor, 
                elapsed / warningDuration);

            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FlashWarning()
    {
        float flashInterval = 0.1f;
        int flashes = Mathf.FloorToInt(warningDuration / (flashInterval * 2));
        
        for (int i = 0; i < flashes; i++)
        {
            if (warningIndicator != null)
            {
                warningIndicator.enabled = !warningIndicator.enabled;
                yield return new WaitForSeconds(flashInterval);
            }
        }
        if (warningIndicator != null) warningIndicator.enabled = true;
    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                dashSpeed * Time.deltaTime
            );
            yield return null;
        }
        transform.position = target;
    }
}