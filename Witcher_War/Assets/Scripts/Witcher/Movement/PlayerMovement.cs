using UnityEngine;
using WizardWar.Enums;
using WizardWar.GameplayObjects;
using WizardWar.Tile;
using WizardWar.Witcher.Interfaces;
using WizardWar.Witcher.RotationFuncs;

namespace WizardWar
{
    namespace Witcher
    {
        namespace Movement
        {
            public class PlayerMovement : IMovable
            {
                private const float _displacementTime = 0.3f;

                private short _freezePos;

                private float _movementTimer;

                private float _percentageMoved;

                private Index2 _movementAxis;

                private Index2 _nextTileIndex;                

                private Vector3 _oldPosition;

                private Vector3 _newPosition;                

                private Gameplay _gameplay;

                public PlayerMovement() 
                {
                    _freezePos = 0;

                    _movementTimer = _displacementTime;

                    _percentageMoved = 0f;

                    _movementAxis = Index2.IndexZero;                    

                    _nextTileIndex = Index2.IndexZero;

                    _oldPosition = Vector3.zero;

                    _newPosition = Vector3.zero;

                    _gameplay = GameObject.FindWithTag("Manager").GetComponent<Gameplay>();                   
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

                    _freezePos = (short)Input.GetAxisRaw("Freeze");                    
                }    

                public void Move(GameObject witcher, ref WitcherLookingDirection direction)
                {                    
                    if (_movementAxis != Index2.IndexZero && !IsObjectMoving()) 
                    {
                        if (_freezePos != 0) 
                        {                           
                            Rotation.RotateWitcher(witcher, _movementAxis, ref direction);
                        }
                        else 
                        {
                            _nextTileIndex = _gameplay.TileObjectsPositioningInTileMap.GetTileObjectIndexPlusOtherIndex(witcher, _movementAxis);
                            
                            if (_gameplay.TileObjectsInstanciator.LevelCreator.TileMap.IsTileEmpty(_nextTileIndex))
                            {
                                _movementTimer = 0f;

                                _oldPosition = _gameplay.TileObjectsPositioningInTileMap.GetTileMapPosition(_gameplay.TileObjectsPositioningInTileMap.GetTileObjectIndex(witcher));

                                _oldPosition.y = witcher.transform.position.y;

                                _newPosition = _gameplay.TileObjectsPositioningInTileMap.GetTileMapPosition(_nextTileIndex);

                                _newPosition.y = witcher.transform.position.y;

                                _gameplay.TileObjectsPositioningInTileMap.MoveGameObjectToTileX(_nextTileIndex, witcher);                              
                            }

                            Rotation.RotateWitcher(witcher, _movementAxis, ref direction);                            
                        }
                    }        

                    if (IsObjectMoving())
                    {
                        _percentageMoved = _movementTimer / _displacementTime;

                        witcher.transform.position = Vector3.Lerp(_oldPosition, _newPosition, _percentageMoved);                        
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
            }
        }
    }
}