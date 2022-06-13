using UnityEngine;

public class Player_Movement : ICanMove
{   
    private Vector2 movementAxis;

    private Vector3 oldPosition;

    private const float cooldownTime = 0.3f;

    private float moveCooldown;        

    private float percentageMoved;

    public Player_Movement() 
    {
        movementAxis = Vector2.zero;
        oldPosition = Vector3.zero;
        moveCooldown = cooldownTime;        
        percentageMoved = 0f;
    }

    public void Move(ref Tile objectTile, Rigidbody rb) 
    {
        MovementAxisInput(ref movementAxis.x, KeyCode.D, KeyCode.A);
        MovementAxisInput(ref movementAxis.y, KeyCode.S, KeyCode.W);

        if (moveCooldown < cooldownTime && oldPosition != objectTile.Position)
        {
            rb.MovePosition(Vector3.Lerp(oldPosition, objectTile.Position, percentageMoved));
        }

        if (movementAxis.x != 0 || movementAxis.y != 0) 
        {
            oldPosition = objectTile.Position;
            
            moveCooldown = 0f;
            percentageMoved = 0f;

            Tile_Map.SetObjectTile(objectTile.Index + movementAxis, ref objectTile);

            movementAxis.x = 0;
            movementAxis.y = 0;            
        }        
    }

    public void Timer() 
    {
        if (moveCooldown <= cooldownTime) 
        {
            moveCooldown += Time.deltaTime;

            if (moveCooldown > cooldownTime)
            {
                moveCooldown = cooldownTime;
            }

            percentageMoved = moveCooldown / cooldownTime;
        }        
    }

    void MovementAxisInput(ref float axis, KeyCode positiveAxisMovement, KeyCode negativeAxisMovement) 
    {
        if (moveCooldown >= cooldownTime) 
        {
            if (Input.GetKeyDown(positiveAxisMovement))
            {
                axis = 1f;               
            }
            else if (Input.GetKeyDown(negativeAxisMovement))
            {
                axis = -1f;
            }            
        }        
    }    
}
