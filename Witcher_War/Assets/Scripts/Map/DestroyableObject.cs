using UnityEngine;
using WizardWar.Witcher.Interfaces;

namespace WizardWar 
{
    namespace TileObjects 
    {
        public class DestroyableObject : MonoBehaviour, IDestroyable
        {
            public delegate void StaticObjectAboutToBeDestroyed();

            public static StaticObjectAboutToBeDestroyed BookshelfAboutToBeDestroyed;

            public void ObjectAboutToBeDestroyed()
            {
                if (gameObject.tag == "Bookshelf") 
                {
                    TileObjectsInstanciator.BookshelfsCount--;

                    BookshelfAboutToBeDestroyed();
                }
            }
        }
    }
}