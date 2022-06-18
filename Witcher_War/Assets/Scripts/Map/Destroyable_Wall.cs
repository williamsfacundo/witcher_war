using UnityEngine;

[RequireComponent(typeof(Wall))]
public class Destroyable_Wall : MonoBehaviour, IDestroyable
{
    public delegate void StaticObjectAboutToBeDestroyed();

    public static StaticObjectAboutToBeDestroyed objectAboutToBeDestroyed;

    public void ObjectAboutToBeDestroyed()  
    {
        objectAboutToBeDestroyed();
    }
}