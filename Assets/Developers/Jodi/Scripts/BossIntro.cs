using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class BossIntroTMP : MonoBehaviour
{
    public float letterDelay = 0.05f;
    public float scrollSpeed = 30f;
    [TextArea] 
    public string fullName = "Boss Sir Bartholomuel V..."; // Paste your full name here

    private TMP_Text nameText;
    private ScrollRect scrollRect;
    private bool isScrolling;

    void Start()
    {
        nameText = GetComponentInChildren<TMP_Text>();
        scrollRect = GetComponent<ScrollRect>();
        nameText.text = ""; // Clear initial text
        StartCoroutine(TypeName());
    }

    IEnumerator TypeName()
    {
        foreach (char c in fullName)
        {
            nameText.text += c;
            
            // Add optional typewriter sound here
            // AudioSource.PlayClipAtPoint(typeSound, Camera.main.transform.position);
            
            yield return new WaitForSeconds(letterDelay);
        }
        
        // Start automatic scrolling
        isScrolling = true;
    }

    void Update()
    {
        if (isScrolling)
        {
            // Scroll horizontally
            scrollRect.horizontalNormalizedPosition += 
                scrollSpeed * Time.deltaTime / fullName.Length;
            
            // Optional: Add bounce when reaching end
            if(scrollRect.horizontalNormalizedPosition >= 1)
            {
                scrollRect.horizontalNormalizedPosition = 0;
            }
        }
    }
}