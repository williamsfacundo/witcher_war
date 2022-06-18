using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes_Management : MonoBehaviour
{
    public static void ChangeToGameplayScene() 
    {
        SceneManager.LoadScene("Gameplay");
    }

    public static void ChangeToMainMenuScene() 
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public static void ChangeToCreditsScene() 
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public static void ChangeToEndGameScene() 
    {
        SceneManager.LoadScene("EndGame");
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
