using UnityEngine;
using System.IO;

public class Map_Generator : MonoBehaviour
{
    [SerializeField] private GameObject nonDestroyableWall;
    [SerializeField] private GameObject estroyableWall;
    [SerializeField] private GameObject witcher;

    const short maxLevel = 3;

    [Range(1, maxLevel)] private int level = 1;    

    void Start()
    {
        char[] map = GetMapArrayChar();

                               
    }

    char[] GetMapArrayChar() 
    {
        string path = Application.dataPath + "/Maps_Files/map_level_" + level + ".txt";

        FileStream fs = File.OpenRead(path);

        StreamReader sr = new StreamReader(fs);

        char[] map = sr.ReadToEnd().ToCharArray();        

        sr.Close();
        fs.Close();

        return map;
    }

    void InstanciateObjects() 
    {

    }
}   
