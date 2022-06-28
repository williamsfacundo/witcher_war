using UnityEngine;
using WizardWar.Enums;
using WizardWar.Tile;
using WizardWar.GameplayObjects;
using WizardWar.Witcher.RotationFuncs;

namespace WizardWar 
{
    namespace Witcher 
    {
        namespace Movement 
        {
            public class Movement
            {
                private const float _displacementTime = 0.3f;

                private float _movementTimer;

                private float _percentageMoved;                

                private Index2 _nextTileIndex;

                private Vector3 _oldPosition;

                private Vector3 _newPosition;

                Gameplay _gameplay;

                protected Movement() 
                {
                    _movementTimer = _displacementTime;

                    _percentageMoved = 0f;                   

                    _nextTileIndex = Index2.IndexNull;

                    _oldPosition = Vector3.zero;

                    _newPosition = Vector3.zero;

                    _gameplay = GameObject.FindWithTag("Manager").GetComponent<Gameplay>();
                }                

                protected void MoveFromCurrentTileToNewTile(GameObject witcher, Index2 moveIndex,ref WitcherLookingDirection direction) 
                {
                    if (moveIndex != Index2.IndexZero && !IsMoving())
                    {
                        _nextTileIndex = _gameplay.TileObjectsPositioningInTileMap.GetTileObjectIndexPlusOtherIndex(witcher, moveIndex);

                        if (_gameplay.TileObjectsInstanciator.LevelCreator.TileMap.IsTileEmpty(_nextTileIndex))
                        {
                            _movementTimer = 0f;

                            _oldPosition = _gameplay.TileObjectsPositioningInTileMap.GetTileMapPosition(_gameplay.TileObjectsPositioningInTileMap.GetTileObjectIndex(witcher));

                            _oldPosition.y = witcher.transform.position.y;

                            _newPosition = _gameplay.TileObjectsPositioningInTileMap.GetTileMapPosition(_nextTileIndex);

                            _newPosition.y = witcher.transform.position.y;

                            _gameplay.TileObjectsPositioningInTileMap.MoveGameObjectToTileX(_nextTileIndex, witcher);
                        }

                        Rotation.RotateWitcher(witcher, moveIndex, ref direction);
                    }

                    if (IsMoving())
                    {
                        _percentageMoved = _movementTimer / _displacementTime;

                        witcher.transform.position = Vector3.Lerp(_oldPosition, _newPosition, _percentageMoved);
                    }
                }

                protected void UpdateMovementTimer() 
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

                protected bool IsMoving()
                {
                    return _movementTimer < _displacementTime;
                }
            }
        }
    }
}