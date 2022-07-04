using UnityEngine;
using WizardWar.GameplayObjects;
using WizardWar.Scenes;

namespace WizardWar 
{
    namespace Menu 
    {
        public class WinningCanvasController : MonoBehaviour
        {
            [SerializeField] private Canvas _winningCanvas;

            [SerializeField] private Gameplay _gameplay;

            private void Awake()
            {
                ShowCanvas();
            }

            private void ShowCanvas() 
            {
                _winningCanvas.gameObject.SetActive(false);
            }

            private void HideCanvas() 
            {
                _winningCanvas.gameObject.SetActive(false);
            }

            public void ResetGame() 
            {
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