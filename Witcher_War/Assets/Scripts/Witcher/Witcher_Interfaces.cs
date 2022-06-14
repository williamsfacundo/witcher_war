using UnityEngine;

public interface ICanMove 
{   
    void Move(ref Tile objectTile, Rigidbody rb, ref WITCHER_DIRECTION direction);

    void Timer();
}

public interface IDestroyable
{
    void ObjectHit();
}

public interface ICanUsePotion 
{
    void InstanciatePotion(GameObject potionPrefab, Vector3 position);   
}