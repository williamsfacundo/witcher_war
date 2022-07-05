using UnityEngine;

namespace WizardWar 
{
    namespace Menu 
    {
        namespace Tutorial 
        {
            public class TutorialManager : MonoBehaviour
            {
                [SerializeField] private Canvas _tutorialCanvas;

                public static bool _showTutorial = true;

                public static bool _tutorialActivated;

                public static bool TutorialActivated 
                {                    
                    get 
                    {
                        return _tutorialActivated;
                    }
                }

                private void Awake()
                {
                    if (_showTutorial) 
                    {
                        ActiveTutorial();
                    }                                                                                
                }

                public void ActiveTutorial() 
                {
                    if (_tutorialCanvas != null) 
                    {
                        _tutorialCanvas.gameObject.SetActive(true);

                        Time.timeScale = 0f;

                        _tutorialActivated = true;
                    }                    
                }     
                
                public void CloseTutorial() 
                {
                    if (_tutorialCanvas != null) 
                    {
                        _tutorialCanvas.gameObject?.SetActive(false);

                        Time.timeScale = 1f;

                        _tutorialActivated = false;
                    }                    
                }                

                public static void SetShowTutorial(bool showTutorial) 
                {
                    _showTutorial = showTutorial;
                }
            }
        }
    }
}