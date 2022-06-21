using UnityEngine;

public class Player_Movement : IMovable
{   
    private Vector2 movementAxis;

    private float freezePos;

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
        
        movementAxis.y = Input.GetAxisRaw("Vertical");

        if (movementAxis.y != 0f)
        {
            movementAxis.y *= -1;
        }

        if (movementAxis.x != 0f && movementAxis.y != 0f) 
        {
            movementAxis.y = 0f;
        }

        freezePos = Input.GetAxisRaw("Freeze");
    }    

    public void Move(GameObject gameObject, ref WITCHER_DIRECTION direction)
    {
        if (movementAxis != Vector2.zero && !IsObjectMoving()) 
        {
            if (freezePos != 0f) 
            {
                RotatePlayer(movementAxis, ref direction, gameObject);
            }
            else 
            {
                nextTileIndex = TileMap.GetGameObjectIndexPlusOtherIndex(gameObject, movementAxis);

                if (TileMap.IsTileEmpty(nextTileIndex))
                {
                    movementTimer = 0f;

                    oldPosition = TileMap.GetTileMapPosition(TileMap.GetGameObjectIndex(gameObject));
                    oldPosition.y = gameObject.transform.position.y;
                    newPosition = TileMap.GetTileMapPosition(nextTileIndex);
                    newPosition.y = gameObject.transform.position.y;

                    TileMap.MoveGameObjectToTileX(nextTileIndex, gameObject);
                }

                RotatePlayer(movementAxis, ref direction, gameObject);
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
    
    public bool IsObjectMoving()
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

        if (movementAxis.x == 0) 
        {
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
