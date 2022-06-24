using UnityEngine;

namespace WizardWar 
{
    namespace Tile 
    {
        public class LevelCreator : MonoBehaviour
        {
            [SerializeField] private short _initialMapRows = 7;
            [SerializeField] private short _initialMapColumns = 7;

            [SerializeField] private Vector3 _tileMapCenter;
            [SerializeField] private Vector3 _tileMapSize;

            private TileMap _tileMap;
            
            private FloorBlocksInstanciator _floorBlocksInstanciator;            

            public TileMap TileMap 
            {
                get 
                {
                    return _tileMap;
                }
            }
           
            void Awake()
            {
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
        }
    }
}
