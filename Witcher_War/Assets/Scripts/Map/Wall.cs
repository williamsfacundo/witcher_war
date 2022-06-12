using UnityEngine;

public class Wall : MonoBehaviour
{
    private Tile tile;

    public Tile Tile 
    {
        get 
        {
            return tile;
        }
    }
    
    private Vector2 initialPosIndex;

    public Vector2 InitialPosIndex
    {
        set
        {
            initialPosIndex = value;
        }
    }

    private void Start()
    {
        Tile_Map.SetObjectTile(initialPosIndex, ref tile);

        transform.position = Tile_Map.TileMap[(int)initialPosIndex.y, (int)initialPosIndex.x].Position;
    }    
}
