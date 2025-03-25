using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Settings")]
    public int score = 0; // Score points
    public TextMeshProUGUI scoreText; // UI Text to display the score
    public string bossSceneName = "BossBattle"; // Name of the boss scene
    public int bossScoreThreshold = 1000; // Score required to trigger boss battle

    void Start()
    {
        UpdateScoreUI();
    }

    // Method to add score
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
        CheckBossBattle();
    }

    // Method to update the score UI
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
    
    private void CheckBossBattle()
    {
        if (score >= bossScoreThreshold)
        {
            SceneManager.LoadScene("BossBattle");
        }
    }
}