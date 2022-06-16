using UnityEngine;

public class Player_Movement : IMovable
{   
    private Vector2 movementAxis;

    private Vector3 oldPosition;

    private Vector3 newPosition;

    private const float displacementTime = 0.3f;

    private float movementTimer;        

    private float percentageMoved;

    private Vector2 nextTileIndex;

    public Player_Movement() 
    {
        movementAxis = Vector2.zero;
        oldPosition = Vector2.zero;
        movementTimer = displacementTime;        
        percentageMoved = 0f;
        nextTileIndex = Vector2.zero;        
    }

    public void MoveInput() 
    {
        movementAxis.x = Input.GetAxisRaw("Horizontal");
        
        if (movementAxis.x == 0) 
        {
            movementAxis.y = Input.GetAxisRaw("Vertical");

            movementAxis.y *= -1;
        }                
    }    

    public void Move(GameObject gameObject, ref WITCHER_DIRECTION direction)
    {
        if (movementAxis != Vector2.zero && !IsObjectMoving()) 
        {
            nextTileIndex = Tile_Map.GetGameObjectIndexPlusOtherIndex(gameObject, movementAxis);

            if (Tile_Map.IsTileEmpty(nextTileIndex)) 
            {
                RotatePlayer(movementAxis, ref direction, gameObject);

                movementTimer = 0f;

                oldPosition = Tile_Map.GetTileMapPosition(Tile_Map.GetGameObjectIndex(gameObject));
                newPosition = Tile_Map.GetTileMapPosition(nextTileIndex);

                Tile_Map.MoveGameObjectToTileX(nextTileIndex, gameObject);
            }
        }

        if (IsObjectMoving()) 
        {
            percentageMoved = movementTimer / displacementTime;

            gameObject.transform.position = Vector3.Lerp(oldPosition, newPosition, percentageMoved);
        }       
    }   

    public void Timer() 
    {
        if (movementTimer < displacementTime) 
        {
            movementTimer += Time.deltaTime;

            if (movementTimer > displacementTime)
            {
                movementTimer = displacementTime;
            }            
        }        
    }   
    
    private bool IsObjectMoving() 
    {
        return movementTimer < displacementTime;
    }

    void RotatePlayer(Vector2 movementAxis, ref WITCHER_DIRECTION witcherDirection, GameObject gameObject) 
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
            gameObject.transform.rotation = Quaternion.identity;

            witcherDirection = newDirection;

            switch (witcherDirection) 
            {
                case WITCHER_DIRECTION.UP:

                    gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    break;

                case WITCHER_DIRECTION.RIGHT:

                    gameObject.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                    break;

                case WITCHER_DIRECTION.LEFT:

                    gameObject.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                    break;

                default:
                    break;
            }
        }
    }
}
