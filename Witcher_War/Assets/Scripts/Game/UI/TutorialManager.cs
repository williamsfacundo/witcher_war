using UnityEngine;

namespace WizardWar 
{
    namespace Menu 
    {
        namespace Tutorial 
        {
            public class TutorialManager : MonoBehaviour
            {
                [SerializeField] private Canvas tutorialCanvas;

                public static bool _showTutorial = true;

                private void Awake()
                {
                    if (_showTutorial) 
                    {
                        ActiveTutorial();
                    }                                                                                
                }

                public void ActiveTutorial() 
                {
                    tutorialCanvas.gameObject?.SetActive(true);

                    Time.timeScale = 0f;
                }     
                
                public void CloseTutorial() 
                {
                    tutorialCanvas.gameObject?.SetActive(false);

                    Time.timeScale = 1f;
                }                

                public static void SetShowTutorial(bool showTutorial) 
                {
                    _showTutorial = showTutorial;
                }
            }
        }
    }
}