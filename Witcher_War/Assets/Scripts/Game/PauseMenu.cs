using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenuCanvas;

    private Map_Generator mapGenerator;

    private KeyCode pauseMenuKey = KeyCode.P;

    private bool gamePaused;

    private void Awake()
    {
        mapGenerator = GameObject.FindWithTag("Manager").GetComponent<Map_Generator>();

        Resume();
    }

    private void Start()
    {
        pauseMenuCanvas.gameObject.SetActive(gamePaused);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseMenuKey))
        {
            if (!gamePaused)
            {
                Pause();
            }            
        }
    }

    private void Resume()
    {
        gamePaused = false;

        Time.timeScale = 1f;

        pauseMenuCanvas.gameObject.SetActive(false);
    }

    private void Pause()
    {
        gamePaused = true;

        Time.timeScale = 0f;

        pauseMenuCanvas.gameObject.SetActive(true);
    }

    public void RestartButtonAction()
    {
        mapGenerator.RestartLevel();

        mapGenerator.SetLevelToOne();

        Resume();
    }

    public void ChangeToMainMenu() 
    {
        Scenes_Management.ChangeToMainMenuScene();

        if (Time.timeScale != 1f) 
        {
            Time.timeScale = 1f;
        }
    }
}
