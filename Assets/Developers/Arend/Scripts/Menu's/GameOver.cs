using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void MainMenu()
    {
        
        if (Player.instance != null)
        {
            Destroy(Player.instance.gameObject);
            Player.instance = null;
        }
        
        if (ScoreManager.instance != null)
        {
            Destroy(ScoreManager.instance.gameObject);
            ScoreManager.instance = null;
        }
        
        PlayerHealthUI[] healthUIs = FindObjectsOfType<PlayerHealthUI>();
        foreach (PlayerHealthUI healthUI in healthUIs)
        {
            Destroy(healthUI.gameObject);
        }

        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        
        if (Player.instance != null)
        {
            Destroy(Player.instance.gameObject);
            Player.instance = null;
        }
        
        if (ScoreManager.instance != null)
        {
            Destroy(ScoreManager.instance.gameObject);
            ScoreManager.instance = null;
        }
        
        PlayerHealthUI[] healthUIs = FindObjectsOfType<PlayerHealthUI>();
        foreach (PlayerHealthUI healthUI in healthUIs)
        {
            Destroy(healthUI.gameObject);
        }

        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
