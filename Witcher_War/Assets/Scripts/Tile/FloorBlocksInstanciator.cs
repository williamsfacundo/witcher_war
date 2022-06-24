using UnityEngine;

namespace WizardWar 
{
    namespace Tile 
    {
        public class FloorBlocksInstanciator 
        {
            private const string _floorBlockOneResourceName = "Floor/Floor_Block_One";
            private const string _floorBlockTwoResourceName = "Floor/Floor_Block_Two";

            private GameObject[,] _floorBlocks;

            private short _maxRows;
            private short _maxColumns;

            public FloorBlocksInstanciator(TileMap tileMap) 
            {
                _maxRows = tileMap.MaxRows;
                _maxColumns = tileMap.MaxColumns;                

                CreateBlocks();

                InstanciateFloorBlocks(tileMap.TileArray2D);

                SetFloorBlocksSize(tileMap.TilesSize);
            }

            private void CreateBlocks() 
            {
                _floorBlocks = new GameObject[_maxRows, _maxColumns];
            }

            private void InstanciateFloorBlocks(Tile[,] tileMap)
            {
                bool instanciateFloorBlockOne = true;

                for (short i = 0; i < _maxRows; i++)
                {
                    for (short v = 0; v < _maxColumns; v++)
                    {
                        if (instanciateFloorBlockOne)
                        {
                             _floorBlocks[i, v] = (GameObject)GameObject.Instantiate(Resources.Load(_floorBlockOneResourceName), tileMap[i, v].TilePosition, Quaternion.identity);
                        }
                        else
                        {
                            _floorBlocks[i, v] = (GameObject)GameObject.Instantiate(Resources.Load(_floorBlockTwoResourceName), tileMap[i, v].TilePosition, Quaternion.identity);
                        }

                        instanciateFloorBlockOne = !instanciateFloorBlockOne;
                    }
                }
            }

            private void SetFloorBlocksSize(Vector3 tilesSize)
            {
                for (short i = 0; i < _maxRows; i++) 
                {
                    for (short v = 0; v < _maxColumns; v++) 
                    {
                        RescaleTool.RescaleGameObject(_floorBlocks[i, v], tilesSize);
                    }
                }                
            }
        }
    }
}