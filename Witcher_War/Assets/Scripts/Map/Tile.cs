using UnityEngine;

public class Tile
{
    Vector3 position;
    Vector2 index;
    bool isEmpty;
    GameObject tileObject;

    public Tile() 
    {
        position = Vector3.zero;
        index = Vector2.zero;
        isEmpty = true;
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

    public Vector2 Index 
    {
        set 
        {
            index = value;
        }
        get 
        {
            return index;
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

    public bool IsEmpty 
    {
        set 
        {
            isEmpty = value;
        }
        get 
        {
            return isEmpty;
        }
    }
}
