using UnityEngine;

public class Tile
{
    Vector3 position;
    bool isEmpty;

    Tile() 
    {
        position = Vector3.zero;
        isEmpty = true;
    }

    Vector3 Position 
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
    
    bool IsEmpty 
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
