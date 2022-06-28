using UnityEngine;
using WizardWar.GameplayObjects;
using WizardWar.Scenes;

namespace WizardWar
{
    namespace Menu 
    {
        namespace Pause
        {
            public class PauseMenu : MonoBehaviour
            {
                [SerializeField] private Canvas _pauseMenuCanvas;

                [SerializeField] private KeyCode _pauseMenuKey = KeyCode.P;

                [SerializeField] private Gameplay _gameplay;

                private bool _gamePaused;
                
                private void Awake()
                {
                    Resume();
                }
                
                private void Update()
                {
                    if (Input.GetKeyDown(_pauseMenuKey) && !_gamePaused)
                    {
                        Pause();
                    }
                }

                public void RestartButtonAction()
                {
                    _gameplay.GoToLevelOne();

                    Resume();
                }

                public void ChangeToMainMenu()
                {
                    ScenesManagement.ChangeToMainMenuScene();

                    if (Time.timeScale != 1f)
                    {
                        Time.timeScale = 1f;
                    }
                }

                private void Resume()
                {
                    _gamePaused = false;

                    Time.timeScale = 1f;

                    _pauseMenuCanvas.gameObject.SetActive(false);
                }

                private void Pause()
                {
                    _gamePaused = true;

                    Time.timeScale = 0f;

                    _pauseMenuCanvas.gameObject.SetActive(true);
                }               
            }
        }        
    }
}