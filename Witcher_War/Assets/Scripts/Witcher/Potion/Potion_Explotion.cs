using UnityEngine;

[RequireComponent(typeof(BoxCollider[]))]
public class Potion_Explotion : MonoBehaviour
{
    private const float explotionTime = 1.5f;
    private float explotionTimer;   

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

    }

    void DecreaseTimer() 
    {
        explotionTimer -= Time.deltaTime;
    }
}
