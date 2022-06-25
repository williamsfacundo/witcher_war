using UnityEngine;
using WizardWar.Tile;
using WizardWar.Witcher.Movement;
using WizardWar.Witcher.Potion;
using WizardWar.TileObjects;

namespace WizardWar 
{
    namespace Witcher 
    {
        public class WitcherController : MonoBehaviour, IDestroyable, ITileleable
        {
            private const float _generateNewPotionTime = 3f;

            private const short _initialBombsCarried = 2;

            private WitcherType _witcherType;

            private WitcherLookingDirection _witcherDirection;

            private GameObject _potionPrefab;

            private IMovable _movementMechanic;

            private ICanUsePotion _usePotionMechanic;

            private TileObjectsInstanciator _tileObjectsInstanciator;

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

                _tileObjectsInstanciator = GameObject.FindWithTag("Manager").GetComponent<TileObjectsInstanciator>();
            }

            private void Update()
            {
                /*if (!_movementMechanic.IsObjectMoving())
                {
                    _usePotionMechanic?.InstanciatePotion(_potionPrefab, _tileObjectsInstanciator.TileObjectsPositioningInTileMap.GetTileObjectIndex(gameObject), _witcherDirection);
                }

                _movementMechanic?.MoveInput();

                _movementMechanic?.Timer();*/
            }

            private void FixedUpdate()
            {
                //_movementMechanic?.Move(gameObject, ref _witcherDirection);
            }

            void SetWitcher(WitcherType witcherType)
            {
                CpuMovement auxCpuMovement;

                switch (witcherType)
                {
                    case WitcherType.Player:

                        _movementMechanic = new PlayerMovement();

                        _usePotionMechanic = new PlayerInstanciatePotion();

                        break;
                    case WitcherType.Cpu:


                        auxCpuMovement = new CpuMovement(gameObject);
                        _movementMechanic = auxCpuMovement;

                        _usePotionMechanic = new CpuInstanciatePotion(auxCpuMovement);

                        break;
                    default:

                        auxCpuMovement = new CpuMovement(gameObject);

                        _movementMechanic = auxCpuMovement;

                        _usePotionMechanic = new CpuInstanciatePotion(auxCpuMovement);
                        break;
                }
            }

            public void ObjectAboutToBeDestroyed()
            {
                if (_witcherType == WitcherType.Player) 
                {
                    ScenesManagement.ChangeToEndGameScene();
                }
            }
        }
    }
}