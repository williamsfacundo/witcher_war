using UnityEngine;

public class DestroyableObject : MonoBehaviour, IDestroyable
{
    public delegate void StaticObjectAboutToBeDestroyed();

    public static StaticObjectAboutToBeDestroyed objectAboutToBeDestroyed;

    public void ObjectAboutToBeDestroyed()  
    {
        
    }
}