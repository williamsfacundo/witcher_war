using UnityEngine;

namespace WizardWar 
{
    namespace Tile 
    {
        public class LevelCreator
        {
            private TileMap _tileMap;
            
            private FloorBlocksInstanciator _floorBlocksInstanciator;

            private short _initialMapRows = 7;
            private short _initialMapColumns = 7;

            private Vector3 _tileMapCenter;
            private Vector3 _tileMapSize;

            private Vector3 _defaultMapSize = new Vector3(50, 4, 50); 

            public Vector3 TileMapSize 
            {
                get 
                {
                    return _tileMapSize;
                }
            }

            public TileMap TileMap 
            {
                get 
                {
                    return _tileMap;
                }
            }

            public LevelCreator(Vector3 tileMapCenter, Vector3 tileMapSize) 
            {
                _tileMapCenter = tileMapCenter;
                _tileMapSize = tileMapSize;

                CreateLevel();
            }
            
            public void CreateLevel() 
            {
                TileMapSizeNotZeroVerification();

                CreateTileMap();

                InstaciateFloorBlocks();
            }

            private void CreateTileMap() 
            {
                _tileMap = new TileMap(_initialMapRows, _initialMapColumns, _tileMapSize, _tileMapCenter);
            }

            private void InstaciateFloorBlocks() 
            {
                _floorBlocksInstanciator = new FloorBlocksInstanciator(_tileMap);
            }

            private void TileMapSizeNotZeroVerification() 
            {
                if (_tileMapSize == Vector3.zero) 
                {
                    _tileMapSize = _defaultMapSize;
                }
            }
        }
    }
}
