using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class BossIntro : MonoBehaviour
{
    [Header("Boss Settings")]
    [SerializeField] string bossName = "LORD OF CINDER";
    [SerializeField] float charactersPerSecond = 15f;
    [Range(0.1f, 2f)] public float shakeIntensity = 0.5f;
    [SerializeField] float textShowDuration = 2f;
    [SerializeField] float fadeDuration = 1f;

    [Header("References")]
    [SerializeField] TMP_Text bossNameText;
    [SerializeField] Image blackPanel;
    [SerializeField] ParticleSystem textParticles;

    [Header("Effects")]
    [SerializeField] Color textColor1 = Color.red;
    [SerializeField] Color textColor2 = new Color(1, 0.5f, 0);
    [SerializeField] float colorLerpSpeed = 2f;
    [SerializeField] AudioClip appearSound;
    [SerializeField] AudioClip disappearSound;

    private Vector3 originalTextPosition;
    private AudioSource audioSource;
    private float currentAlpha = 1f;

    void Start()
    {
        originalTextPosition = bossNameText.rectTransform.localPosition;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(BossIntroSequence());
    }

    IEnumerator BossIntroSequence()
    {
        Time.timeScale = 0f;
        blackPanel.color = Color.black;
        bossNameText.text = "";
        bossNameText.color = textColor1;

        if(textParticles != null) textParticles.Play();
        
        float delayPerCharacter = 1f / charactersPerSecond;
        foreach (char c in bossName)
        {
            bossNameText.text += c;
            float timer = 0f;
            while (timer < delayPerCharacter)
            {
                timer += Time.unscaledDeltaTime;
                ApplyShakeEffect();
                UpdateTextColor();
                yield return null;
            }
        }

        if(appearSound != null) audioSource.PlayOneShot(appearSound);
        
        float shakeTimer = 0f;
        while (shakeTimer < textShowDuration)
        {
            shakeTimer += Time.unscaledDeltaTime;
            ApplyShakeEffect();
            UpdateTextColor();
            yield return null;
        }

        // Combined fade out
        if(disappearSound != null) audioSource.PlayOneShot(disappearSound);
        float fadeTimer = 0f;
        while (fadeTimer < fadeDuration)
        {
            fadeTimer += Time.unscaledDeltaTime;
            currentAlpha = Mathf.Lerp(1, 0, fadeTimer / fadeDuration);
            
            blackPanel.color = new Color(0, 0, 0, currentAlpha);
            bossNameText.color = new Color(
                bossNameText.color.r,
                bossNameText.color.g,
                bossNameText.color.b,
                currentAlpha
            );

            ApplyShakeEffect();
            UpdateTextColor();
            yield return null;
        }

        Cleanup();
    }

    void ApplyShakeEffect()
    {
        float offsetX = Mathf.PerlinNoise(Time.unscaledTime * 8f, 0) * 2 - 1;
        float offsetY = Mathf.PerlinNoise(0, Time.unscaledTime * 8f) * 2 - 1;
        
        bossNameText.rectTransform.localPosition = originalTextPosition + 
            new Vector3(offsetX, offsetY) * (shakeIntensity * 0.3f);

        // Gentle pulse effect
        float pulse = Mathf.Sin(Time.unscaledTime * 6f) * 0.03f + 1f;
        bossNameText.rectTransform.localScale = Vector3.one * pulse;
    }

    void UpdateTextColor()
    {
        float t = Mathf.PingPong(Time.unscaledTime * colorLerpSpeed, 1);
        Color newColor = Color.Lerp(textColor1, textColor2, t);
        bossNameText.color = new Color(newColor.r, newColor.g, newColor.b, currentAlpha);
    }

    void Cleanup()
    {
        bossNameText.text = "";
        blackPanel.color = Color.clear;
        bossNameText.rectTransform.localPosition = originalTextPosition;
        bossNameText.rectTransform.localScale = Vector3.one;
        Time.timeScale = 1f;
        if(textParticles != null) textParticles.Stop();
    }
}