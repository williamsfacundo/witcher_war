using UnityEngine;
using WizardWar.Scenes;

namespace WizardWar 
{
    namespace EndGame 
    {
        public class EndGame : MonoBehaviour
        {
            const float _timeToEndGame = 1.5f;

            float _timer;
           
            void Start()
            {
                _timer = 0;
            }
            
            void Update()
            {
                UpdateTimer();

                ChangeToEndGame();
            }

            private void UpdateTimer()
            {
                _timer += Time.deltaTime;
            }

            private void ChangeToEndGame() 
            {
                if (_timer >= _timeToEndGame) 
                {
                    //ScenesManagement.ChangeToEndGameScene();
                }
            }
        }
    }
}