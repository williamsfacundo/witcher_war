using UnityEngine;
using WizardWar.Tile;
using WizardWar.GameplayObjects;

namespace WizardWar 
{
    namespace Witcher 
    {
        namespace Potion
        {
            public class PlayerInstanciatePotion : ICanUsePotion
            {
                private const KeyCode _instanciatePotionKey = KeyCode.Space;

                private const float _maxPotions = 2;

                private const float _newPotionTime = 1.5f;

                private float _amountPotions;

                private float _newPotionTimer;
                
                private Gameplay _gameplay;

                public PlayerInstanciatePotion()
                {
                    _amountPotions = _maxPotions;

                    _newPotionTimer = 0f;

                    _gameplay = GameObject.FindWithTag("Manager").GetComponent<Gameplay>();
                }

                public void InstanciatePotion(GameObject potionPrefab, Index2 instantiatorIndex, WitcherLookingDirection direction)
                {
                    if (Input.GetKeyDown(_instanciatePotionKey) && _amountPotions > 0)
                    {
                        Index2 targetIndex = GetIndexWhereWitcherIsLooking(instantiatorIndex, direction);

                        if (_gameplay.TileObjectsInstanciator.LevelCreator.TileMap.IsTileEmpty(targetIndex))
                        {
                            GameObject potion = Object.Instantiate(potionPrefab);

                            _gameplay.TileObjectsInstanciator.TileObjectsPositioningInTileMap.NewGameObjectInTile(instantiatorIndex, potion);
                            
                            potion.GetComponent<PotionExplotion>().ExplosionIndex = targetIndex;

                            _amountPotions--;
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
                    if (_amountPotions < _maxPotions)
                    {
                        _newPotionTimer += Time.deltaTime;
                    }

                    if (_newPotionTimer >= _newPotionTime)
                    {
                        _newPotionTimer = 0f;
                        _amountPotions++;
                    }
                }                
            }
        }
    }
}