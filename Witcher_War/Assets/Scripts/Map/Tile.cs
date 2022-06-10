using UnityEngine;

public class Tile
{
    Vector3 position;
    bool isEmpty;

    public Tile() 
    {
        position = Vector3.zero;
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
    
    public float X 
    {
        set 
        {
            position.x = value;
        }
        get 
        {
            return position.x;
        }
    }

    public float Y 
    {
        set 
        {
            position.y = value;
        }
        get 
        {
            return position.y;
        }
    }

    public float Z 
    {
        set 
        {
            position.z = value;

        }
        get 
        {
            return position.z;
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
