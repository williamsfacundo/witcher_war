using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes_Management : MonoBehaviour
{
    public static void ChangeToGameplayScene() 
    {
        SceneManager.LoadScene("Gameplay");
    }

    public static void ChangeToMenuesScene() 
    {
        SceneManager.LoadScene("Menues");
    }   
}
