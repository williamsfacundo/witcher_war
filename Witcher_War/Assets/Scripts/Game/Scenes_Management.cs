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
        SceneManager.LoadScene("Credits");
    }

    public static void ChangeToEndGameScene() 
    {
        SceneManager.LoadScene("EndGame");
    }

    public static void ChangeToOptionsScene() 
    {
        SceneManager.LoadScene("Options");
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
