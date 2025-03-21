using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Settings")]
    public int score = 0; // Score points
    public TextMeshProUGUI scoreText; // UI Text to display the score

    void Start()
    {
        UpdateScoreUI();
    }

    // Method to add score
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    // Method to update the score UI
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
} 