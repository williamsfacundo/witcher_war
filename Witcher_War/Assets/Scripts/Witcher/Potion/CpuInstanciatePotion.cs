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
            public class CpuInstanciatePotion : IPotionInstanceable
            {
                private const float _minTimeToSpawnPotion = 3.5f;
                private const float _maxTimeToSpawnPotion = 6.5f;

                private float _timer;

                private CpuMovement _cpuMovement;
                
                private Gameplay _gameplay;

                private GameObject _witcher;

                public CpuInstanciatePotion(GameObject witcher, CpuMovement cpuMovement)
                {
                    RandomTime();

                    _cpuMovement = cpuMovement;

                    _gameplay = GameObject.FindWithTag("Manager").GetComponent<Gameplay>();

                    _witcher = witcher;
                }

                public void InstanciatePotion(GameObject potionPrefab, WitcherLookingDirection direction)
                {
                    if (_timer > 0f)
                    {
                        _timer -= Time.deltaTime;
                    }

                    if (!_cpuMovement.IsMoving() && _timer <= 0f)
                    {
                        Index2 indexWhereWitcherIsLooking = WitcherController.GetIndexWhereWitcherIsLooking(_witcher, _gameplay, direction);

                        Index2 opositeTileIndex = WitcherController.GetOpositeTileDirectionOfTheOneWitcherIsLooking(direction);

                        if (_gameplay.LevelCreator.TileMap.IsTileEmpty(indexWhereWitcherIsLooking) && _gameplay.LevelCreator.TileMap.IsTileEmpty(opositeTileIndex))
                        {
                            GameObject potion = Object.Instantiate(potionPrefab);

                            _gameplay.TileObjectsPositioningInTileMap.NewGameObjectInTile(indexWhereWitcherIsLooking, potion);

                            _gameplay.TileObjectsInstanciator.GameObjectPositioningCorrectly(potion, indexWhereWitcherIsLooking);

                            potion.GetComponent<PotionExplotion>().ExplosionIndex = indexWhereWitcherIsLooking;                            

                            _cpuMovement.AlterMovementToScapeBombExplotion(opositeTileIndex);
                        }

                        RandomTime();
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