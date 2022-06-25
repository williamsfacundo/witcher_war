using UnityEngine;
using WizardWar.Tile;
using WizardWar.TileObjects;

namespace WizardWar 
{
    namespace Witcher 
    {
        namespace Movement 
        {
            public class CpuMovement : IMovable
            {
                /*private const float _minInputTime = 1.5f;
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

                TileObjectsPositioningInTileMap _tileObjectsPositioningInTileMap;*/

                public CpuMovement(GameObject cpuGameObject)
                {
                    //_player = GameObject.FindWithTag("Player");

                    //this._cpu = cpuGameObject;

                    //RandomMoveTimer();

                    //_moveHorizontal = true;

                    //_calculateNewPosition = false;

                    //_potionInstanciated = false;

                    //_enemyIndex = Index2.IndexNull;

                    //_movementDirection = Index2.IndexNull;

                    //_nextTileIndex = Index2.IndexNull;

                    //_potionInstanciatedMoveIndex = Index2.IndexNull;

                    //_movementTimer = _displacementTime;

                    //_percentageMoved = 0f;

                    //_newPos = Vector2.zero;
                    //_oldPos = Vector2.zero;

                    //_tileObjectsPositioningInTileMap = GameObject.FindWithTag("Manager").GetComponent<TileObjectsInstanciator>().TileObjectsPositioningInTileMap;
                }

                /*public void MoveInput()
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
                        if (TileMap.IsTileEmpty(_potionInstanciatedMoveIndex))
                        {
                            _movementTimer = 0f;

                            _oldPos = TileMap.GetTileMapPosition(TileMap.GetGameObjectIndex(_cpu));
                            _oldPos.y = gameObject.transform.position.y;
                            _newPos = TileMap.GetTileMapPosition(_potionInstanciatedMoveIndex);
                            _newPos.y = gameObject.transform.position.y;

                            TileMap.MoveGameObjectToTileX(_potionInstanciatedMoveIndex, _cpu);
                        }

                        RotatePlayer(_potionInstanciatedLookingDirection, ref _potionInstanciatedLookingDirection, gameObject);

                        _potionInstanciated = false;
                    }
                    else
                    {
                        if (_calculateNewPosition)
                        {
                            _movementDirection = GetDirectionToMoveTowardsPlayer();
                            _nextTileIndex = TileMap.GetGameObjectIndexPlusOtherIndex(gameObject, _movementDirection);

                            if (TileMap.IsTileEmpty(_nextTileIndex))
                            {
                                _movementTimer = 0f;

                                _oldPos = TileMap.GetTileMapPosition(TileMap.GetGameObjectIndex(_cpu));
                                _oldPos.y = gameObject.transform.position.y;
                                _newPos = TileMap.GetTileMapPosition(_nextTileIndex);
                                _newPos.y = gameObject.transform.position.y;

                                TileMap.MoveGameObjectToTileX(_nextTileIndex, _cpu);
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

                private Vector2 GetDirectionToMoveTowardsPlayer()
                {
                    _playerIndex = TileMap.GetGameObjectIndex(_player);
                    _enemyIndex = TileMap.GetGameObjectIndex(_cpu);

                    if (_moveHorizontal)
                    {
                        if (_enemyIndex.x < _playerIndex.x)
                        {
                            _moveHorizontal = false;
                            return new Vector2(1f, 0f);
                        }
                        else if (_enemyIndex.x > _playerIndex.x)
                        {
                            _moveHorizontal = false;
                            return new Vector2(-1f, 0f);
                        }
                        else
                        {
                            if (_enemyIndex.y < _playerIndex.y)
                            {
                                _moveHorizontal = false;
                                return new Vector2(0f, 1f);
                            }
                            else
                            {
                                _moveHorizontal = false;
                                return new Vector2(0f, -1f);
                            }
                        }
                    }
                    else
                    {
                        if (_enemyIndex.y < _playerIndex.y)
                        {
                            _moveHorizontal = true;
                            return new Vector2(0f, 1f);
                        }
                        else if (_enemyIndex.y > _playerIndex.y)
                        {
                            _moveHorizontal = true;
                            return new Vector2(0f, -1f);
                        }
                        else
                        {
                            if (_enemyIndex.x < _playerIndex.x)
                            {
                                _moveHorizontal = true;
                                return new Vector2(1f, 0f);
                            }
                            else
                            {
                                _moveHorizontal = true;
                                return new Vector2(-1f, 0f);
                            }
                        }
                    }
                }

                private void RotatePlayer(Vector2 movementAxis, ref WitcherLookingDirection witcherDirection, GameObject gameObject)
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
                }

                private void RotatePlayer(WitcherLookingDirection newDirection, ref WitcherLookingDirection witcherDirection, GameObject gameObject)
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