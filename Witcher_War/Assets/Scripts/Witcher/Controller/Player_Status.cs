using UnityEngine;

public class Player_Status : MonoBehaviour
{
    GameObject player;

    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }   

    // Update is called once per frame
    void Update()
    {
                        
    }
}
