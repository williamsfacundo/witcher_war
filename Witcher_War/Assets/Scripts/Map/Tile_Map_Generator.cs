using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Map_Generator : MonoBehaviour
{
    [SerializeField] private GameObject tileMapObject;
    [SerializeField] private GameObject potion;
    
    public const int maxRows = 30;
    public const int maxColumns = 30;        

    private static Vector3[,] tileMap;
    
    public static Vector3[,] TileMap 
    {
        get 
        {
            return tileMap;
        }
    }

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
        tileMap = new Vector3[maxRows, maxColumns];

        SetMap();                      
    }

    private void SetMap() 
    {
        mapSize = _renderer.bounds.size;
        mapCenter = _renderer.bounds.center;
               
        tileSize = new Vector2(mapSize.x / maxColumns, mapSize.z / maxRows);
        
        for (short i = 0; i < maxRows; i++) 
        {
            for (short v = 0; v < maxColumns; v++) 
            {
                tileMap[i, v] = GetFirstMapPosition();
                tileMap[i, v].x += tileSize.x * v;
                tileMap[i, v].z -= tileSize.y * i;
                tileMap[i, v].y = 10f;               
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
}
