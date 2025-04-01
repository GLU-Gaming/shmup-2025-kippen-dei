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
    private BossController bossController;

    void Start()
    {
        bossController = GetComponent<BossController>();
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
        bossController.SetDashing(true);
        originalPosition = transform.position;

        // Calculate horizontal dash direction
        Vector3 dashDirection = (DashTransform.position - transform.position).normalized;
        dashDirection.y = 0; // Remove vertical component
        Vector3 targetPosition = originalPosition + dashDirection * dashDistance;

        // Show warning indicator
        ShowWarning(targetPosition, dashDirection);
        StartCoroutine(AnimateWarning());
        StartCoroutine(FlashWarning());
        yield return new WaitForSeconds(warningDuration);
        HideWarning();

        // Perform dash movement
        yield return StartCoroutine(MoveToPosition(targetPosition));
        
        // Return to X=8 position
        yield return StartCoroutine(ReturnToBasePosition());
        
        isDashing = false;
        bossController.SetDashing(false);
    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        // Maintain vertical position from BossController's movement
        Vector3 finalTarget = new Vector3(target.x, transform.position.y, target.z);
        
        while (Vector3.Distance(transform.position, finalTarget) > 0.01f)
        {
            // Only modify X position, Y is handled by BossController
            transform.position = Vector3.MoveTowards(
                transform.position,
                finalTarget,
                dashSpeed * Time.deltaTime
            );
            yield return null;
        }
    }

    IEnumerator ReturnToBasePosition()
    {
        Vector3 returnPosition = new Vector3(8f, transform.position.y, transform.position.z);
        
        while (Vector3.Distance(transform.position, returnPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                returnPosition,
                dashSpeed * Time.deltaTime
            );
            yield return null;
        }
    }

    // Rest of the original methods remain unchanged
    void ShowWarning(Vector3 targetPos, Vector3 dashDirection)
    {
        if (warningIndicator != null)
        {
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
            float scale = Mathf.Lerp(minScale, maxScale, 
                Mathf.PingPong(elapsed * pulseSpeed, 1f));
            warningIndicator.transform.localScale = originalScale * scale;
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
}