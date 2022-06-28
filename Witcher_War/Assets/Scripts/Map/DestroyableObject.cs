using UnityEngine;
using WizardWar.Witcher.Interfaces;

namespace WizardWar 
{
    namespace TileObjects 
    {
        public class DestroyableObject : MonoBehaviour, IDestroyable
        {
            public delegate void StaticObjectAboutToBeDestroyed();

            public static StaticObjectAboutToBeDestroyed objectAboutToBeDestroyed;

            public void ObjectAboutToBeDestroyed()
            {

            }
        }
    }
}