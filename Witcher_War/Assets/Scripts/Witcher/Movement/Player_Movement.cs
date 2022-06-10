using UnityEngine;

public class Player_Movement : ICanMove
{
    private Vector2 movement = Vector2.zero;
    
    public void Move(ref Tile objectTile, Rigidbody rb) 
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0 || movement.y != 0) 
        {
            Tile_Map_Generator.SetObjectTile(objectTile.Index + movement, ref objectTile);

            rb.MovePosition(objectTile.Position);
        }               
    }   
}
