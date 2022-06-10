using UnityEngine;

public interface ICanMove 
{   
    void Move(ref Vector2 indexPos);
}

public interface IDestroyable
{
    void objectHit();
}

public interface ICanUsePotion 
{
    void InstanciatePotion(GameObject potionPrefab, Vector3 position);   
}