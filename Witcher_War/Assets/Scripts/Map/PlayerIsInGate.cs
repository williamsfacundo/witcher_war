using System;
using UnityEngine;
using WizardWar.Tile;
using WizardWar.GameplayObjects;
using WizardWar.TileObjects;

namespace WizardWar 
{
    namespace Gate 
    {
        public class PlayerIsInGate : MonoBehaviour
        {
            private Gameplay _gameplay;

            private TileObjectsPositioningInTileMap _tileObjectsPositioningInTileMap; 
            
            private GameObject _player;

            public static Action playerWon;

            private void Start()
            {
                _player = GameObject.FindWithTag("Player");

                _gameplay = GameObject.FindWithTag("Manager").GetComponent<Gameplay>();

                if (_gameplay != null) 
                {
                    _tileObjectsPositioningInTileMap = _gameplay.TileObjectsPositioningInTileMap;                       
                }              
            }

            private void Update()
            {
                ActionWhenPlayerInGate();               
            }

            private void ActionWhenPlayerInGate() 
            {
                if (_player != null && _tileObjectsPositioningInTileMap != null && _gameplay != null)
                {
                    if (_tileObjectsPositioningInTileMap.GetTileObjectIndex(_player) == _tileObjectsPositioningInTileMap.SpecialTileIndex2
                        && Vector3.Distance(transform.position, _player.transform.position) <= _gameplay.LevelCreator.TileMapSize.y
                        && TileObjectsInstanciator.BookshelfsCount <= 0)
                    {
                        if (!_gameplay.OnLastLevel())
                        {
                            _gameplay.GoToNextLevel();
                        }
                        else   
                        {
                            playerWon();
                        }
                    }
                }
            }            
        }
    }
}