using UnityEngine;

public interface ICanMove 
{
    void Move(Rigidbody rb, float speed);
}

public interface IDestroyable
{
    void ObjectDestroyed();
}

public interface ICanUsePotion 
{
    void InstanciatePotion(GameObject potionPrefab, Vector3 position);   
}