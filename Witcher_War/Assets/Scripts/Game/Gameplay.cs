using UnityEngine;
using WizardWar.Tile;
using WizardWar.TileObjects;
using WizardWar.Gate;

namespace WizardWar 
{
    namespace GameplayObjects 
    {    
        [RequireComponent(typeof(GateInstanciator))]
        public class Gameplay : MonoBehaviour
        {            
            private const short _maxLevel = 5;

            [SerializeField] [Range(1, _maxLevel)] private short _level;

            [SerializeField] private GateInstanciator _gateInstanciator;

            [SerializeField] private Vector3 _tileMapSize;
            [SerializeField] private Vector3 _tileMapCenter;            

            private LevelCreator _levelCreator;
            private TileObjectsInstanciator _tileObjectsInstanciator;
            private TileObjectsPositioningInTileMap _tileObjectsPositioningInTileMap;

            public LevelCreator LevelCreator 
            {
                get 
                {
                    return _levelCreator;
                }
            }

            public TileObjectsInstanciator TileObjectsInstanciator 
            {
                get 
                {
                    return _tileObjectsInstanciator;
                }
            }

            public TileObjectsPositioningInTileMap TileObjectsPositioningInTileMap 
            {
                get 
                {
                    return _tileObjectsPositioningInTileMap;
                }
            }

            private void Awake()
            {
                _levelCreator = new LevelCreator(_tileMapCenter, _tileMapSize);

                _tileObjectsPositioningInTileMap = new TileObjectsPositioningInTileMap(_levelCreator.TileMap);

                _tileObjectsInstanciator = new TileObjectsInstanciator(_levelCreator, _tileObjectsPositioningInTileMap);               
            }            

            public void GoToNextLevel()
            {
                if (_level < _maxLevel)
                {
                    _level++;

                    ResetTileObjectsGameplay();                    
                }
            }

            public void GoToLevelOne()
            {
                _level = 1;

                ResetTileObjectsGameplay();
            }            

            public bool OnLastLevel()
            {
                return _level == _maxLevel;
            }

            private void ResetTileObjectsGameplay() 
            {
                _tileObjectsPositioningInTileMap.ClearTileMap();

                _gateInstanciator.DestroyGate();

                _tileObjectsInstanciator.SetMapWithTextFileChars(_level);

                _tileObjectsInstanciator.InstanciateObjects();
            }
        }
    }
}