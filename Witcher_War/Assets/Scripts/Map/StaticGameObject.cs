using UnityEngine;

public class StaticGameObject : MonoBehaviour
{    
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
        Tile_Map.NewGameObjectInTile(initialPosIndex, gameObject);

        transform.position = Tile_Map.GetTileMapPosition(initialPosIndex);

        transform.position = Tile_Map.GetGameObjectRightYPosition(gameObject);
    }    
}
