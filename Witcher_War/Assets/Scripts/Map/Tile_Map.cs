using UnityEngine;

public static class Tile_Map
{
    public const int maxRows = 30;

    public const int maxColumns = 30;

    private static Tile[,] tileMap;

    private static Vector2 tileSize;    

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
    
    private static Vector2 nullIndex 
    {
        get 
        {
            return new Vector2(-1, -1);
        }
    }

    public static void GenerateTileMap(Vector3 surfaceSize, Vector3 surfaceCenter)
    {        
        if (mapGenerated != true) 
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
            Vector2 auxIndex = GetGameObjectIndex(gameObject);

            if (IsGameObjectInTileMap(gameObject) && auxIndex != destinyIndex)
            {
                LocateGameObjectInTile(destinyIndex, auxIndex, gameObject);
            }
        }        
    }

    public static void DestroyGameObjectInTileX(Vector2 targetIndex) 
    {
        if (ValidIndex(targetIndex)) 
        {
            if (!tileMap[(int)targetIndex.y, (int)targetIndex.x].isEmpty) 
            {
                GameObject.Destroy(tileMap[(int)targetIndex.y, (int)targetIndex.x].TileObject);
                tileMap[(int)targetIndex.y, (int)targetIndex.x].TileObject = null;
            }
        }
    }

    public static Vector2 GetGameObjectUpIndex(GameObject gameObject) 
    {
        return GetGameObjectIndexPlusOtherIndex(gameObject, -Vector2.up);
    }

    public static Vector2 GetGameObjectDownIndex(GameObject gameObject)
    {
        return GetGameObjectIndexPlusOtherIndex(gameObject, Vector2.up);
    }

    public static Vector2 GetGameObjectRightIndex(GameObject gameObject)
    {
        return GetGameObjectIndexPlusOtherIndex(gameObject, Vector2.right);
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

        return nullIndex;
    }

    private static Vector2 GetGameObjectIndexPlusOtherIndex(GameObject gameObject, Vector2 otherIndex)
    {
        Vector2 index = GetGameObjectIndex(gameObject);

        if (index != nullIndex)
        {
            index += otherIndex;

            if (!ValidIndex(index))
            {
                index = nullIndex;
            }
        }

        return index;
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

    private static void LocateGameObjectInTile(Vector2 newIndex, GameObject gameObject) 
    {
        if (ValidIndex(newIndex))
        {
            if (tileMap[(int)newIndex.y, (int)newIndex.x].isEmpty && gameObject != null)
            {    
                tileMap[(int)newIndex.y, (int)newIndex.x].TileObject = gameObject;               
            }
        }
    }

    private static void LocateGameObjectInTile(Vector2 newIndex, Vector2 oldIndex,GameObject gameObject)
    {
        if (ValidIndex(newIndex))
        {
            if (tileMap[(int)newIndex.y, (int)newIndex.x].isEmpty)
            {
                tileMap[(int)oldIndex.y, (int)oldIndex.x].TileObject = null;
                tileMap[(int)newIndex.y, (int)newIndex.x].TileObject = gameObject;
            }
        }
    }

    private static bool ValidIndex(Vector2 tileIndex) 
    {
        return (tileIndex.x >= 0 && tileIndex.x <= maxColumns - 1) &&
            (tileIndex.y >= 0 && tileIndex.y <= maxRows - 1);
    }   
}
