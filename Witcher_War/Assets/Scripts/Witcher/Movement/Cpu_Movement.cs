using UnityEngine;

public class Cpu_Movement : IMovable
{
    const float minInputTime = 1.5f;
    const float maxInputTime = 4.5f;

    private const float displacementTime = 0.3f;

    private GameObject player;
    private GameObject cpu;

    private bool moveHorizontal;
    private bool calculateNewPosition;

    private Vector2 playerIndex;
    private Vector2 enemyIndex;    
    private Vector2 movementDirection;
    private Vector2 nextTileIndex;

    private Vector3 newPos;
    private Vector3 oldPos;

    private float inputTimer;
    private float movementTimer;
    private float percentageMoved;

    public Cpu_Movement(GameObject cpuGameObject) 
    {
        player = GameObject.FindWithTag("Player");

        this.cpu = cpuGameObject;

        RandomMoveTimer();

        moveHorizontal = true;

        calculateNewPosition = false;

        enemyIndex = Tile_Map.nullIndex;

        movementDirection = Tile_Map.nullIndex;

        nextTileIndex = Tile_Map.nullIndex;

        movementTimer = displacementTime;

        percentageMoved = 0f;

        newPos = Vector2.zero;
        oldPos = Vector2.zero;
    }    

    public void MoveInput() 
    {
        if (inputTimer <= 0f) 
        {            
            RandomMoveTimer();
            calculateNewPosition = true;
        }
    }

    public void Move(GameObject gameObject, ref WITCHER_DIRECTION direction)
    {
        if (calculateNewPosition) 
        {
            movementDirection = GetDirectionToMoveTowardsPlayer();
            nextTileIndex = Tile_Map.GetGameObjectIndexPlusOtherIndex(gameObject, movementDirection);

            if (Tile_Map.IsTileEmpty(nextTileIndex)) 
            {
                movementTimer = 0f;

                oldPos = Tile_Map.GetTileMapPosition(Tile_Map.GetGameObjectIndex(cpu));
                newPos = Tile_Map.GetTileMapPosition(nextTileIndex);

                Tile_Map.MoveGameObjectToTileX(nextTileIndex, cpu);
            }

            RotatePlayer(movementDirection, ref direction, gameObject);
            calculateNewPosition = false;
        }

        if (IsObjectMoving())
        {
            percentageMoved = movementTimer / displacementTime;

            gameObject.transform.position = Vector3.Lerp(oldPos, newPos, percentageMoved);
        }
    }

    public void Timer() 
    {
        if (!IsObjectMoving()) 
        {
            inputTimer -= Time.deltaTime;            
        }        

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

    private void RandomMoveTimer()
    {
        inputTimer = Random.Range(minInputTime, maxInputTime);
    }
    private Vector2 GetDirectionToMoveTowardsPlayer() 
    {
        playerIndex = Tile_Map.GetGameObjectIndex(player);
        enemyIndex = Tile_Map.GetGameObjectIndex(cpu);

        if (moveHorizontal) 
        {
            if (enemyIndex.x < playerIndex.x) 
            {
                moveHorizontal = false;
                return new Vector2(1f, 0f); 
            }
            else if(enemyIndex.x > playerIndex.x)
            {
                moveHorizontal = false;
                return new Vector2(-1f, 0f);
            }
            else 
            {
                if (enemyIndex.y < playerIndex.y)
                {
                    moveHorizontal = false;
                    return new Vector2(0f, 1f);
                }
                else 
                {
                    moveHorizontal = false;
                    return new Vector2(0f, -1f);
                }
            }
        }
        else
        {
            if (enemyIndex.y < playerIndex.y)
            {
                moveHorizontal = true;
                return new Vector2(0f, 1f);
            }
            else if (enemyIndex.y > playerIndex.y)
            {
                moveHorizontal = true;
                return new Vector2(0f, -1f);
            }
            else
            {
                if (enemyIndex.x < playerIndex.x)
                {
                    moveHorizontal = true;
                    return new Vector2(1f, 0f);
                }
                else
                {
                    moveHorizontal = true;
                    return new Vector2(-1f, 0f);
                }
            }
        }        
    }

    private void RotatePlayer(Vector2 movementAxis, ref WITCHER_DIRECTION witcherDirection, GameObject gameObject)
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
