using UnityEngine;

[RequireComponent(typeof(BoxCollider[]))]
public class Potion_Explotion : MonoBehaviour
{
    private const float explotionTime = 1.5f;
    private float explotionTimer;    

    private BoxCollider[] boxColliders;   

    private void Awake()
    {
        boxColliders = GetComponents<BoxCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        explotionTimer = explotionTime;

        SetColliders();        
    }

    void SetColliders() 
    {
        boxColliders[0].isTrigger = true;

        boxColliders[0].size = new Vector3(Tile_Map.TileSize.y * 2f, 1f, 1f);

        boxColliders[1].isTrigger = true;

        boxColliders[1].size = new Vector3(1f, 1f, Tile_Map.TileSize.y * 2f);        
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseTimer();
    }   

    private void OnTriggerStay(Collider other)
    {
        Explotion(other);
    }

    void Explotion(Collider other) 
    {
        if (explotionTimer <= 0f) 
        {
            IDestroyable interfaceComparisonAux = other.GetComponent<IDestroyable>();

            if (interfaceComparisonAux != null)
            {
                interfaceComparisonAux.ObjectHit();
            }

            Destroy(gameObject);
        }        
    }

    void DecreaseTimer() 
    {
        explotionTimer -= Time.deltaTime;
    }
}
