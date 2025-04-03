using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Method to load the game scene
    public void LoadIntor()
    {
        SceneManager.LoadScene("Intro"); 
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("Game"); 
    }

    public void LoadBestiary()
    {
        SceneManager.LoadScene("Bestiary");
    }
    
    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    // Method to quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}