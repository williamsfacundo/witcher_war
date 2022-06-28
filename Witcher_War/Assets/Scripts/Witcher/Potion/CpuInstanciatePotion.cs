using UnityEngine;
using WizardWar.Witcher.Movement;
using WizardWar.GameplayObjects;
using WizardWar.Tile;
using WizardWar.Enums;
using WizardWar.Witcher.Interfaces;

namespace WizardWar 
{
    namespace Witcher 
    {
        namespace Potion 
        {
            public class CpuInstanciatePotion : ICanUsePotion
            {
                private const float _minTimeToSpawnPotion = 3.5f;
                private const float _maxTimeToSpawnPotion = 6.5f;

                private float _timer;

                private CpuMovement _cpuMovement;
                
                private Gameplay _gameplay;

                public CpuInstanciatePotion(CpuMovement cpuMovement)
                {
                    _timer = 0f;

                    _cpuMovement = cpuMovement;

                    _gameplay = GameObject.FindWithTag("Manager").GetComponent<Gameplay>();

                    RandomTime();
                }

                public void InstanciatePotion(GameObject potionPrefab, Index2 instantiatorIndex, WitcherLookingDirection direction)
                {
                    if (_timer > 0f)
                    {
                        _timer -= Time.deltaTime;
                    }

                    if (!_cpuMovement.IsObjectMoving() && _timer <= 0f)
                    {
                        Index2 targetIndex = PlayerInstanciatePotion.GetIndexWhereWitcherIsLooking(instantiatorIndex, direction);
                        Index2 moveIndex = GetOpositeTileIndex(instantiatorIndex, direction);

                        if (_gameplay.LevelCreator.TileMap.IsTileEmpty(targetIndex) && _gameplay.LevelCreator.TileMap.IsTileEmpty(moveIndex))
                        {
                            GameObject potion = Object.Instantiate(potionPrefab);

                            _gameplay.TileObjectsPositioningInTileMap.NewGameObjectInTile(targetIndex, potion);

                            _gameplay.TileObjectsInstanciator.GameObjectPositioningCorrectly(potion, targetIndex);

                            potion.GetComponent<PotionExplotion>().ExplosionIndex = targetIndex;                            

                            direction = GetOpositeTileDirection(direction);

                            _cpuMovement.PotionInstanciated(moveIndex, direction);
                        }

                        RandomTime();
                    }
                }

                public static Index2 GetOpositeTileIndex(Index2 witcherIndex, WitcherLookingDirection witcherDirection)
                {
                    switch (witcherDirection)
                    {
                        case WitcherLookingDirection.Down:

                            return witcherIndex - Index2.Up;
                        case WitcherLookingDirection.Up:

                            return witcherIndex + Index2.Up;
                        case WitcherLookingDirection.Right:

                            return witcherIndex - Index2.Right;
                        case WitcherLookingDirection.Left:

                            return witcherIndex + Index2.Right;
                        default:

                            return witcherIndex - Index2.Up;
                    }
                }

                public static WitcherLookingDirection GetOpositeTileDirection(WitcherLookingDirection witcherDirection)
                {
                    switch (witcherDirection)
                    {
                        case WitcherLookingDirection.Down:

                            return WitcherLookingDirection.Up;
                        case WitcherLookingDirection.Up:

                            return WitcherLookingDirection.Down;
                        case WitcherLookingDirection.Right:

                            return WitcherLookingDirection.Left;
                        case WitcherLookingDirection.Left:

                            return WitcherLookingDirection.Right;
                        default:

                            return WitcherLookingDirection.Up;
                    }
                }

                private void RandomTime()
                {
                    _timer = Random.Range(_minTimeToSpawnPotion, _maxTimeToSpawnPotion);
                }                
            }
        }
    }
}