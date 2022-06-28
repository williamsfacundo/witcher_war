using UnityEngine;
using WizardWar.Witcher.Interfaces;
using WizardWar.Enums;

namespace WizardWar 
{
    namespace Witcher 
    {
        namespace Movement 
        {
            public class Movement : IMovable
            {
                public void MoveInput() 
                {

                }

                public void Move(GameObject gameObject, ref WitcherLookingDirection direction) 
                {

                }

                public void Timer() 
                {

                }

                public bool IsObjectMoving() 
                {
                    return true;
                }
            }
        }
    }
}