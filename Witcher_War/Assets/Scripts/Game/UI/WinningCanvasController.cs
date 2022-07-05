using UnityEngine;
using WizardWar.GameplayObjects;
using WizardWar.Scenes;
using WizardWar.Gate;

namespace WizardWar 
{
    namespace Menu 
    {       
        public class WinningCanvasController : MonoBehaviour
        {
            [SerializeField] private Canvas _winningCanvas;

            [SerializeField] private Gameplay _gameplay;

            public static bool _winningActivated;

            public static bool WinningActivated
            {
                get
                {
                    return _winningActivated;
                }
            }

            private void OnEnable()
            {
                PlayerIsInGate.playerWon += PlayerWon;
            }

            private void OnDisable()
            {
                PlayerIsInGate.playerWon -= PlayerWon;
            }

            private void Awake()
            {
                HideCanvas();
            }

            private void ShowCanvas() 
            {
                _winningCanvas.gameObject.SetActive(true);

                _winningActivated = true;
            }

            private void HideCanvas() 
            {
                _winningCanvas.gameObject.SetActive(false);

                _winningActivated = false;
            }

            public void PlayerWon() 
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