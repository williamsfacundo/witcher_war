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
            if (Tile_Map.GetGameObjectIndex(player) == Tile_Map.GateTile && Tile_Map.DestroyableStaticObjectsCount <= 0) 
            {
                if (Vector3.Distance(transform.position, player.transform.position) < (Tile_Map.TileSize.x) / 10f) 
                {
                    Debug.Log("NEXT LEVEL");
                }                
            }
        }        
    }
}
