using UnityEngine;
using WizardWar.Enums;

namespace WizardWar 
{
    namespace Witcher 
    {
        namespace Interfaces 
        {
            public interface IMovable //Replace functions with input and update
            {
                void MoveInput();

                void Move(GameObject witcher, ref WitcherLookingDirection direction);

                void Timer();         
            }

            public interface IDestroyable
            {
                void ObjectAboutToBeDestroyed();
            }

            public interface IPotionInstanceable
            {
                void InstanciatePotion(GameObject potionPrefab, WitcherLookingDirection direction);
            }
        }      
    }
}