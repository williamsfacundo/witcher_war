using UnityEngine;

public static class Tile_Map
{
    public const int maxRows = 30;

    public const int maxColumns = 30;

    private static Tile[,] tileMap;

    private static Vector2 tileSize;

    //private static Vector3 mapSize;

    //private static Vector3 mapCenter;

    private static bool mapGenerated = false;

    public static Tile[,] TileMap 
    {
        get 
        {
            return tileMap;
        }
    }    

    public static Vector2 TileSize 
    {        
        get 
        {
            return tileSize;
        }
    }       

    public static void GenerateTileMap(Vector3 surfaceSize, Vector3 surfaceCenter)
    {        
        if(mapGenerated != true) 
        {
            mapGenerated = true;
        }


        InitialMapSetting();

        SetMapPositions(surfaceSize, surfaceCenter);
    }

    public static void NewGameObjectInTile(Vector2 tileIndex, GameObject gameObject)
    {
        if (mapGenerated) 
        {
            if (!IsGameObjectInTileMap(gameObject))
            {
                LocateGameObjectInTile(tileIndex, gameObject);
            }
        }        
    }

    public static void MoveGameObjectToTileX(Vector2 destinyIndex, GameObject gameObject)
    {
        if (mapGenerated) 
        {
            if (IsGameObjectInTileMap(gameObject) && tileMap[(int)destinyIndex.y, (int)destinyIndex.x].IsEmpty)
            {
                if (GetGameObjectIndex(gameObject) != destinyIndex)
                {

                }
            }
        }        
    }

    private static void InitialMapSetting() 
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

    private static void SetMapPositions(Vector3 mapSize, Vector3 mapCenter) 
    {              
        tileSize = new Vector2(mapSize.x / maxColumns, mapSize.z / maxRows);
        
        for (short i = 0; i < maxRows; i++) 
        {
            for (short v = 0; v < maxColumns; v++) 
            {
                tileMap[i, v].Position = GetFirstMapPosition(mapSize, mapCenter) + new Vector3(tileSize.x * v, 0f,-tileSize.y * i);
                tileMap[i, v].Index = new Vector2(v, i);                
            }
        }       
    }

    private static Vector3 GetFirstMapPosition(Vector3 mapSize, Vector3 mapCenter) 
    {
        Vector3 firstMapPosition = mapCenter;

        firstMapPosition.y += mapSize.y * 2;

        firstMapPosition.x -= mapSize.x / 2f;
        firstMapPosition.z += mapSize.z / 2f;

        firstMapPosition.x += tileSize.x / 2f;
        firstMapPosition.z -= tileSize.y / 2f;       

        return firstMapPosition;
    }    

    private static Vector2 GetGameObjectIndex(GameObject gameObject)
    {
        for (short i = 0; i < maxRows; i++)
        {
            for (short v = 0; v < maxColumns; v++)
            {
                if (TileMap[i, v].TileObject == gameObject)
                {
                    return new Vector2(v, i);
                }
            }
        }

        return GetNullIndex();
    }

    private static bool IsGameObjectInTileMap(GameObject gameObject) 
    {
        for (short i = 0; i < maxRows; i++) 
        {
            for (short v = 0; v < maxColumns; v++) 
            {
                if (TileMap[i, v].TileObject == gameObject) 
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static void LocateGameObjectInTile(Vector2 tileIndex, GameObject gameObject) 
    {
        if ((tileIndex.x >= 0 && tileIndex.x <= maxColumns - 1) &&
            (tileIndex.y >= 0 && tileIndex.y <= maxRows - 1))
        {
            if (tileMap[(int)tileIndex.y, (int)tileIndex.x].IsEmpty)
            {    
                tileMap[(int)tileIndex.y, (int)tileIndex.x].TileObject = gameObject;               
            }
        }
    }

    private static Vector2 GetNullIndex() 
    {
        return new Vector2(-1, -1);
    }
}
