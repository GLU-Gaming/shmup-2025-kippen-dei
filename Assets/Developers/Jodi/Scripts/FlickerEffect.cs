using UnityEngine;
using System.Collections;

public class FlickerEffect : MonoBehaviour
{
    [SerializeField] private float flickerSpeed = 10f;
    [SerializeField] private float minAlpha = 0.3f;
    [SerializeField] private float maxAlpha = 1f;

    private Material material;
    private Coroutine flickerCoroutine;
    private Color originalColor;

    void Awake()
    {
        material = GetComponent<Renderer>().material;
        originalColor = material.color;
    }

    public void StartFlicker(float duration)
    {
        if (flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine);
        }
        flickerCoroutine = StartCoroutine(FlickerRoutine(duration));
    }

    IEnumerator FlickerRoutine(float duration)
    {
        float endTime = Time.time + duration;
        
        while (Time.time < endTime)
        {
            float alpha = Mathf.Lerp(minAlpha, maxAlpha, 
                Mathf.Abs(Mathf.Sin(Time.time * flickerSpeed)));
            
            Color newColor = originalColor;
            newColor.a = alpha;
            material.color = newColor;

            yield return null;
        }

        // Restore original alpha
        Color finalColor = originalColor;
        finalColor.a = maxAlpha;
        material.color = finalColor;
    }
}