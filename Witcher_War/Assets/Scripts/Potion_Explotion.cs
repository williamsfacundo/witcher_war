using UnityEngine;

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
        explotionTimer -= Time.deltaTime;
    }    

    private void OnTriggerStay(Collider other)
    {
        IDestroyable var = other.GetComponent<IDestroyable>();
        
        if (var != null && explotionTimer <= 0f) 
        {
            var.objectHit();
            Destroy(gameObject);
        }       
    }    
}
