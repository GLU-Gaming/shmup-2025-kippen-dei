using System.Collections;
using UnityEngine;


public class DashAttack: MonoBehaviour
{
    [Header("DashAttack Settings")]
    public float dashSpeed;
    public Vector3 dashPoint;
    private Vector3 startPosition;

    public bool isDashing = false;

    private void Start()
    {
        startPosition = transform.position;
    }
    public void Dash()
    {
        if (!isDashing)
        {
            StartCoroutine(StartDash());
        }
        

    }
    IEnumerator StartDash()
    {
        isDashing = true;

        // Beweeg naar dashPoint
        while (Vector3.Distance(transform.position, dashPoint) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, dashPoint, dashSpeed * Time.deltaTime);
            yield return null; // Wacht tot de volgende frame
        }

        yield return new WaitForSeconds(0.2f); // Eventueel een korte pauze na de dash

        // Beweeg terug naar startpositie
        while (Vector3.Distance(transform.position, startPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, dashSpeed * Time.deltaTime);
            yield return null;
        }

        isDashing = false;
    }
}
