/*using System.IO;
using UnityEngine;

public class MapGenerator : MonoBehaviour
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

    public void SetLevelToOne() 
    {
        level = 1;
    }

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
        TileMap.ClearTileMap();
        SetUpMap();
        GetComponent<GateInstanciator>().DestroyGate();
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
            TileMap.CalculateDestroyableStaticObjectsCount();

            calculatedStaticObjects = true;
        }        
    }

    private void GenerateMap() 
    {
        floorPrefab = (GameObject)Instantiate(Resources.Load(floorResourceName));        

        renderer = floorPrefab?.GetComponent<Renderer>();

        TileMap.GenerateTileMap(renderer.bounds.size, renderer.bounds.center);

        Destroy(floorPrefab);

        renderer = null;
    }

    private void SetUpMap() 
    {
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
        char[] mapWithOutLineBreaks = new char[map.Length - ((TileMap.maxRows - 1) * 2) ];

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

        witcher_Controller.WitcherType = WitcherType.Player;       

        witcher_Controller.PotionPrefab = (GameObject)Resources.Load(potionResourceName);

        TileMap.RescaleGameObjectDependingTileSize(witcher_Controller.PotionPrefab, potionSizePercentage, TileMap.TileSize.x);

        witcher_Controller.InitialPosIndex = GetTileMapIndex(index);

        player.transform.tag = "Player";
    }

    private void NewCauldron(short index) 
    {
        GameObject cauldron = (GameObject)Instantiate(Resources.Load(cauldronResourceName));

        GameObjectRandomRotation(cauldron);

        TileMap.RescaleGameObjectDependingTileSize(cauldron, cauldronSizePercentage, TileMap.TileSize.x);

        cauldron.GetComponent<StaticGameObject>().InitialPosIndex = GetTileMapIndex(index);        
    }

    private void NewBookshelf(short index) 
    {
        GameObject boockshelf = (GameObject)Instantiate(Resources.Load(bookshelfResourceName));

        GameObjectRandomRotation(boockshelf);

        TileMap.RescaleGameObjectDependingTileSize(boockshelf, bookshelfSizePercentage, TileMap.TileSize.x);

        boockshelf.GetComponent<StaticGameObject>().InitialPosIndex = GetTileMapIndex(index);        
    }

    private void NewEnemy(short index) 
    {
        GameObject enemy = (GameObject)Instantiate(Resources.Load(witcherResourceName));

        Witcher_Controller witcher_Controller = enemy.GetComponent<Witcher_Controller>();

        witcher_Controller.WitcherType = WitcherType.Cpu;

        witcher_Controller.PotionPrefab = (GameObject)Resources.Load(potionResourceName);

        TileMap.RescaleGameObjectDependingTileSize(witcher_Controller.PotionPrefab, potionSizePercentage, TileMap.TileSize.x);

        witcher_Controller.InitialPosIndex = GetTileMapIndex(index);        
    }

    private Vector2 GetTileMapIndex(int arrayIndex) 
    {        
        Vector2 index = new Vector2(0f, 0f);                        

        for (short i = 0; i < arrayIndex; i++) 
        {
            index.x++;

            if (index.x == TileMap.maxRows - 1 && i + 1 < arrayIndex) 
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
}   */
