using UnityEngine;
using WizardWar.Tile;
using WizardWar.GameplayObjects;
using WizardWar.Enums;
using WizardWar.Witcher.Interfaces;

namespace WizardWar 
{
    namespace Witcher 
    {
        namespace Movement 
        {
            public class CpuMovement : IMovable
            {
                private const float _minInputTime = 1.5f;
                private const float _maxInputTime = 5.5f;

                private const float _displacementTime = 0.3f;

                private GameObject _player;
                private GameObject _cpu;

                WitcherLookingDirection _potionInstanciatedLookingDirection;

                private bool _moveHorizontal;
                private bool _calculateNewPosition;
                private bool _potionInstanciated;

                private Index2 _playerIndex;
                private Index2 _enemyIndex;
                private Index2 _movementDirection;
                private Index2 _nextTileIndex;
                private Index2 _potionInstanciatedMoveIndex;

                private Vector3 _newPos;
                private Vector3 _oldPos;

                private float _inputTimer;
                private float _movementTimer;
                private float _percentageMoved;

                private Gameplay _gameplay;

                public CpuMovement(GameObject cpuGameObject)
                {
                    _player = GameObject.FindWithTag("Player");

                    this._cpu = cpuGameObject;

                    RandomMoveTimer();

                    _moveHorizontal = true;

                    _calculateNewPosition = false;

                    _potionInstanciated = false;

                    _enemyIndex = Index2.IndexNull;

                    _movementDirection = Index2.IndexNull;

                    _nextTileIndex = Index2.IndexNull;

                    _potionInstanciatedMoveIndex = Index2.IndexNull;

                    _movementTimer = _displacementTime;

                    _percentageMoved = 0f;

                    _newPos = Vector2.zero;
                    _oldPos = Vector2.zero;

                    _gameplay = GameObject.FindWithTag("Manager").GetComponent<Gameplay>();                                        
                }

                public void MoveInput()
                {
                    if (_inputTimer <= 0f)
                    {
                        RandomMoveTimer();
                        _calculateNewPosition = true;
                    }
                }

                public void Move(GameObject gameObject, ref WitcherLookingDirection direction)
                {
                    if (_potionInstanciated)
                    {
                        if (_gameplay.TileObjectsInstanciator.LevelCreator.TileMap.IsTileEmpty(_potionInstanciatedMoveIndex))
                        {
                            _movementTimer = 0f;

                            _oldPos = _gameplay.TileObjectsPositioningInTileMap.GetTileMapPosition(_gameplay.TileObjectsPositioningInTileMap.GetTileObjectIndex(_cpu));

                            _oldPos.y = gameObject.transform.position.y;
                            
                            _newPos = _gameplay.TileObjectsPositioningInTileMap.GetTileMapPosition(_potionInstanciatedMoveIndex);
                            
                            _newPos.y = gameObject.transform.position.y;

                            _gameplay.TileObjectsPositioningInTileMap.MoveGameObjectToTileX(_potionInstanciatedMoveIndex, _cpu);
                        }

                        RotatePlayer(_potionInstanciatedLookingDirection, ref _potionInstanciatedLookingDirection, gameObject);

                        _potionInstanciated = false;
                    }
                    else
                    {
                        if (_calculateNewPosition)
                        {
                            _movementDirection = GetDirectionToMoveTowardsPlayer();

                            _nextTileIndex = _gameplay.TileObjectsPositioningInTileMap.GetTileObjectIndexPlusOtherIndex(gameObject, _movementDirection);

                            if (_gameplay.TileObjectsInstanciator.LevelCreator.TileMap.IsTileEmpty(_nextTileIndex))
                            {
                                _movementTimer = 0f;

                                _oldPos = _gameplay.TileObjectsPositioningInTileMap.GetTileMapPosition(_gameplay.TileObjectsPositioningInTileMap.GetTileObjectIndex(_cpu));

                                _oldPos.y = gameObject.transform.position.y;

                                _newPos = _gameplay.TileObjectsPositioningInTileMap.GetTileMapPosition(_nextTileIndex);

                                _newPos.y = gameObject.transform.position.y;

                                _gameplay.TileObjectsPositioningInTileMap.MoveGameObjectToTileX(_nextTileIndex, _cpu);
                            }

                            RotatePlayer(_movementDirection, ref direction, gameObject);

                            _calculateNewPosition = false;
                        }
                    }

                    if (IsObjectMoving())
                    {
                        _percentageMoved = _movementTimer / _displacementTime;

                        gameObject.transform.position = Vector3.Lerp(_oldPos, _newPos, _percentageMoved);
                    }
                }

                public void Timer()
                {
                    if (!IsObjectMoving())
                    {
                        _inputTimer -= Time.deltaTime;
                    }

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

                public void PotionInstanciated(Index2 moveIndex, WitcherLookingDirection newWitcherDirection)
                {
                    _potionInstanciated = true;
                    _potionInstanciatedMoveIndex = moveIndex;
                    _potionInstanciatedLookingDirection = newWitcherDirection;
                }

                private void RandomMoveTimer()
                {
                    _inputTimer = Random.Range(_minInputTime, _maxInputTime);
                }

                private Index2 GetDirectionToMoveTowardsPlayer()
                {
                    _playerIndex = _gameplay.TileObjectsPositioningInTileMap.GetTileObjectIndex(_player);

                    _enemyIndex = _gameplay.TileObjectsPositioningInTileMap.GetTileObjectIndex(_cpu);

                    if (_moveHorizontal)
                    {
                        if (_enemyIndex.X < _playerIndex.X)
                        {
                            _moveHorizontal = false;
                            return new Index2(1, 0);
                        }
                        else if (_enemyIndex.X > _playerIndex.X)
                        {
                            _moveHorizontal = false;
                            return new Index2(-1, 0);
                        }
                        else
                        {
                            if (_enemyIndex.Y < _playerIndex.X)
                            {
                                _moveHorizontal = false;
                                return new Index2(0, 1);
                            }
                            else
                            {
                                _moveHorizontal = false;
                                return new Index2(0, -1);
                            }
                        }
                    }
                    else
                    {
                        if (_enemyIndex.Y < _playerIndex.Y)
                        {
                            _moveHorizontal = true;
                            return new Index2(0, 1);
                        }
                        else if (_enemyIndex.Y > _playerIndex.Y)
                        {
                            _moveHorizontal = true;
                            return new Index2(0, -1);
                        }
                        else
                        {
                            if (_enemyIndex.X < _playerIndex.X)
                            {
                                _moveHorizontal = true;
                                return new Index2(1, 0);
                            }
                            else
                            {
                                _moveHorizontal = true;
                                return new Index2(-1, 0);
                            }
                        }
                    }
                }

                private void RotatePlayer(Index2 movementAxis, ref WitcherLookingDirection witcherDirection, GameObject gameObject)
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

                private void RotatePlayer(WitcherLookingDirection newDirection, ref WitcherLookingDirection witcherDirection, GameObject gameObject)
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