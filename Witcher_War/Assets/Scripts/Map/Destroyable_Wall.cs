using UnityEngine;

[RequireComponent(typeof(Wall))]
public class Destroyable_Wall : MonoBehaviour, IDestroyable
{
    public void objectHit() 
    {
        Wall wall = GetComponent<Wall>();

        wall.Tile.IsEmpty = true;
        
        Destroy(gameObject);
    }
}
