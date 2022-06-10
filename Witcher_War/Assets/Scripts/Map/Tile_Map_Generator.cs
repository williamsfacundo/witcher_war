using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Map_Generator : MonoBehaviour
{
    [SerializeField] private GameObject tileMapObject;
    [SerializeField] private GameObject potion;
    
    public const int maxRows = 30;
    public const int maxColumns = 30;        

    private static Tile[,] tileMap;   

    private Vector3 mapSize;
    private Vector3 mapCenter;

    private Vector2 tileSize;
    
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = tileMapObject.GetComponent<Renderer>();        
    }

    void Start()
    {
        InitialMapSetting();

        SetMapPositions();                      
    }

    private void InitialMapSetting() 
    {
        tileMap = new Tile[maxRows, maxColumns];

        for (short i = 0; i < maxRows; i++)
        {
            for (short v = 0; v < maxColumns; v++)
            {
                tileMap[i, v] = new Tile();
            }
        }
    }

    private void SetMapPositions() 
    {
        mapSize = _renderer.bounds.size;
        mapCenter = _renderer.bounds.center;
               
        tileSize = new Vector2(mapSize.x / maxColumns, mapSize.z / maxRows);
        
        for (short i = 0; i < maxRows; i++) 
        {
            for (short v = 0; v < maxColumns; v++) 
            {
                tileMap[i, v].Position = GetFirstMapPosition();
                tileMap[i, v].X += tileSize.x * v;
                tileMap[i, v].Z -= tileSize.y * i;
                tileMap[i, v].Y = 10f;               
            }
        }       
    }

    private Vector3 GetFirstMapPosition() 
    {
        Vector3 firstMapPosition = mapCenter;
        
        firstMapPosition.x -= mapSize.x / 2f;
        firstMapPosition.z += mapSize.z / 2f;

        firstMapPosition.x += tileSize.x / 2f;
        firstMapPosition.z -= tileSize.y / 2f;       

        return firstMapPosition;
    }   

    public static void SetObjectInitialTile(Vector2 initialTileIndex, Tile objectTile) 
    {
        if ((initialTileIndex.x >= 0 && initialTileIndex.x <= maxColumns - 1) &&
            (initialTileIndex.y >= 0 && initialTileIndex.y <= maxRows - 1)) 
        {
            if (objectTile == null && tileMap[(int)initialTileIndex.x, (int)initialTileIndex.y].IsEmpty)
            {
                tileMap[(int)initialTileIndex.x, (int)initialTileIndex.y].IsEmpty = false;

                objectTile = tileMap[(int)initialTileIndex.x, (int)initialTileIndex.y];
            }
        }               
    }

    public static void SetObjectNewTiles(Vector2 destinyTileIndex, Tile objectTile)
    {
        if ((destinyTileIndex.x >= 0 && destinyTileIndex.x <= maxColumns - 1) &&
            (destinyTileIndex.y >= 0 && destinyTileIndex.y <= maxRows - 1))
        {
            if (objectTile == null && tileMap[(int)destinyTileIndex.x, (int)destinyTileIndex.y].IsEmpty)
            {
                objectTile.IsEmpty = true;

                objectTile = tileMap[(int)destinyTileIndex.x, (int)destinyTileIndex.y];

                objectTile.IsEmpty = false;
            }
        }
    }
}
