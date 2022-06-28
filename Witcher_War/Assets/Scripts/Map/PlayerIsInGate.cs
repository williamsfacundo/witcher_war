using UnityEngine;
using WizardWar.Tile;
using WizardWar.GameplayObjects;
using WizardWar.Scenes;
using WizardWar.TileObjects;
using WizardWar.Witcher.Movement;

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

                    PlayerMovement.PlayerMoveing += ActionWhenPlayerInGate;
                }               
            }            

            private void ActionWhenPlayerInGate() 
            {
                if (_player != null && _tileObjectsPositioningInTileMap != null)
                {
                    if (_tileObjectsPositioningInTileMap.GetTileObjectIndex(_player) == _tileObjectsPositioningInTileMap.SpecialTileIndex2
                        && Vector3.Distance(transform.position, _player.transform.position) <= _gameplay.LevelCreator.TileMapSize.y
                        && TileObjectsInstanciator.BookshelfsCount <= 0)
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

            private void OnDestroy()
            {
                PlayerMovement.PlayerMoveing -= ActionWhenPlayerInGate;
            }
        }
    }
}