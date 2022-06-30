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
            public class CpuMovement : BasedMovement, IMovable
            {
                private const float _minInputTime = 1.5f;
                private const float _maxInputTime = 5.5f;                

                private GameObject _player;
                private GameObject _cpu;                

                private bool _moveHorizontal;               
                private bool _potionInstanciated;               
                
                private Index2 _movementIndex;
                private Index2 _potionEscapeIndex;

                private float _inputTimer;                

                public CpuMovement(GameObject cpuGameObject) : base()
                {
                    _player = GameObject.FindWithTag("Player");

                    _cpu = cpuGameObject;                    

                    RandomMoveTimer();

                    _moveHorizontal = true;                    

                    _potionInstanciated = false;                   

                    _movementIndex = Index2.IndexZero;
                    _potionEscapeIndex = Index2.IndexZero;
                }

                public void MoveInput()
                {
                    
                }

                public void Move(GameObject witcher, ref WitcherLookingDirection direction)
                {
                    if (_potionInstanciated)
                    {
                        _movementIndex = _potionEscapeIndex;

                        _potionInstanciated = false;                        

                        RandomMoveTimer();
                    }
                    else if (_inputTimer <= 0f)
                    {
                        _movementIndex = GetMoveIndexToMoveTowardsPlayer();                        

                        RandomMoveTimer();                       
                    }                  

                    MoveFromCurrentTileToNewTile(witcher, _movementIndex, ref direction);

                    if (_movementIndex != Index2.IndexZero)
                    {
                        _movementIndex = Index2.IndexZero;
                    }
                }

                public void Timer()
                {
                    FakeInputTimerUpdate();

                    UpdateMovementTimer();
                }

                public void FakeInputTimerUpdate() 
                {
                    if (!IsMoving())
                    {
                        _inputTimer -= Time.deltaTime;
                    }
                }                

                public void AlterMovementToScapeBombExplotion(Index2 moveIndex)
                {
                    _potionInstanciated = true;

                    _potionEscapeIndex = moveIndex;                    
                }                

                private void RandomMoveTimer()
                {
                    _inputTimer = Random.Range(_minInputTime, _maxInputTime);
                }

                private Index2 GetMoveIndexToMoveTowardsPlayer()
                {
                    Index2 _playerIndex = Gameplay.TileObjectsPositioningInTileMap.GetTileObjectIndex(_player);

                    Index2 _enemyIndex = Gameplay.TileObjectsPositioningInTileMap.GetTileObjectIndex(_cpu);

                    if (_moveHorizontal)
                    {
                        _moveHorizontal = !_moveHorizontal;

                        if (_enemyIndex.X < _playerIndex.X)
                        {
                            return new Index2(1, 0);
                        }
                        else if (_enemyIndex.X > _playerIndex.X)
                        {
                            return new Index2(-1, 0);
                        }
                        else
                        {
                            if (_enemyIndex.Y < _playerIndex.X)
                            {
                                return new Index2(0, 1);
                            }
                            else
                            {
                                return new Index2(0, -1);
                            }
                        }
                    }
                    else
                    {
                        if (_enemyIndex.Y < _playerIndex.Y)
                        {
                            return new Index2(0, 1);
                        }
                        else if (_enemyIndex.Y > _playerIndex.Y)
                        {
                            return new Index2(0, -1);
                        }
                        else
                        {
                            if (_enemyIndex.X < _playerIndex.X)
                            {
                                return new Index2(1, 0);
                            }
                            else
                            {
                                return new Index2(-1, 0);
                            }
                        }
                    }
                }                       
            }
        }    
    }
}