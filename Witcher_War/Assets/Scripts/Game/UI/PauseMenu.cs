using UnityEngine;
using WizardWar.GameplayObjects;
using WizardWar.Scenes;
using WizardWar.Menu.Tutorial;

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

                private const KeyCode _pauseDefault = KeyCode.Escape;

                [SerializeField] private Gameplay _gameplay;

                private bool _gamePaused;
                
                private void Awake()
                {
                    Resume();
                }
                
                private void Update()
                {
                    if ((Input.GetKeyDown(_pauseMenuKey) || Input.GetKeyDown(_pauseDefault)) && !_gamePaused
                        && !WinningCanvasController.WinningActivated && !EndGameCanvasController.EndGameActivated && !TutorialManager.TutorialActivated)
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

                    _gameplay.GoToLevelOne();

                    Resume();

                    if (Time.timeScale != 1f)
                    {
                        Time.timeScale = 1f;
                    }
                }

                public void Resume()
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