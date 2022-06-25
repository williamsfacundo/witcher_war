using UnityEngine;

namespace WizardWar 
{
    namespace Tile 
    {
        public class TileMap
        {
            private short _maxRows;
            private short _maxColumns;

            private Vector3 _tilesSize;

            private Tile[,] _tilesArray2D;            

            public short MaxRows 
            {
                get 
                {
                    return _maxRows;
                }
            }

            public short MaxColumns 
            {
                get 
                {
                    return _maxColumns;
                }
            }

            public Vector3 TilesSize
            {
                get
                {
                    return _tilesSize;
                }
            }

            public Tile[,] TileArray2D
            {
                get
                {
                    return _tilesArray2D;
                }
            }

            public TileMap(short maxRows, short maxColumns, Vector3 surfaceSize, Vector3 surfaceCenter) 
            {
                _maxRows = maxRows;
                _maxColumns = maxColumns;

                NewDefaultTileArray2D();

                CalculateTilesSize(surfaceSize, surfaceCenter);

                SetTilesArray2DPositions(surfaceSize ,surfaceCenter);
            }

            public bool IsIndexValid(Index2 index)
            {
                return (index.X >= 0 && index.X < _maxColumns) && (index.Y >= 0 && index.Y < _maxRows);
            }

            public bool IsTileEmpty(Index2 tileIndex) 
            {
                return _tilesArray2D[tileIndex.Y, tileIndex.X].IsEmpty;
            }

            private void NewDefaultTileArray2D() 
            {
                _tilesArray2D = new Tile[_maxRows, _maxColumns];

                for (short i = 0; i < _maxRows; i++)
                {
                    for (short v = 0; v < _maxColumns; v++)
                    {
                        _tilesArray2D[i, v] = new Tile();
                    }
                }
            }

            private void CalculateTilesSize(Vector3 surfaceSize, Vector3 surfaceCenter)
            {
                _tilesSize = new Vector3(surfaceSize.x / _maxColumns, surfaceCenter.y + surfaceSize.y, surfaceSize.z / _maxRows);                
            }

            private void SetTilesArray2DPositions(Vector3 surfaceSize, Vector3 surfaceCenter) 
            {                
                Vector3 firstTilePosition = GetFirstTilePosition(surfaceSize, surfaceCenter);

                for (short i = 0; i < _maxRows; i++)
                {
                    for (short v = 0; v < _maxColumns; v++)
                    {
                        _tilesArray2D[i, v].TilePosition = firstTilePosition + new Vector3(_tilesSize.x * v, 0f, -_tilesSize.z * i);
                    }
                }
            }

            private Vector3 GetFirstTilePosition(Vector3 mapSize, Vector3 mapCenter)
            {
                Vector3 firstMapPosition = mapCenter;

                firstMapPosition.y += mapSize.y / 2;

                firstMapPosition.x -= mapSize.x / 2f;
                firstMapPosition.z += mapSize.z / 2f;

                firstMapPosition.x += _tilesSize.x / 2f;
                firstMapPosition.z -= _tilesSize.z / 2f;

                return firstMapPosition;
            }                      
        }
    }
}