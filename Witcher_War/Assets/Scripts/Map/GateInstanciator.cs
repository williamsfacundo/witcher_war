using UnityEngine;
using WizardWar.Tile;
using WizardWar.TileObjects;
using WizardWar.GameplayObjects;

namespace WizardWar 
{
    namespace Gate
    {
        public class GateInstanciator : MonoBehaviour
        {
            private const short _maxProbability = 100;

            [SerializeField] private GameObject _gatePrefab;

            [SerializeField] [Range(0, _maxProbability)] private short _probabilityToSpawnGate = 35;

            private TileObjectsInstanciator _tileObjectsInstanciator;

            private bool gateInstanciated;

            private GameObject gate;

            private void OnEnable()
            {
                DestroyableObject.BookshelfAboutToBeDestroyed += InstanciateGateMechanic;
            }

            private void OnDisable()
            {
                DestroyableObject.BookshelfAboutToBeDestroyed -= InstanciateGateMechanic;
            }           

            private void Start()
            {
                _tileObjectsInstanciator = GameObject.FindWithTag("Manager").GetComponent<Gameplay>().TileObjectsInstanciator;                

                gateInstanciated = false;
            }

            private void InstanciateGateMechanic()
            {
                if (_tileObjectsInstanciator != null)
                {
                    if (!gateInstanciated)
                    {
                        if (TileObjectsInstanciator.BookshelfsCount > 0)
                        {
                            if ((short)Random.Range(1, _maxProbability + 1) <= _probabilityToSpawnGate) 
                            {
                                InstanciateGate();
                            }                            
                        }
                        else
                        {
                            InstanciateGate();
                        }
                    }
                }
            }

            private void InstanciateGate()
            {
                if (!gateInstanciated && _gatePrefab != null)
                {
                    gateInstanciated = true;

                    Index2 gateIndex = _tileObjectsInstanciator.TileObjectsPositioningInTileMap.GetRandomEmptyIndex();

                    if (gateIndex != Index2.IndexNull)
                    {
                        gate = Instantiate(_gatePrefab);

                        _tileObjectsInstanciator.TileObjectsPositioningInTileMap.SetSpecialTile(gate, gateIndex);

                        gate.transform.position = _tileObjectsInstanciator.TileObjectsPositioningInTileMap.GetTileMapPosition(gateIndex);
                    }
                }
            }

            public void DestroyGate()
            {
                if (gateInstanciated)
                {
                    _tileObjectsInstanciator.TileObjectsPositioningInTileMap.DestroySpecialTile();

                    gateInstanciated = false;
                }
            }
        }
    } 
}