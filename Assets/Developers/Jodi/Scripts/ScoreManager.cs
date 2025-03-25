using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Settings")]
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public string bossSceneName = "BossBattle";
    public int bossScoreThreshold = 1000;

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
        CheckBossBattle();
    }

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
            SceneManager.LoadScene(bossSceneName);
        }
    }
}