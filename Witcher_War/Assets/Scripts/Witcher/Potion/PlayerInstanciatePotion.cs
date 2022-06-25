using UnityEngine;
using WizardWar.Tile;

namespace WizardWar 
{
    namespace Witcher 
    {
        namespace Potion
        {
            public class PlayerInstanciatePotion : ICanUsePotion
            {
                /*private const KeyCode instanciatePotionKey = KeyCode.Space;

                private const float maxPotions = 2;

                private const float newPotionTime = 1.5f;

                private float amountPotions;

                private float newPotionTimer;

                private TileObjectsInstanciator _tileObjectsInstanciator;*/

                public PlayerInstanciatePotion()
                {
                    //amountPotions = maxPotions;

                    //newPotionTimer = 0f;
                }

                /*public void InstanciatePotion(GameObject potionPrefab, Vector2 instantiatorIndex, WitcherLookingDirection direction)
                {
                    if (Input.GetKeyDown(instanciatePotionKey) && amountPotions > 0)
                    {
                        Index2 targetIndex = GetIndexWhereWitcherIsLooking(instantiatorIndex, direction);

                        if (TileMap.IsTileEmpty(targetIndex))
                        {
                            GameObject potion = Object.Instantiate(potionPrefab);

                            potion.GetComponent<StaticGameObject>().InitialPosIndex = targetIndex;
                            potion.GetComponent<PotionExplotion>().ExplosionIndex = targetIndex;

                            amountPotions--;
                        }
                    }

                    PotionRegeneration();
                }

                public static Index2 GetIndexWhereWitcherIsLooking(Vector2 index, WitcherLookingDirection witcherDirection)
                {
                    switch (witcherDirection)
                    {
                        case WitcherLookingDirection.Down:

                            return index + Vector2.up;

                        case WitcherLookingDirection.Up:

                            return index - Vector2.up;

                        case WitcherLookingDirection.Right:

                            return index + Vector2.right;
                        case WitcherLookingDirection.Left:

                            return index - Vector2.right;
                        default:

                            return index + Vector2.up;
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
                }*/

                public void InstanciatePotion(GameObject potionPrefab, Index2 instantiatorIndex, WitcherLookingDirection direction) 
                {

                }
            }
        }
    }
}