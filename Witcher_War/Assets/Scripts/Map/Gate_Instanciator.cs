using UnityEngine;

public class Gate_Instanciator : MonoBehaviour
{
    [Range(0, 100)] private short probabilityToSpawnGate = 35;

    private const string gateResourceName = "Gate";

    private static bool gateInstanciated = false;

    public static bool GateInstanciated 
    {
        set 
        {
            gateInstanciated = value;
        }
    }

    GameObject gate;

    private void OnEnable()
    {        
        DestroyableObject.objectAboutToBeDestroyed += InstanciateGateMechanic;
    }

    private void OnDisable()
    {
        DestroyableObject.objectAboutToBeDestroyed -= InstanciateGateMechanic;
    }    

    private void InstanciateGateMechanic() 
    {
        if (!gateInstanciated) 
        {            
            if (Tile_Map.DestroyableStaticObjectsCount - 1 >= 0f)
            {
                if ((short)Random.Range(1f, 100f) <= probabilityToSpawnGate)
                {
                    InstanciateGate();
                }
            }
            else
            {
                InstanciateGate();
            }
        }        
    }    

    private void InstanciateGate() 
    {
        gateInstanciated = true;

        Vector2 gateIndex = Tile_Map.GetRandomEmptyIndex();

        gate = (GameObject)Instantiate(Resources.Load(gateResourceName));

        gate.transform.position = Tile_Map.GetTileMapPosition(gateIndex);

        Tile_Map.SetGateTile(gate, gateIndex);
    }
}
