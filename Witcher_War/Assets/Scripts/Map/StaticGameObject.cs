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
        TileMap.NewGameObjectInTile(initialPosIndex, gameObject);

        transform.position = TileMap.GetTileMapPosition(initialPosIndex);

        transform.position = TileMap.GetGameObjectRightYPosition(gameObject);
    }    
}
