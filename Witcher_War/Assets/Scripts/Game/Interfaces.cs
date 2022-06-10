using UnityEngine;

public interface ICanMove 
{   
    void Move(ref Tile objectTile, Rigidbody rb);    
}

public interface IDestroyable
{
    void objectHit();
}

public interface ICanUsePotion 
{
    void InstanciatePotion(GameObject potionPrefab, Vector3 position);   
}