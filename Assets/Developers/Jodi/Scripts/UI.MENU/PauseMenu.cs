using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }
    
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        
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
    public void QuitGame()
    {
        Application.Quit();
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }
}