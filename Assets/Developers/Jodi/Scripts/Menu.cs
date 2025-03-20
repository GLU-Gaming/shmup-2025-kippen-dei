using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Method to load the game scene
    public void LoadGame()
    {
        SceneManager.LoadScene("Game"); 
    }

    // Method to quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}