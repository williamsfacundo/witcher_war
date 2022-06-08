using UnityEngine;

public class Potion_Explotion : MonoBehaviour
{
    private const float explotionTime = 1.5f;
    private float explotionTimer;

    IDestroyable interfaceComparisonAux;

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
        if (explotionTimer <= 0f) 
        {
            interfaceComparisonAux = other.GetComponent<IDestroyable>();
            
            if (interfaceComparisonAux != null)
            {
                interfaceComparisonAux.objectHit();
                Destroy(gameObject);
            }
        }               
    }    
}
