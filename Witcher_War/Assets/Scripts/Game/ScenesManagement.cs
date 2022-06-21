using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManagement : MonoBehaviour
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

    public static void ChangeToTutorialScene() 
    {
        SceneManager.LoadScene("Tutorial");
    }

    public static void ChangeToWinningScene() 
    {
        SceneManager.LoadScene("Winning");
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
