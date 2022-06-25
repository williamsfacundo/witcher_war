using UnityEngine;
using WizardWar.TileObjects;

namespace WizardWar
{
    namespace Menu 
    {
        namespace Pause
        {
            public class PauseMenu : MonoBehaviour
            {
                [SerializeField] private Canvas _pauseMenuCanvas;
                [SerializeField] private KeyCode pauseMenuKey = KeyCode.P;

                private TileObjectsInstanciator _tileObjectsInstanciator;

                private bool _gamePaused;

                private void Awake()
                {
                    _tileObjectsInstanciator = GetComponent<TileObjectsInstanciator>();

                    Resume();
                }

                private void Start()
                {
                    _pauseMenuCanvas.gameObject.SetActive(_gamePaused);
                }


                // Update is called once per frame
                private void Update()
                {
                    if (Input.GetKeyDown(pauseMenuKey))
                    {
                        if (!_gamePaused)
                        {
                            Pause();
                        }
                    }
                }

                public void RestartButtonAction()
                {
                    _tileObjectsInstanciator.ResetTileObjects();

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