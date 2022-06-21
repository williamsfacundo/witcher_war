using UnityEngine;

public class PlayerIsInGate : MonoBehaviour
{
    GameObject player;    

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        gameObject.transform.position = TileMap.GetGameObjectRightYPosition(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) 
        {
            if (TileMap.GetGameObjectIndex(player) == TileMap.GateTile && TileMap.DestroyableStaticObjectsCount <= 0) 
            {
                if (Vector3.Distance(transform.position, player.transform.position) < (TileMap.TileSize.x) / 2f) 
                {
                    MapGenerator mapGenerator = GameObject.FindWithTag("Manager").GetComponent<MapGenerator>();
                    
                    if (mapGenerator.Level + 1 < mapGenerator.MaxLevel) 
                    {                        
                        mapGenerator?.NextLevel();
                        GateInstanciator.GateInstanciated = false;
                        Destroy(gameObject);
                    }
                    else 
                    {
                        ScenesManagement.ChangeToWinningScene();
                    }
                }                
            }
        }        
    }
}
