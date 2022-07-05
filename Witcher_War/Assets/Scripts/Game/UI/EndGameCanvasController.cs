using UnityEngine;
using WizardWar.GameplayObjects;
using WizardWar.Scenes;

namespace WizardWar 
{
    namespace Menu 
    {
        public class EndGameCanvasController : MonoBehaviour
        {
            [SerializeField] private Canvas _endGameCanvas;

            [SerializeField] private Gameplay _gameplay;

            public static bool _endGameActivated;

            public static bool EndGameActivated
            {
                get
                {
                    return _endGameActivated;
                }
            }

            private void OnEnable()
            {
                EndGame.EndGame.GameplayEnded += GameOver;
            }

            private void OnDisable()
            {
                EndGame.EndGame.GameplayEnded -= GameOver;
            }

            private void Awake()
            {
                HideCanvas();
            }            

            private void ShowCanvas()
            {
                _endGameCanvas.gameObject.SetActive(true);

                _endGameActivated = true;
            }

            private void HideCanvas()
            {
                _endGameCanvas.gameObject.SetActive(false);

                _endGameActivated = false;
            }

            public void GameOver()
            {
                Time.timeScale = 0f;

                ShowCanvas();
            }

            public void ResetGame()
            {
                Time.timeScale = 1f;

                _gameplay.GoToLevelOne();

                HideCanvas();
            }

            public void GoToMainMenu()
            {
                ScenesManagement.ChangeToMainMenuScene();
            }
        }
    }
}