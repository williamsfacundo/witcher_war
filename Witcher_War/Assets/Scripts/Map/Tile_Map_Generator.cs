using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Map_Generator : MonoBehaviour
{
    [SerializeField] private GameObject tileMapObject;
    [SerializeField] private int maxRows = 1;
    [SerializeField] private int maxColumns = 1;

    private Vector2[,] tileMap;
    private Vector3 mapSize;

    private Vector2 tileSize;
    
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = tileMapObject.GetComponent<Renderer>();        
    }

    void Start()
    {
        SetMap();                      
    }

    private void SetMap() 
    {
        mapSize = _renderer.bounds.size;

        tileSize = new Vector2(mapSize.x /= maxColumns, mapSize.z /= maxRows);

        tileMap = new Vector2[maxRows, maxColumns];

        for (short i = 0; i < maxRows; i++) 
        {
            for (short v = 0; v < maxColumns; v++) 
            {
                tileMap[v, i] = GetFirstMapPosition();
            }
        }
    }

    private Vector2 GetFirstMapPosition() 
    {
        return new Vector2();
    }
}
