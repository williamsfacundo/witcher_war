using UnityEngine;
using WizardWar.Menu.Tutorial;

namespace WizardWar 
{
    namespace Menu 
    {
        namespace MainMenu 
        {
            public class MainMenuManager : MonoBehaviour
            {
                [SerializeField] private TutorialManager _tutorialManager;

                [SerializeField] private Canvas _mainMenuCanvas;

                private void Awake()
                {
                    ActiveMainMenuCanvas();

                    _tutorialManager.CloseTutorial();
                }                

                public void ActiveMainMenuCanvas() 
                {
                    _mainMenuCanvas.gameObject.SetActive(true);
                }

                public void CloseMainMenuCanvas()
                {
                    _mainMenuCanvas.gameObject.SetActive(false);
                }
            }
        }
    }
}