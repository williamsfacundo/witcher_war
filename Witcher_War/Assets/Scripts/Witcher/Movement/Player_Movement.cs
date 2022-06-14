using UnityEngine;

public class Player_Movement : IMovable
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

    public void MoveInput() 
    {
        MovementAxisInput(ref movementAxis.x, KeyCode.D, KeyCode.A);
        MovementAxisInput(ref movementAxis.y, KeyCode.S, KeyCode.W);
    }

    public void Move(ref Tile objectTile, Rigidbody rb, ref WITCHER_DIRECTION direction) 
    {
        if (moveCooldown < cooldownTime && oldPosition != objectTile.Position)
        {
            rb.MovePosition(Vector3.Lerp(oldPosition, objectTile.Position, percentageMoved));
        }

        if (movementAxis.x != 0 || movementAxis.y != 0) 
        {
            RotatePlayer(movementAxis, ref direction, rb);

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

    void RotatePlayer(Vector2 movementAxis, ref WITCHER_DIRECTION witcherDirection, Rigidbody rb) 
    {
        WITCHER_DIRECTION newDirection = WITCHER_DIRECTION.LEFT;

        switch ((int)movementAxis.x) 
        {
            case -1:
                
                newDirection = WITCHER_DIRECTION.LEFT;
                break;
            case 1:

                newDirection = WITCHER_DIRECTION.RIGHT;
                break;
            default:
                break;
        }

        switch ((int)movementAxis.y)
        {
            case -1:

                newDirection = WITCHER_DIRECTION.UP;
                break;
            case 1:

                newDirection = WITCHER_DIRECTION.DOWN;
                break;
            default:
                break;
        }

        if (newDirection != witcherDirection) 
        {
            rb.rotation = Quaternion.identity;

            witcherDirection = newDirection;

            switch (witcherDirection) 
            {
                case WITCHER_DIRECTION.UP:

                    rb.MoveRotation(Quaternion.Euler(0f, 180f, 0f));
                    break;

                case WITCHER_DIRECTION.RIGHT:

                    rb.MoveRotation(Quaternion.Euler(0f, -90f, 0f));
                    break;

                case WITCHER_DIRECTION.LEFT:

                    rb.MoveRotation(Quaternion.Euler(0f, 90f, 0f));
                    break;

                default:
                    break;
            }
        }
    }
}
