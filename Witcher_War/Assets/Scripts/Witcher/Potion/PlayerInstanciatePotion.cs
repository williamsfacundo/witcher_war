using UnityEngine;
using WizardWar.Tile;
using WizardWar.TileObjects;

namespace WizardWar 
{
    namespace Witcher 
    {
        namespace Potion
        {
            public class PlayerInstanciatePotion : ICanUsePotion
            {
                private const KeyCode instanciatePotionKey = KeyCode.Space;

                private const float maxPotions = 2;

                private const float newPotionTime = 1.5f;

                private float amountPotions;

                private float newPotionTimer;

                private TileObjectsInstanciator _tileObjectsInstanciator;

                public PlayerInstanciatePotion()
                {
                    amountPotions = maxPotions;

                    newPotionTimer = 0f;

                    _tileObjectsInstanciator = GameObject.FindWithTag("Manager").GetComponent<TileObjectsInstanciator>();
                }

                public void InstanciatePotion(GameObject potionPrefab, Index2 instantiatorIndex, WitcherLookingDirection direction)
                {
                    if (Input.GetKeyDown(instanciatePotionKey) && amountPotions > 0)
                    {
                        Index2 targetIndex = GetIndexWhereWitcherIsLooking(instantiatorIndex, direction);

                        if (_tileObjectsInstanciator.LevelCreator.TileMap.IsTileEmpty(targetIndex))
                        {
                            GameObject potion = Object.Instantiate(potionPrefab);

                            _tileObjectsInstanciator.TileObjectsPositioningInTileMap.NewGameObjectInTile(instantiatorIndex, potion);
                            
                            potion.GetComponent<PotionExplotion>().ExplosionIndex = targetIndex;

                            amountPotions--;
                        }
                    }

                    PotionRegeneration();
                }

                public static Index2 GetIndexWhereWitcherIsLooking(Index2 index, WitcherLookingDirection witcherDirection)
                {
                    switch (witcherDirection)
                    {
                        case WitcherLookingDirection.Down:

                            return index + Index2.Up;

                        case WitcherLookingDirection.Up:

                            return index - Index2.Up;

                        case WitcherLookingDirection.Right:

                            return index + Index2.Right;
                        case WitcherLookingDirection.Left:

                            return index - Index2.Right;
                        default:

                            return index + Index2.Up;
                    }
                }

                private void PotionRegeneration()
                {
                    if (amountPotions < maxPotions)
                    {
                        newPotionTimer += Time.deltaTime;
                    }

                    if (newPotionTimer >= newPotionTime)
                    {
                        newPotionTimer = 0f;
                        amountPotions++;
                    }
                }                
            }
        }
    }
}