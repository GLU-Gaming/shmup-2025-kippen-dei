using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        // Load the main menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
    public void AdditionalCredits()
    {
        // Load the additional credits scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("AdditionalCredits");
    }
    public void BackToCredits()
    {
        // Load the credits scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    }
}
