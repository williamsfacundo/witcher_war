using UnityEngine;

public interface IMovable 
{
    void MoveInput();

    void Move(GameObject gameObject, ref WITCHER_DIRECTION direction);

    void Timer();

    bool IsObjectMoving();
}

public interface IDestroyable
{
    void ObjectHit();
}

public interface ICanUsePotion 
{
    void InstanciatePotion(GameObject potionPrefab, Vector3 position);   
}