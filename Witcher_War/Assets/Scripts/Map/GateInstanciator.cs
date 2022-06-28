using UnityEngine;
using WizardWar.Tile;
using WizardWar.TileObjects;

namespace WizardWar 
{
    namespace Gate
    {
        public class GateInstanciator : MonoBehaviour
        {
            [SerializeField] private GameObject _gatePrefab;

            [SerializeField] [Range(0, 100)] private short _probabilityToSpawnGate = 35;

            [SerializeField] private TileObjectsInstanciator _tileObjectsInstanciator;

            private bool gateInstanciated = false;

            private GameObject gate;

            private void OnEnable()
            {
                DestroyableObject.objectAboutToBeDestroyed += InstanciateGateMechanic;
            }

            private void OnDisable()
            {
                DestroyableObject.objectAboutToBeDestroyed -= InstanciateGateMechanic;
            }

            private void InstanciateGateMechanic()
            {
                if (_tileObjectsInstanciator != null)
                {
                    if (!gateInstanciated)
                    {
                        if (Random.Range(1, 100) <= _probabilityToSpawnGate) //Hacer que si la cantidad de librerias es igual a cero si o si instanciar portal
                        {
                            if (Random.Range(1, 100) <= _probabilityToSpawnGate)
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
                if (_gatePrefab != null)
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
                if (gate != null)
                {
                    _tileObjectsInstanciator.TileObjectsPositioningInTileMap.DestroySpecialTile(gameObject);

                    gateInstanciated = false;
                }
            }
        }
    } 
}