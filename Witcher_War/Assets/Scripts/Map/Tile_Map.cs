using UnityEngine;

public static class Tile_Map
{
    public const int maxRows = 7;

    public const int maxColumns = 7;    

    private static Tile[,] tileMap;

    private static Vector2 gateTile;

    private static Vector2 tileSize;

    private static bool mapGenerated = false;

    private static short destroyableStaticObjectsCount;

    public static Vector2 TileSize
    {
        get
        {
            return tileSize;
        }
    }

    public static short DestroyableStaticObjectsCount 
    {
        get 
        {
            return destroyableStaticObjectsCount;
        }
    }

    public static Vector2 GateTile 
    {
        get 
        {
            return gateTile;
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

        gateTile = nullIndex;

        destroyableStaticObjectsCount = 0;

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

    public static void DestroyDestroyableGameObjectInTileX(Vector2 targetIndex)
    {
        if (mapGenerated && IsValidIndex(targetIndex))
        {
            if (!IsTileEmpty(targetIndex))
            {
                GameObject destroyedGameObject = tileMap[(int)targetIndex.y, (int)targetIndex.x].TileObject;

                IDestroyable aux = destroyedGameObject.GetComponent<IDestroyable>();

                if (aux != null)
                {
                    if(destroyedGameObject.tag != "Witcher") 
                    {
                        destroyableStaticObjectsCount--;                        
                    }

                    aux.ObjectAboutToBeDestroyed();

                    GameObject.Destroy(destroyedGameObject);
                }
            }
        }
    }

    public static void DestroyAdjacentObjectsOfATile(Vector2 targetIndex)
    {
        if (mapGenerated && IsValidIndex(targetIndex))
        {
            DestroyDestroyableGameObjectInTileX(targetIndex);
            DestroyDestroyableGameObjectInTileX(targetIndex - new Vector2(0f, 1f));
            DestroyDestroyableGameObjectInTileX(targetIndex + new Vector2(0f, 1f));
            DestroyDestroyableGameObjectInTileX(targetIndex - new Vector2(1f, 0f));
            DestroyDestroyableGameObjectInTileX(targetIndex - new Vector2(-1f, 0f));
        }
    }

    public static Vector2 GetGameObjectIndex(GameObject gameObject)
    {
        for (short i = 0; i < maxRows; i++)
        {
            for (short v = 0; v < maxColumns; v++)
            {
                if (tileMap[i, v].TileObject == gameObject)
                {
                    return new Vector2((float)v, (float)i);
                }
            }
        }

        return nullIndex;
    }

    public static Vector2 GetGameObjectIndexPlusOtherIndex(GameObject gameObject, Vector2 otherIndex)
    {
        Vector2 index = GetGameObjectIndex(gameObject);

        if (index != nullIndex)
        {
            index += otherIndex;

            if (!IsValidIndex(index))
            {
                index = nullIndex;
            }
        }

        return index;
    }

    public static Vector3 GetTileMapPosition(Vector2 targetIndex)
    {
        return tileMap[(int)targetIndex.y, (int)targetIndex.x].Position;
    }

    public static bool IsTileEmpty(Vector2 targetIndex)
    {
        if (IsValidIndex(targetIndex))
        {
            return tileMap[(int)targetIndex.y, (int)targetIndex.x].isEmpty;
        }

        return false;
    }

    public static void CalculateDestroyableStaticObjectsCount()
    {
        destroyableStaticObjectsCount = 0;

        for (short i = 0; i < maxRows; i++)
        {
            for (short v = 0; v < maxColumns; v++)
            {
                if (!tileMap[i, v].isEmpty)
                {
                    if (IsGameObjectDestroyableAndNotAWitcher(tileMap[i, v].TileObject))
                    {
                        destroyableStaticObjectsCount++;
                    }
                }
            }
        }
    }

    public static Vector2 GetRandomEmptyIndex() 
    {
        Vector2 index = Vector2.zero;
        short aux = 0;
        short maxIterations = 500;
        short iterations = 0;

        do
        {
            iterations++;

            aux = (short)Random.Range(0f, maxColumns - 1);

            index.x = (float)aux;

            aux = (short)Random.Range(0f, maxRows - 1);

            index.y = (float)aux;

        } while (!IsTileEmpty(index) && iterations < maxIterations);

        if (iterations >= maxIterations) 
        {
            for (short i = 0; i < maxRows; i++) 
            {
                for (short v = 0; v < maxColumns; v++) 
                {
                    index.x = (float)v;
                    index.y = (float)i;

                    if (IsTileEmpty(index)) 
                    {
                        return index;
                    }
                }
            }
        }

        return index;
    }

    public static void SetGateTile(GameObject gate, Vector2 index) 
    {
        if (gate.tag == "Gate") 
        {
            gateTile = index;
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

    private static bool IsGameObjectInTileMap(GameObject gameObject) 
    {
        for (short i = 0; i < maxRows; i++) 
        {
            for (short v = 0; v < maxColumns; v++) 
            {
                if (tileMap[i, v].TileObject == gameObject) 
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static void LocateGameObjectInTile(Vector2 newIndex, GameObject gameObject) 
    {
        if (IsValidIndex(newIndex))
        {
            if (tileMap[(int)newIndex.y, (int)newIndex.x].isEmpty && gameObject != null)
            {    
                tileMap[(int)newIndex.y, (int)newIndex.x].TileObject = gameObject;                
            }
        }
    }

    private static void LocateGameObjectInTile(Vector2 newIndex, Vector2 oldIndex,GameObject gameObject)
    {
        if (IsValidIndex(newIndex))
        {
            if (tileMap[(int)newIndex.y, (int)newIndex.x].isEmpty)
            {
                tileMap[(int)oldIndex.y, (int)oldIndex.x].TileObject = null;
                tileMap[(int)newIndex.y, (int)newIndex.x].TileObject = gameObject;
            }
        }
    }

    private static bool IsValidIndex(Vector2 tileIndex) 
    {
        return (tileIndex.x >= 0 && tileIndex.x <= maxColumns - 1) &&
            (tileIndex.y >= 0 && tileIndex.y <= maxRows - 1);
    }    

    private static bool IsGameObjectDestroyableAndNotAWitcher(GameObject gameObject)
    {
        IDestroyable auxDestroyable = gameObject.GetComponent<IDestroyable>();
        Witcher_Controller auxWitcherController = gameObject.GetComponent<Witcher_Controller>();

        return auxDestroyable != null && auxWitcherController == null;
    }    
}