using UnityEngine;
using WizardWar.Tile;
using WizardWar.GameplayObjects;
using WizardWar.Enums;
using WizardWar.Witcher.Interfaces;
using WizardWar.Witcher.Movement;

namespace WizardWar 
{
    namespace Witcher 
    {
        namespace Potion
        {
            public class PlayerInstanciatePotion : IPotionInstanceable
            {
                private const KeyCode _instanciatePotionKey = KeyCode.Space;

                private const float _maxPotions = 2;

                private const float _newPotionTime = 1.5f;

                private float _amountPotions;

                private float _newPotionTimer;

                PlayerMovement _playerMovement;

                private Gameplay _gameplay;

                private GameObject _witcher;

                public PlayerInstanciatePotion(GameObject witcher, PlayerMovement playerMovement)
                {
                    _amountPotions = _maxPotions;

                    _newPotionTimer = 0f;

                    _playerMovement = playerMovement;

                    _gameplay = GameObject.FindWithTag("Manager").GetComponent<Gameplay>();

                    _witcher = witcher;
                }

                public void InstanciatePotion(GameObject potionPrefab, WitcherLookingDirection direction)
                {
                    if (Input.GetKeyDown(_instanciatePotionKey) && _amountPotions > 0 && !_playerMovement.IsMoving())
                    {
                        Index2 targetIndex = WitcherController.GetIndexWhereWitcherIsLooking(_witcher, _gameplay, direction);

                        if (_gameplay.LevelCreator.TileMap.IsTileEmpty(targetIndex))
                        {
                            GameObject potion = Object.Instantiate(potionPrefab);

                            _gameplay.TileObjectsInstanciator.TileObjectsPositioningInTileMap.NewGameObjectInTile(targetIndex, potion);

                            _gameplay.TileObjectsInstanciator.GameObjectPositioningCorrectly(potion, targetIndex);

                            potion.GetComponent<PotionExplotion>().ExplosionIndex = targetIndex;

                            _amountPotions--;
                        }
                    }

                    PotionRegeneration();
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