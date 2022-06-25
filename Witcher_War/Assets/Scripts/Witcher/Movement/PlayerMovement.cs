using UnityEngine;

namespace WizardWar
{
    namespace Witcher 
    {
        namespace Movement 
        {
            public class PlayerMovement : IMovable
            {
                /*private Vector2 movementAxis;

                private float freezePos;

                private Vector3 oldPosition;

                private Vector3 newPosition;

                private const float displacementTime = 0.3f;

                private float movementTimer;        

                private float percentageMoved;

                private Vector2 nextTileIndex;*/

                public PlayerMovement() 
                {
                    //movementAxis = Vector2.zero;
                    //oldPosition = Vector2.zero;
                    //movementTimer = displacementTime;        
                    //percentageMoved = 0f;
                    //nextTileIndex = Vector2.zero;        
                }

                /*public void MoveInput() 
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

                public void Move(GameObject gameObject, ref WitcherLookingDirection direction)
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

                void RotatePlayer(Vector2 movementAxis, ref WitcherLookingDirection witcherDirection, GameObject gameObject) 
                {
                    WitcherLookingDirection newDirection = WitcherLookingDirection.Left;

                    switch ((int)movementAxis.x) 
                    {
                        case -1:

                            newDirection = WitcherLookingDirection.Left;
                            break;
                        case 1:

                            newDirection = WitcherLookingDirection.Right;
                            break;
                        default:
                            break;
                    }

                    if (movementAxis.x == 0) 
                    {
                        switch ((int)movementAxis.y)
                        {
                            case -1:

                                newDirection = WitcherLookingDirection.Up;
                                break;
                            case 1:

                                newDirection = WitcherLookingDirection.Down;
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
                            case WitcherLookingDirection.Up:

                                gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                                break;

                            case WitcherLookingDirection.Right:

                                gameObject.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                                break;

                            case WitcherLookingDirection.Left:

                                gameObject.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                                break;

                            default:
                                break;
                        }
                    }
                }*/

                public void MoveInput()
                {

                }

                public void Move(GameObject gameObject, ref WitcherLookingDirection direction)
                {

                }

                public void Timer()
                {

                }

                public bool IsObjectMoving()
                {
                    return false;
                }
            }
        }
    }
}