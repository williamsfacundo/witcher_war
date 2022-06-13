using UnityEngine;

public class Tile_Map : MonoBehaviour
{
    [SerializeField] private GameObject floor;    

    public const int maxRows = 30;
    public const int maxColumns = 30;

    private static Tile[,] tileMap;

    public static Tile[,] TileMap 
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
        _renderer = floor.GetComponent<Renderer>();        
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
                tileMap[i, v].Position = GetFirstMapPosition() + new Vector3(tileSize.x * v, 0f,-tileSize.y * i);
                tileMap[i, v].Index = new Vector2(v, i);                
            }
        }       
    }

    private Vector3 GetFirstMapPosition() 
    {
        Vector3 firstMapPosition = mapCenter;

        firstMapPosition.y += mapSize.y * 2;

        firstMapPosition.x -= mapSize.x / 2f;
        firstMapPosition.z += mapSize.z / 2f;

        firstMapPosition.x += tileSize.x / 2f;
        firstMapPosition.z -= tileSize.y / 2f;       

        return firstMapPosition;
    }

    public static void SetObjectTile(Vector2 tileIndex, ref Tile objectTile)
    {
        if ((tileIndex.x >= 0 && tileIndex.x <= maxColumns - 1) &&
            (tileIndex.y >= 0 && tileIndex.y <= maxRows - 1))
        {
            if (tileMap[(int)tileIndex.y, (int)tileIndex.x].IsEmpty) 
            {
                if (objectTile != null)
                {
                    objectTile.IsEmpty = true;
                }

                objectTile = tileMap[(int)tileIndex.y, (int)tileIndex.x];
                objectTile.IsEmpty = false;
            }            
        }        
    }    
}
