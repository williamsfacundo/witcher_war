using UnityEngine;

public class Gate_Instanciator : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private short probabilityToSpawnGate = 10;

    private const string gateResourceName = "Gate";

    bool gateInstanciated = false;

    GameObject gate;

    private void OnEnable()
    {        
        Destroyable_Wall.objectAboutToBeDestroyed += InstanciateGateMechanic;
    }

    private void OnDisable()
    {
        Destroyable_Wall.objectAboutToBeDestroyed -= InstanciateGateMechanic;
    }    

    private void InstanciateGateMechanic() 
    {
        if (!gateInstanciated) 
        {
            if (Tile_Map.DestroyableStaticObjectsCount - 1 >= 0f)
            {
                float random = Random.Range(1f, 100f);

                if ((short)random <= probabilityToSpawnGate)
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

        gate = (GameObject)Instantiate(Resources.Load(gateResourceName));

        gate.transform.position = Tile_Map.GetTileMapPosition(Tile_Map.GetRandomEmptyIndex());
    }
}
