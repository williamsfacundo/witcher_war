using UnityEngine;
using System;

namespace WizardWar 
{
    namespace EndGame 
    {
        public class EndGame : MonoBehaviour
        {
            private const float _timeToEndGame = 1f;

            private float _timer;

            public static Action GameplayEnded;

            void Start()
            {
                _timer = 0;
            }
            
            void Update()
            {
                UpdateTimer();

                GameplayEnds();
            }

            private void UpdateTimer()
            {
                _timer += Time.deltaTime;
            }

            private void GameplayEnds() 
            {
                if (_timer >= _timeToEndGame) 
                {
                    GameplayEnded();

                    Destroy(gameObject);
                }
            }
        }
    }
}