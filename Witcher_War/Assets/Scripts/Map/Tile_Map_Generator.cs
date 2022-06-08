using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Map_Generator : MonoBehaviour
{
    [SerializeField] private GameObject tileMapObject;
    [SerializeField] private GameObject potion;
    [SerializeField] private int maxRows = 1;
    [SerializeField] private int maxColumns = 1;

    private Vector3[,] tileMap;
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
