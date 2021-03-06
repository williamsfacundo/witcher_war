using UnityEngine;
using UnityEngine.SceneManagement;

namespace WizardWar
{
    namespace Scenes 
    {
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
            
            public void ExitGame()
            {
                Application.Quit();
            }
        }
    }
}