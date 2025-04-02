using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Settings")]
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public string bossSceneName = "BossBattle";
    public int bossScoreThreshold = 750;

    public static ScoreManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
            scoreText.text = score.ToString("D4");
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