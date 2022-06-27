using UnityEngine;
using WizardWar.Tile;
using WizardWar.GameplayObjects;

namespace WizardWar 
{
    namespace Gate 
    {
        public class PlayerIsInGate : MonoBehaviour
        {
            private Gameplay _gameplay;

            private TileObjectsPositioningInTileMap _tileObjectsPositioningInTileMap; 
            
            private GameObject _player;

            private void Start()
            {
                _player = GameObject.FindWithTag("Player");

                _gameplay = GameObject.FindWithTag("Manager").GetComponent<Gameplay>();

                if (_gameplay != null) 
                {
                    _tileObjectsPositioningInTileMap = _gameplay.TileObjectsPositioningInTileMap;
                }                
            }

            void Update() //Agregar al segundo if la condicion de que la cantidad de librerias debe ser igual a cero
            {
                if (_player != null && _tileObjectsPositioningInTileMap != null)
                {
                    if (_tileObjectsPositioningInTileMap.GetTileObjectIndex(_player) == _tileObjectsPositioningInTileMap.SpecialTileIndex2)
                    {
                        if (Vector3.Distance(transform.position, _player.transform.position) <= _gameplay.LevelCreator.TileMapSize.y)
                        {
                            if (!_gameplay.OnLastLevel())
                            {
                                _gameplay.GoToNextLevel();                                
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