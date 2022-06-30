using UnityEngine;
using WizardWar.Tile;
using WizardWar.Witcher.Movement;
using WizardWar.Witcher.Potion;
using WizardWar.GameplayObjects;
using WizardWar.Scenes;
using WizardWar.Enums;
using WizardWar.Witcher.Interfaces;

namespace WizardWar 
{
    namespace Witcher 
    {
        public class WitcherController : MonoBehaviour, IDestroyable, ITileleable
        {
            private WitcherType _witcherType;

            private WitcherLookingDirection _witcherDirection;

            private GameObject _potionPrefab;

            private IMovable _movementMechanic;

            private ICanUsePotion _usePotionMechanic;

            private Gameplay _gameplay;

            public WitcherType WitcherType
            {
                set
                {
                    _witcherType = value;
                }
            }

            public WitcherLookingDirection WitcherDirection
            {
                set
                {
                    _witcherDirection = value;
                }
                get
                {
                    return _witcherDirection;
                }
            }

            public GameObject PotionPrefab
            {
                set
                {
                    _potionPrefab = value;
                }
                get
                {
                    return _potionPrefab;
                }
            }

            private void Start()
            {
                SetWitcher(_witcherType);

                transform.rotation = Quaternion.identity;

                WitcherDirection = WitcherLookingDirection.Down;

                _gameplay = GameObject.FindWithTag("Manager").GetComponent<Gameplay>();
            }

            private void Update()
            {
                _usePotionMechanic?.InstanciatePotion(_potionPrefab, _witcherDirection);

                _movementMechanic?.MoveInput();

                _movementMechanic?.Timer();
            }

            private void FixedUpdate()
            {
                _movementMechanic?.Move(gameObject, ref _witcherDirection);
            }

            public static Index2 GetIndexWhereWitcherIsLooking(GameObject witcher, Gameplay gameplay, WitcherLookingDirection witcherDirection)
            {
                Index2 witcherIndex = gameplay.TileObjectsPositioningInTileMap.GetTileObjectIndex(witcher);

                switch (witcherDirection)
                {
                    case WitcherLookingDirection.Down:

                        return witcherIndex + Index2.Up;

                    case WitcherLookingDirection.Up:

                        return witcherIndex - Index2.Up;

                    case WitcherLookingDirection.Right:

                        return witcherIndex + Index2.Right;
                    case WitcherLookingDirection.Left:

                        return witcherIndex - Index2.Right;
                    default:

                        return witcherIndex + Index2.Up;
                }
            }

            public static Index2 GetOpositeTileDirectionOfTheOneWitcherIsLooking(WitcherLookingDirection witcherDirection)
            {
                switch (witcherDirection)
                {
                    case WitcherLookingDirection.Down:

                        return -Index2.Up;
                    case WitcherLookingDirection.Up:

                        return Index2.Up;
                    case WitcherLookingDirection.Right:

                        return -Index2.Right;
                    case WitcherLookingDirection.Left:

                        return Index2.Right;
                    default:

                        return -Index2.Up;
                }
            }

            public void ObjectAboutToBeDestroyed()
            {
                if (_witcherType == WitcherType.Player)
                {
                    ScenesManagement.ChangeToEndGameScene();
                }
            }            

            private void SetWitcher(WitcherType witcherType)
            {
                switch (witcherType)
                {
                    case WitcherType.Player:

                        _movementMechanic = new PlayerMovement();

                        _usePotionMechanic = new PlayerInstanciatePotion(gameObject, (PlayerMovement)_movementMechanic);

                        break;
                    case WitcherType.Cpu:


                        CpuMovement auxCpuMovement = new CpuMovement(gameObject);
                        
                        _movementMechanic = auxCpuMovement;

                        _usePotionMechanic = new CpuInstanciatePotion(gameObject, auxCpuMovement);

                        break;
                    default:
                        
                        break;
                }
            }           
        }
    }
}