using UnityEngine;
using WizardWar.Tile;
using WizardWar.Enums;

namespace WizardWar 
{
    namespace Witcher 
    {
        namespace Interfaces 
        {
            public interface IMovable
            {
                void MoveInput();

                void Move(GameObject gameObject, ref WitcherLookingDirection direction);

                void Timer();

                bool IsObjectMoving();
            }

            public interface IDestroyable
            {
                void ObjectAboutToBeDestroyed();
            }

            public interface ICanUsePotion
            {
                void InstanciatePotion(GameObject potionPrefab, Index2 instantiatorIndex, WitcherLookingDirection direction);
            }
        }      
    }
}