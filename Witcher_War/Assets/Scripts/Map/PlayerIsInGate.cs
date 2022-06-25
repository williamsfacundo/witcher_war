using UnityEngine;
using WizardWar.TileObjects;

namespace WizardWar 
{
    namespace Gate 
    {
        public class PlayerIsInGate : MonoBehaviour
        {
            private TileObjectsInstanciator _tileObjectsInstanciator;

            private GameObject _player;

            private void Start()
            {
                _player = GameObject.FindWithTag("Player");

                _tileObjectsInstanciator = GameObject.FindWithTag("Manager").GetComponent<TileObjectsInstanciator>();
            }

            void Update() //Agregar al segundo if la condicion de que la cantidad de librerias debe ser igual a cero
            {
                if (_player != null && _tileObjectsInstanciator != null)
                {
                    if (_tileObjectsInstanciator.TileObjectsPositioningInTileMap.GetTileObjectIndex(_player) == _tileObjectsInstanciator.TileObjectsPositioningInTileMap.SpecialTileIndex2)
                    {
                        if (Vector3.Distance(transform.position, _player.transform.position) <= _tileObjectsInstanciator.LevelCreator.TileMapSize.y)
                        {
                            if (_tileObjectsInstanciator.OnLastLevel())
                            {
                                _tileObjectsInstanciator.NextLevel();

                                _tileObjectsInstanciator.TileObjectsPositioningInTileMap.DestroySpecialTile(gameObject);
                            }
                            else //No cambia la escena sera la misma solo hay que activar el canvas de endgame defeat  
                            {
                                ScenesManagement.ChangeToWinningScene();
                            }
                        }
                    }
                }
            }
        }
    }
}