using UnityEngine;

public class Cpu_Movement : IMovable
{
    const float minInputTime = 1.5f;
    const float maxInputTime = 5.5f;

    private const float displacementTime = 0.3f;

    private GameObject player;
    private GameObject cpu;

    WITCHER_DIRECTION potionInstanciatedMoveDirection;

    private bool moveHorizontal;
    private bool calculateNewPosition;
    private bool potionInstanciated;

    private Vector2 playerIndex;
    private Vector2 enemyIndex;    
    private Vector2 movementDirection;
    private Vector2 nextTileIndex;
    private Vector2 potionInstanciatedMoveIndex;

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

        potionInstanciated = false;

        enemyIndex = TileMap.nullIndex;

        movementDirection = TileMap.nullIndex;        

        nextTileIndex = TileMap.nullIndex;

        potionInstanciatedMoveIndex = TileMap.nullIndex;

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
        if (potionInstanciated) 
        {
            if (TileMap.IsTileEmpty(potionInstanciatedMoveIndex)) 
            {
                movementTimer = 0f;

                oldPos = TileMap.GetTileMapPosition(TileMap.GetGameObjectIndex(cpu));
                oldPos.y = gameObject.transform.position.y;
                newPos = TileMap.GetTileMapPosition(potionInstanciatedMoveIndex);
                newPos.y = gameObject.transform.position.y;

                TileMap.MoveGameObjectToTileX(potionInstanciatedMoveIndex, cpu);
            }

            RotatePlayer(potionInstanciatedMoveDirection, ref potionInstanciatedMoveDirection, gameObject);

            potionInstanciated = false;
        }
        else 
        {
            if (calculateNewPosition)
            {
                movementDirection = GetDirectionToMoveTowardsPlayer();
                nextTileIndex = TileMap.GetGameObjectIndexPlusOtherIndex(gameObject, movementDirection);

                if (TileMap.IsTileEmpty(nextTileIndex))
                {
                    movementTimer = 0f;

                    oldPos = TileMap.GetTileMapPosition(TileMap.GetGameObjectIndex(cpu));
                    oldPos.y = gameObject.transform.position.y;
                    newPos = TileMap.GetTileMapPosition(nextTileIndex);
                    newPos.y = gameObject.transform.position.y;

                    TileMap.MoveGameObjectToTileX(nextTileIndex, cpu);
                }

                RotatePlayer(movementDirection, ref direction, gameObject);
                calculateNewPosition = false;
            }
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

    public void PotionInstanciated(Vector2 moveIndex, WITCHER_DIRECTION newWitcherDirection) 
    {
        potionInstanciated = true;
        potionInstanciatedMoveIndex = moveIndex;
        potionInstanciatedMoveDirection = newWitcherDirection;
    }

    private void RandomMoveTimer()
    {
        inputTimer = Random.Range(minInputTime, maxInputTime);
    }    

    private Vector2 GetDirectionToMoveTowardsPlayer() 
    {
        playerIndex = TileMap.GetGameObjectIndex(player);
        enemyIndex = TileMap.GetGameObjectIndex(cpu);

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

    private void RotatePlayer(WITCHER_DIRECTION newDirection, ref WITCHER_DIRECTION witcherDirection, GameObject gameObject)
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
