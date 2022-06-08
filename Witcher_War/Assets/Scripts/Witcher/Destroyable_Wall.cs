using UnityEngine;

public class Destroyable_Wall : MonoBehaviour, IDestroyable
{
    public void objectHit() 
    {
        Destroy(gameObject);
    }
}
