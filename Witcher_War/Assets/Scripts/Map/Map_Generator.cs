using UnityEngine;
using System.IO;

public class Map_Generator : MonoBehaviour
{
    [SerializeField] private GameObject wall;    
    [SerializeField] private GameObject witcher;
    [SerializeField] private GameObject potion;

    const short maxLevel = 3;

    [Range(1, maxLevel)] private int level = 1;

    private const char nonDestroyableWallChar = 'X';
    private const char destroyableWallChar = 'W';
    private const char playerChar = 'P';

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

        sr.Close();
        fs.Close();

        return map;
    }

    private void InstanciateObjects(char[] map) 
    {
        GameObject gameObject;        
       
        for (short i = 0; i < map.Length; i++) 
        {
            switch (map[i])
            {
                case nonDestroyableWallChar:

                    gameObject = Instantiate(wall);
                    Destroy(gameObject.GetComponent<Destroyable_Wall>());
                    gameObject.GetComponent<Wall>().InitialPosIndex = GetCorrectTileMapIndex(i);
                    break;
                case destroyableWallChar:

                    gameObject = Instantiate(wall);
                    gameObject.GetComponent<Wall>().InitialPosIndex = GetCorrectTileMapIndex(i);
                    break;
                case playerChar:

                    gameObject = Instantiate(witcher);
                    Witcher_Controller witcher_Controller = gameObject.GetComponent<Witcher_Controller>();
                    witcher_Controller.WitcherType = WITCHER_TYPE.PLAYER;
                    witcher_Controller.PotionPrefab = potion;
                    witcher_Controller.InitialPosIndex = GetCorrectTileMapIndex(i);
                    break;
                default:
                    break;
            }
        }
        
        //i -> que indice representa en la matriz del tile map
    }

    Vector2 GetCorrectTileMapIndex(int arrayIndex) 
    {
        Vector2 index = Vector2.zero;

        for (short i = 0; i <= arrayIndex; i++) 
        {
            index.x++;

            if (index.x >= Tile_Map.maxColumns) 
            {
                index.x = 0;
                index.y++;
            }

            if (i != 0 && i % Tile_Map.maxColumns == 0) 
            {
                i += 2;
            }
        }

        return index;
    }
}   
