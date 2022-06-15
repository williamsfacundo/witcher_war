using UnityEngine;

public class Tile
{
    Vector3 position;    
    GameObject tileObject;

    public Tile() 
    {
        position = Vector3.zero;        
        tileObject = null;
    }

    public Vector3 Position 
    {
        set 
        {
            position = value;            
        }
        get 
        {
            return position;
        }
    }   

    public GameObject TileObject
    {
        set 
        {
            tileObject = value;
        }
        get 
        {
            return tileObject;
        }
    }

    public bool isEmpty 
    {       
        get 
        {
            return tileObject == null;
        }
    }
}
