using UnityEngine;

public class Player_Movement : IMovable
{   
    private Vector2 movementAxis;

    private Vector3 oldPosition;

    private Vector3 position;

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
        movementAxis.x = Input.GetAxisRaw("Horizontal");
        
        if (movementAxis.x == 0) 
        {
            movementAxis.y = Input.GetAxisRaw("Vertical");                       
        }                
    }

    public void Move(GameObject gameObject, ref WITCHER_DIRECTION direction) 
    {
        if (moveCooldown < cooldownTime && oldPosition != position)
        {
            gameObject.transform.position = Vector3.Lerp(oldPosition, position, percentageMoved);            
        }

        if (movementAxis.x != 0 || movementAxis.y != 0) 
        {
            RotatePlayer(movementAxis, ref direction, gameObject);

            oldPosition = Tile_Map.GetTileMapPosition(Tile_Map.GetGameObjectUpIndex(gameObject));
            
            moveCooldown = 0f;
            percentageMoved = 0f;

            Tile_Map.MoveGameObjectToTileX(Tile_Map.GetGameObjectIndexPlusOtherIndex(gameObject, movementAxis), gameObject);            

            position = Tile_Map.GetTileMapPosition(Tile_Map.GetGameObjectUpIndex(gameObject));

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
