using UnityEngine;

[RequireComponent(typeof(Wall))]
public class Destroyable_Wall : MonoBehaviour, IDestroyable
{
    public void ObjectAboutToBeDestroyed() 
    {
        //Tile_Map.TryToGenerateDoor();
    }
}
