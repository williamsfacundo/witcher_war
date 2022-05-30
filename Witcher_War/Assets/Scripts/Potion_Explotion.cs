using UnityEngine;

public class Potion_Explotion : MonoBehaviour
{
    private const float explotionTime = 2f;
    private float explotionTimer;       
    
    // Start is called before the first frame update
    void Start()
    {
        explotionTimer = explotionTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (explotionTimer > 0f) 
        {
            explotionTimer -= Time.deltaTime;
        }
        else 
        {
            Destroy(gameObject);            
        }
    }    

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.tag);

        if ((other.gameObject.tag == "Player" || other.gameObject.tag == "Destroyable Wall") 
            && explotionTimer <= 0f)
        {
            other.gameObject.GetComponent<IDestroyable>().objectHit();
        }
    }    
}
