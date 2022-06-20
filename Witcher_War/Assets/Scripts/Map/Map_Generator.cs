using System.IO;
using UnityEngine;

public class Map_Generator : MonoBehaviour
{
    const float potionSizePercentage = 0.5f;
    const float bookshelfSizePercentage = 1f;

    private const string floorResourceName = "Floor";
    private const string bookshelfResourceName = "Static_Objects/Bookshelf";
    private const string cauldronResourceName = "Static_Objects/Cauldron";
    private const string witcherResourceName = "Witcher";
    private const string potionResourceName = "Witcher/Potion";

    private GameObject floorPrefab;       

    const short maxLevel = 3;

    [Range(1, maxLevel)] private int level = 1;

    new Renderer renderer;

    private bool calculatedStaticObjects;

    private const char nonDestroyableWallChar = 'X';
    private const char destroyableWallChar = 'W';
    private const char playerChar = 'P';
    private const char enemyChar = 'E';
    private const char lineBreakCharOne = (char)13;
    private const char lineBreakCharTwo = (char)10;

    private void Awake()
    {
        floorPrefab = (GameObject)Instantiate(Resources.Load(floorResourceName));        
        
        calculatedStaticObjects = false;

        renderer = floorPrefab?.GetComponent<Renderer>();        

        Tile_Map.GenerateTileMap(renderer.bounds.size, renderer.bounds.center);

        Destroy(floorPrefab);

        renderer = null;

        char[] map = GetMapArrayChar();

        InstanciateObjects(map);       
    }

    private void Update()
    {
        if (!calculatedStaticObjects) 
        {
            Tile_Map.CalculateDestroyableStaticObjectsCount();

            calculatedStaticObjects = true;
        }        
    }

    private char[] GetMapArrayChar() 
    {
        string path = Application.dataPath + "/Maps_Files/map_level_" + level + ".txt";

        FileStream fs = File.OpenRead(path);

        StreamReader sr = new StreamReader(fs);

        char[] map = sr.ReadToEnd().ToCharArray();
        char[] mapWithOutLineBreaks = new char[map.Length - ((Tile_Map.maxRows - 1) * 2) ];

        int auxIndex = 0;

        for (short i = 0; i < map.Length; i++) 
        {            
            if (map[i] != lineBreakCharOne && map[i] != lineBreakCharTwo) 
            {
                mapWithOutLineBreaks[auxIndex] = map[i];

                auxIndex++;
            }
        }

        sr.Close();
        fs.Close();        

        return mapWithOutLineBreaks;
    }

    private void InstanciateObjects(char[] map) 
    {       
        for (short i = 0; i < map.Length; i++) 
        {
            switch (map[i])
            {
                case nonDestroyableWallChar:

                    NewNonDestroyableWall(i);                 
                    break;
                case destroyableWallChar:

                    NewDestroyableWall(i);
                    break;
                case playerChar:

                    NewPlayer(i);
                    break;
                case enemyChar:

                    NewEnemy(i);
                    break;
                default:
                    break;
            }            
        }       
    }

    void NewPlayer(short index) 
    {
        GameObject gameObject = (GameObject)Instantiate(Resources.Load(witcherResourceName));
        Witcher_Controller witcher_Controller = gameObject.GetComponent<Witcher_Controller>();
        witcher_Controller.WitcherType = WITCHER_TYPE.PLAYER;       
        witcher_Controller.PotionPrefab = (GameObject)Resources.Load(potionResourceName);
        Tile_Map.RescaleGameObjectDependingTileSize(witcher_Controller.PotionPrefab, potionSizePercentage, Tile_Map.TileSize.x);
        witcher_Controller.InitialPosIndex = GetTileMapIndex(index);
        gameObject.transform.tag = "Player";
    }

    void NewDestroyableWall(short index) 
    {
        GameObject gameObject = (GameObject)Instantiate(Resources.Load(cauldronResourceName));
        Tile_Map.RescaleGameObjectDependingTileSize(gameObject, bookshelfSizePercentage, Tile_Map.TileSize.x);
        gameObject.GetComponent<Wall>().InitialPosIndex = GetTileMapIndex(index);        
    }

    void NewNonDestroyableWall(short index) 
    {
        GameObject nonDestroyableWall = (GameObject)Instantiate(Resources.Load(bookshelfResourceName));
        Tile_Map.RescaleGameObjectDependingTileSize(nonDestroyableWall, bookshelfSizePercentage, Tile_Map.TileSize.x);
        nonDestroyableWall.GetComponent<Wall>().InitialPosIndex = GetTileMapIndex(index);        
    }

    void NewEnemy(short index) 
    {
        GameObject gameObject = (GameObject)Instantiate(Resources.Load(witcherResourceName));
        Witcher_Controller witcher_Controller = gameObject.GetComponent<Witcher_Controller>();
        witcher_Controller.WitcherType = WITCHER_TYPE.CPU;
        witcher_Controller.PotionPrefab = (GameObject)Resources.Load(potionResourceName);
        Tile_Map.RescaleGameObjectDependingTileSize(witcher_Controller.PotionPrefab, potionSizePercentage, Tile_Map.TileSize.x);
        witcher_Controller.InitialPosIndex = GetTileMapIndex(index);        
    }

    Vector2 GetTileMapIndex(int arrayIndex) 
    {        
        Vector2 index = new Vector2(0f, 0f);                        

        for (short i = 0; i < arrayIndex; i++) 
        {
            index.x++;

            if (index.x == Tile_Map.maxRows - 1 && i + 1 < arrayIndex) 
            {
                index.x = -1;
                index.y++;
            }
        }       

        return index;
    }
}   
