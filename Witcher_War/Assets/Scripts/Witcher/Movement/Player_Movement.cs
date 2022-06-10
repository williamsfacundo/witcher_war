using UnityEngine;

public class Player_Movement : ICanMove
{    
    private Vector2 movement = Vector2.zero;    

    public void Move(ref Tile objectTile, Rigidbody rb) 
    {
        MovementAxisInput(ref movement.x, KeyCode.D, KeyCode.A);
        MovementAxisInput(ref movement.y, KeyCode.S, KeyCode.W);     

        if (movement.x != 0 || movement.y != 0) 
        {
            Tile_Map_Generator.SetObjectTile(objectTile.Index + movement, ref objectTile);            

            rb.MovePosition(objectTile.Position);
        }        
    }    

    void MovementAxisInput(ref float axis, KeyCode positiveAxisMovement, KeyCode negativeAxisMovement) 
    {
        if (Input.GetKeyDown(positiveAxisMovement))
        {            
            axis = 1f;            
        }
        else if (Input.GetKeyDown(negativeAxisMovement))
        {            
            axis = -1f;
        }
        else
        {
            axis = 0f;
        }              
    }    
}
