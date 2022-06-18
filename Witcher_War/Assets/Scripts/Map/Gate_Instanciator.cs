using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_Instanciator : MonoBehaviour
{
    bool gateInstanciated = false;

    // Update is called once per frame
    void Update()
    {
        if (Tile_Map.DestroyableStaticObjectsCount <= 0f && !gateInstanciated) 
        {
            gateInstanciated = true;            
        }
    }
}
