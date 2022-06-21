using UnityEngine;

public class Player_Is_In_Gate : MonoBehaviour
{
    GameObject player;    

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        gameObject.transform.position = Tile_Map.GetGameObjectRightYPosition(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) 
        {
            if (Tile_Map.GetGameObjectIndex(player) == Tile_Map.GateTile && Tile_Map.DestroyableStaticObjectsCount <= 0) 
            {
                if (Vector3.Distance(transform.position, player.transform.position) < (Tile_Map.TileSize.x) / 2f) 
                {
                    Map_Generator mapGenerator = GameObject.FindWithTag("Manager").GetComponent<Map_Generator>();
                    
                    if (mapGenerator.Level < mapGenerator.MaxLevel - 1) 
                    {                        
                        mapGenerator?.NextLevel();
                        Gate_Instanciator.GateInstanciated = false;
                        Destroy(gameObject);
                    }
                    else 
                    {
                        Scenes_Management.ChangeToWinningScene();
                    }
                }                
            }
        }        
    }
}
