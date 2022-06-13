using UnityEngine;
using System.IO;

public class Map_Generator : MonoBehaviour
{    
    [SerializeField] private GameObject wall;    
    [SerializeField] private GameObject witcher;
    [SerializeField] private GameObject potion;

    [SerializeField] private Material nonDestroyableWallMat;
    [SerializeField] private Material destroyableWallMat;    

    const short maxLevel = 3;

    [Range(1, maxLevel)] private int level = 1;

    private const char nonDestroyableWallChar = 'X';
    private const char destroyableWallChar = 'W';
    private const char playerChar = 'P';
    private const char lineBreakCharOne = (char)13;
    private const char lineBreakCharTwo = (char)10;

    void Start()
    {
        char[] map = GetMapArrayChar();
        
        InstanciateObjects(map);                       
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
                default:
                    break;
            }            
        }       
    }

    void NewPlayer(short index) 
    {
        GameObject gameObject = Instantiate(witcher);
        Witcher_Controller witcher_Controller = gameObject.GetComponent<Witcher_Controller>();
        witcher_Controller.WitcherType = WITCHER_TYPE.PLAYER;
        witcher_Controller.PotionPrefab = potion;
        witcher_Controller.InitialPosIndex = GetTileMapIndex(index);        
    }

    void NewDestroyableWall(short index) 
    {
        GameObject gameObject = Instantiate(wall);
        gameObject.GetComponent<Renderer>().material = destroyableWallMat;
        gameObject.GetComponent<Wall>().InitialPosIndex = GetTileMapIndex(index);
    }

    void NewNonDestroyableWall(short index) 
    {
        GameObject gameObject = Instantiate(wall);
        Destroy(gameObject.GetComponent<Destroyable_Wall>());
        gameObject.GetComponent<Renderer>().material = nonDestroyableWallMat;
        gameObject.GetComponent<Wall>().InitialPosIndex = GetTileMapIndex(index);
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
