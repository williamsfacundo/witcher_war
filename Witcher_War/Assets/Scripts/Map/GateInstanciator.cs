using UnityEngine;

public class GateInstanciator : MonoBehaviour
{
    /*[Range(0, 100)] private short probabilityToSpawnGate = 35;

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
            if (TileMap.DestroyableStaticObjectsCount - 1 >= 0f)
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

        Vector2 gateIndex = TileMap.GetRandomEmptyIndex();

        gate = (GameObject)Instantiate(Resources.Load(gateResourceName));

        gate.transform.position = TileMap.GetTileMapPosition(gateIndex);

        TileMap.SetGateTile(gate, gateIndex);
    }

    public void DestroyGate() 
    {
        if (gate != null) 
        {
            Destroy(gate);
            gateInstanciated = false;
        }
    }*/
}
