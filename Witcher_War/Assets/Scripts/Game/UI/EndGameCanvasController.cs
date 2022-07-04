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
            }

            private void HideCanvas()
            {
                _endGameCanvas.gameObject.SetActive(false);
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