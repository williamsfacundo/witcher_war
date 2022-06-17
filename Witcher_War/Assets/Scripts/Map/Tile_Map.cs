using UnityEngine;

public static class Tile_Map
{
    public const int maxRows = 30;

    public const int maxColumns = 30;

    private const short maxBorders = 4;

    private static Tile[,] tileMap;

    private static Vector2 tileSize;

    private static bool mapGenerated = false;

    private static bool doorCreated = false;

    private static short destroyableStaticObjectsCount;

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

    public static void ReplaceGameObjectForOtherInTileX(Vector2 targetIndex, GameObject newGameObject)
    {
        if (mapGenerated && IsValidIndex(targetIndex))
        {
            if (!IsTileEmpty(targetIndex))
            {
                DestroyGameObjectInTileX(targetIndex);
            }

            NewGameObjectInTile(targetIndex, newGameObject);
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
            Vector2 upIndex = targetIndex - new Vector2(0f, 1f);
            Vector2 downIndex = targetIndex - new Vector2(0f, 1f);
            Vector2 rightIndex = targetIndex - new Vector2(1f, 0f);
            Vector2 leftIndex = targetIndex - new Vector2(-1f, 0f);

            DestroyDestroyableGameObjectInTileX(upIndex);
            DestroyDestroyableGameObjectInTileX(downIndex);
            DestroyDestroyableGameObjectInTileX(rightIndex);
            DestroyDestroyableGameObjectInTileX(leftIndex);
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

                if (IsGameObjectDestroyableAndNotAWitcher(gameObject))
                {
                    destroyableStaticObjectsCount++;
                }
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

    private static void DestroyGameObjectInTileX(Vector2 targetIndex)
    {
        if (IsValidIndex(targetIndex))
        {
            if (!IsTileEmpty(targetIndex))
            {
                if (IsGameObjectDestroyableAndNotAWitcher(tileMap[(int)targetIndex.y, (int)targetIndex.x].TileObject)) 
                {
                    destroyableStaticObjectsCount--;
                }

                GameObject.Destroy(tileMap[(int)targetIndex.y, (int)targetIndex.x].TileObject);
            }
        }
    }

    private static bool IsGameObjectDestroyableAndNotAWitcher(GameObject gameObject)
    {
        IDestroyable aux = gameObject.GetComponent<IDestroyable>();

        return aux != null && gameObject.tag != "Witcher";
    }

    public static void TryToGenerateDoor() //https://answers.unity.com/questions/551934/instantiating-using-a-string-for-prefab-name.html
    {
        if (!doorCreated)
        {
            if (destroyableStaticObjectsCount <= 1)
            {                
                //Instanciar puerta
            }
            else
            {

            }

            //Si es el ultimo objeto generar puerta
            //Si no es el ultimo objeto intentar generarla
            //El lugar donde se generara la puerta no puede ser una esquina
        }
    }

    private static Vector2 GetRandomBorderIndex() 
    {
        short side = (short)Random.Range(1f, maxBorders);        

        switch (side) 
        {
            case 1:               
                return new Vector2(Random.Range(1f, maxColumns - 2), 0f);                
            case 2:
                return new Vector2(0f, Random.Range(1f, maxRows - 2));                 
            case 3:                
                return new Vector2(Random.Range(1f, maxColumns - 2), maxRows - 1);
            case 4:                
                return new Vector2(maxColumns - 1, Random.Range(1f, maxRows - 2));
            default:                
                return new Vector2(Random.Range(1f, maxColumns - 2), 0f);
        }
    }
}