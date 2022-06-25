using UnityEngine;
using WizardWar.Witcher.Movement;
using WizardWar.TileObjects;
using WizardWar.Tile;

namespace WizardWar 
{
    namespace Witcher 
    {
        namespace Potion 
        {
            public class CpuInstanciatePotion : ICanUsePotion
            {
                /*private const float minTimeToSpawnPotion = 3.5f;
                private const float maxTimeToSpawnPotion = 6.5f;

                private float timer = 0f;

                private CpuMovement cpuMovement;

                TileObjectsInstanciator _tileObjectsInstanciator;*/

                public CpuInstanciatePotion(CpuMovement cpuMovement)
                {
                    //this.cpuMovement = cpuMovement;

                    //_tileObjectsInstanciator = GameObject.FindWithTag("Manager").GetComponent<TileObjectsInstanciator>();

                    //RandomTime();
                }

                /*public void InstanciatePotion(GameObject potionPrefab, Vector2 instantiatorIndex, WitcherLookingDirection direction)
                {
                    if (timer > 0f)
                    {
                        timer -= Time.deltaTime;
                    }

                    if (!cpuMovement.IsObjectMoving() && timer <= 0f)
                    {
                        Index2 targetIndex = PlayerInstanciatePotion.GetIndexWhereWitcherIsLooking(instantiatorIndex, direction);
                        Index2 moveIndex = GetOpositeTileIndex(instantiatorIndex, direction);

                        if (_tileObjectsInstanciator.LevelCreator.TileMap.IsIndexValid(targetIndex) && _tileObjectsInstanciator.LevelCreator.TileMap.IsTileEmpty(moveIndex))
                        {
                            GameObject potion = Object.Instantiate(potionPrefab);

                            potion.GetComponent<StaticGameObject>().InitialPosIndex = targetIndex;
                            potion.GetComponent<PotionExplotion>().ExplosionIndex = targetIndex;

                            direction = GetOpositeTileDirection(direction);
                            cpuMovement.PotionInstanciated(moveIndex, direction);
                        }

                        RandomTime();
                    }
                }

                public static Index2 GetOpositeTileIndex(Vector2 witcherIndex, WitcherLookingDirection witcherDirection)
                {
                    switch (witcherDirection)
                    {
                        case WitcherLookingDirection.Down:

                            return witcherIndex - Vector2.up;
                        case WitcherLookingDirection.Up:

                            return witcherIndex + Vector2.up;
                        case WitcherLookingDirection.Right:

                            return witcherIndex - Vector2.right;
                        case WitcherLookingDirection.Left:

                            return witcherIndex + Vector2.right;
                        default:

                            return witcherIndex - Vector2.up;
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
                    timer = Random.Range(minTimeToSpawnPotion, maxTimeToSpawnPotion);
                }*/

                public void InstanciatePotion(GameObject potionPrefab, Index2 instantiatorIndex, WitcherLookingDirection direction) 
                {

                }
            }
        }
    }
}