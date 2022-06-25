using UnityEngine;
using WizardWar.Tile;
using WizardWar.TileObjects;

namespace WizardWar
{
    namespace Witcher 
    {
        namespace Movement 
        {
            public class PlayerMovement : IMovable
            {
                private const float _displacementTime = 0.3f;

                private float _freezePos;

                private float _movementTimer;

                private float _percentageMoved;

                private Index2 _movementAxis;

                private Index2 _nextTileIndex;

                private Vector3 _oldPosition;

                private Vector3 _newPosition;       

                private TileObjectsPositioningInTileMap _tileObjectsPositioningInTileMap;

                private TileObjectsInstanciator _tileObjectsInstanciator;

                public PlayerMovement() 
                {
                    _freezePos = 0f;

                    _movementTimer = _displacementTime;

                    _percentageMoved = 0f;

                    _movementAxis = Index2.IndexNull;

                    _nextTileIndex = Index2.IndexNull;

                    _oldPosition = Vector3.zero;

                    _newPosition = Vector3.zero;                   

                    _tileObjectsInstanciator = GameObject.FindWithTag("Manager").GetComponent<TileObjectsInstanciator>();

                    _tileObjectsPositioningInTileMap = _tileObjectsInstanciator.TileObjectsPositioningInTileMap;
                }

                public void MoveInput() 
                {
                    _movementAxis.X = (short)Input.GetAxisRaw("Horizontal");

                    _movementAxis.Y = (short)Input.GetAxisRaw("Vertical");

                    if (_movementAxis.Y != 0)
                    {
                        _movementAxis.Y *= -1;
                    }

                    if (_movementAxis.X != 0 && _movementAxis.Y != 0) 
                    {
                        _movementAxis.Y = 0;
                    }

                    _freezePos = Input.GetAxisRaw("Freeze");
                }    

                public void Move(GameObject gameObject, ref WitcherLookingDirection direction)
                {
                    if (_movementAxis != Index2.IndexNull && !IsObjectMoving()) 
                    {
                        if (_freezePos != 0f) 
                        {
                            RotatePlayer(_movementAxis, ref direction, gameObject);
                        }
                        else 
                        {
                            _nextTileIndex = _tileObjectsPositioningInTileMap.GetTileObjectIndexPlusOtherIndex(gameObject, _movementAxis);

                            if (_tileObjectsInstanciator.LevelCreator.TileMap.IsTileEmpty(_nextTileIndex))
                            {
                                _movementTimer = 0f;

                                _oldPosition = _tileObjectsPositioningInTileMap.GetTileMapPosition(_tileObjectsPositioningInTileMap.GetTileObjectIndex(gameObject));

                                _oldPosition.y = gameObject.transform.position.y;

                                _newPosition = _tileObjectsPositioningInTileMap.GetTileMapPosition(_nextTileIndex);

                                _newPosition.y = gameObject.transform.position.y;

                                _tileObjectsPositioningInTileMap.MoveGameObjectToTileX(_nextTileIndex, gameObject);                              
                            }

                            RotatePlayer(_movementAxis, ref direction, gameObject);
                        }
                    }        

                    if (IsObjectMoving())
                    {
                        _percentageMoved = _movementTimer / _displacementTime;

                        gameObject.transform.position = Vector3.Lerp(_oldPosition, _newPosition, _percentageMoved);
                    }
                }   

                public void Timer() 
                {
                    if (_movementTimer < _displacementTime) 
                    {
                        _movementTimer += Time.deltaTime;

                        if (_movementTimer > _displacementTime)
                        {
                            _movementTimer = _displacementTime;
                        }            
                    }        
                }   

                public bool IsObjectMoving()
                {
                    return _movementTimer < _displacementTime;
                }

                void RotatePlayer(Index2 movementAxis, ref WitcherLookingDirection witcherDirection, GameObject gameObject) 
                {
                    WitcherLookingDirection newDirection = WitcherLookingDirection.Left;

                    switch (movementAxis.X) 
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

                    if (movementAxis.X == 0) 
                    {
                        switch (movementAxis.Y)
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
                        witcherDirection = newDirection;

                        switch (witcherDirection) 
                        {
                            case WitcherLookingDirection.Down:

                                gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                break;
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
                }              
            }
        }
    }
}