using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Is_In_Gate : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) 
        {
            if (player.transform.position == transform.position && Tile_Map.DestroyableStaticObjectsCount <= 0) 
            {
                //Next Level
            }
        }        
    }
}
