using UnityEngine;

[RequireComponent(typeof(Wall))]
public class Destroyable_Wall : MonoBehaviour, IDestroyable
{
    public void ObjectHit() 
    {
        Tile_Map.DestroyGameObjectInTileX(Tile_Map.GetGameObjectDownIndex(gameObject));
    }
}
