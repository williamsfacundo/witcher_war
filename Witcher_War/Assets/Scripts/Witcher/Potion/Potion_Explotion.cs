using UnityEngine;

public class Potion_Explotion : MonoBehaviour
{
    private const float explotionTime = 1.5f;
    private float explotionTimer;

    Vector2 explosionIndex;

    // Start is called before the first frame update
    void Start()
    {
        explotionTimer = explotionTime;

        explosionIndex = Tile_Map.GetGameObjectIndex(GameObject.FindGameObjectWithTag("Player"));
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
