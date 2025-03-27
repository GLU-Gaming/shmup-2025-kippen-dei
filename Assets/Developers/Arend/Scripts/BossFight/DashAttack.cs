using UnityEngine;
using System.Collections;

public class DashAttack : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashDistance = 5f;
    public float dashSpeed = 15f;
    public Transform DashTransform; 

    private Vector3 originalPosition;
    private bool isDashing = false;

    public IEnumerator Dash()
    {
        if (isDashing || DashTransform == null) yield break;
        
        isDashing = true;
        originalPosition = transform.position;

        // Calculate dash direction toward player
        Vector3 dashDirection = (DashTransform.position - transform.position).normalized;
        Vector3 targetPosition = originalPosition + dashDirection * dashDistance;

        // Dash forward
        yield return StartCoroutine(MoveToPosition(targetPosition));

        // Return to original position
        yield return StartCoroutine(MoveToPosition(originalPosition));

        isDashing = false;
    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                dashSpeed * Time.deltaTime
            );
            yield return null;
        }
        transform.position = target; // Snap to final position
    }
}