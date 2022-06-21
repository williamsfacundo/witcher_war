using System.IO;
using UnityEngine;

public class Map_Generator : MonoBehaviour
{
    const float potionSizePercentage = 0.35f;
    const float bookshelfSizePercentage = 0.80f;
    const float cauldronSizePercentage = 0.65f;

    private const string floorResourceName = "Floor";
    private const string bookshelfResourceName = "Static_Objects/Bookshelf";
    private const string cauldronResourceName = "Static_Objects/Cauldron";
    private const string witcherResourceName = "Witcher";
    private const string potionResourceName = "Witcher/Potion";

    private GameObject floorPrefab;       

    private const short maxLevel = 5;

    [Range(1, maxLevel)] private int level = 1;

    public int Level 
    {
        get 
        {
            return level;
        }        
    }

    public short MaxLevel 
    {
        get 
        {
            return maxLevel;
        }
    }

    new Renderer renderer;

    private bool calculatedStaticObjects;

    private const char bookshelfChar = 'W';
    private const char cauldronChar = 'X';
    private const char playerChar = 'P';
    private const char enemyChar = 'E';
    private const char lineBreakCharOne = (char)13;
    private const char lineBreakCharTwo = (char)10;

    public void NextLevel() 
    {        
        if (level + 1 <= maxLevel) 
        {
            level++;
        }
        
        RestartLevel();
    }

    public void RestartLevel()
    {
        Tile_Map.ClearTileMap();
        SetUpMap();
    }

    private void Awake()
    {
        GenerateMap();
        SetUpMap();   
    }

    private void Update()
    {
        if (!calculatedStaticObjects) 
        {
            Tile_Map.CalculateDestroyableStaticObjectsCount();

            calculatedStaticObjects = true;
        }        
    }

    private void GenerateMap() 
    {
        floorPrefab = (GameObject)Instantiate(Resources.Load(floorResourceName));        

        renderer = floorPrefab?.GetComponent<Renderer>();

        Tile_Map.GenerateTileMap(renderer.bounds.size, renderer.bounds.center);

        Destroy(floorPrefab);

        renderer = null;
    }

    private void SetUpMap() 
    {
        level = 1;

        char[] map = GetMapArrayChar();

        InstanciateObjects(map);

        calculatedStaticObjects = false;
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
                case bookshelfChar:

                    NewBookshelf(i);                 
                    break;
                case cauldronChar:

                    NewCauldron(i);
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

    private void NewPlayer(short index) 
    {
        GameObject player = (GameObject)Instantiate(Resources.Load(witcherResourceName));        

        Witcher_Controller witcher_Controller = player.GetComponent<Witcher_Controller>();

        witcher_Controller.WitcherType = WITCHER_TYPE.PLAYER;       

        witcher_Controller.PotionPrefab = (GameObject)Resources.Load(potionResourceName);

        Tile_Map.RescaleGameObjectDependingTileSize(witcher_Controller.PotionPrefab, potionSizePercentage, Tile_Map.TileSize.x);

        witcher_Controller.InitialPosIndex = GetTileMapIndex(index);

        player.transform.tag = "Player";
    }

    private void NewCauldron(short index) 
    {
        GameObject cauldron = (GameObject)Instantiate(Resources.Load(cauldronResourceName));

        GameObjectRandomRotation(cauldron);

        Tile_Map.RescaleGameObjectDependingTileSize(cauldron, cauldronSizePercentage, Tile_Map.TileSize.x);

        cauldron.GetComponent<StaticGameObject>().InitialPosIndex = GetTileMapIndex(index);        
    }

    private void NewBookshelf(short index) 
    {
        GameObject boockshelf = (GameObject)Instantiate(Resources.Load(bookshelfResourceName));

        GameObjectRandomRotation(boockshelf);

        Tile_Map.RescaleGameObjectDependingTileSize(boockshelf, bookshelfSizePercentage, Tile_Map.TileSize.x);

        boockshelf.GetComponent<StaticGameObject>().InitialPosIndex = GetTileMapIndex(index);        
    }

    private void NewEnemy(short index) 
    {
        GameObject enemy = (GameObject)Instantiate(Resources.Load(witcherResourceName));

        Witcher_Controller witcher_Controller = enemy.GetComponent<Witcher_Controller>();

        witcher_Controller.WitcherType = WITCHER_TYPE.CPU;

        witcher_Controller.PotionPrefab = (GameObject)Resources.Load(potionResourceName);

        Tile_Map.RescaleGameObjectDependingTileSize(witcher_Controller.PotionPrefab, potionSizePercentage, Tile_Map.TileSize.x);

        witcher_Controller.InitialPosIndex = GetTileMapIndex(index);        
    }

    private Vector2 GetTileMapIndex(int arrayIndex) 
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
    
    private void GameObjectRandomRotation(GameObject gameObject) 
    {
        switch ((short)Random.Range(1f, 4.9f)) 
        {
            case 1:
                gameObject.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                break;
            case 2:
                gameObject.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                break;
            case 3:
                gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                break;
            default:
                break;
        }        
    }
}   
