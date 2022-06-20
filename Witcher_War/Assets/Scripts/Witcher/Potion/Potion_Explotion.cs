using UnityEngine;

public class Potion_Explotion : MonoBehaviour
{
    private const float explotionTime = 1.5f;
    private float explotionTimer;

    private Vector2 explosionIndex;

    public Vector2 ExplosionIndex 
    {
        set 
        {
            explosionIndex = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        explotionTimer = explotionTime;        
    }    

    // Update is called once per frame
    void Update()
    {
        Explotion();

        DecreaseTimer();       
    }

    void Explotion() 
    {
        if (explotionTimer <= 0f) 
        {
            DestroyAdjacentObjects();

            Destroy(gameObject);
        }        
    }

    void DestroyAdjacentObjects() 
    {
        Tile_Map.DestroyAdjacentObjectsOfATile(explosionIndex);
    }

    void DecreaseTimer() 
    {
        explotionTimer -= Time.deltaTime;
    }
}
