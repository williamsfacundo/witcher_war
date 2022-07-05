using UnityEngine;

namespace WizardWar 
{
    namespace Menu 
    {
        namespace OptionsMenu 
        {
            public class OptionsManager : MonoBehaviour
            {
                [SerializeField] private Canvas _optionsCanvas;
                                
                private void Awake()
                {
                    CloseOptionsCanvas();
                }

                public void ActiveOptionsCanvas() 
                {
                    _optionsCanvas.gameObject.SetActive(true);
                }

                public void CloseOptionsCanvas()
                {
                    _optionsCanvas.gameObject.SetActive(false);
                }                
            }
        }
    }
}