using UnityEngine;

namespace WizardWar 
{
    namespace Tile 
    {
        public class TileObjectsPositioningInTileMap
        {
            private TileMap _tileMap;

            private Tile _specialTile; //Index of special tile object that will be unique (only 1) and other game objects can go through it  

            public TileObjectsPositioningInTileMap(TileMap tileMap) 
            {
                _tileMap = tileMap;

                _specialTile = new Tile();               
            }
            
            ~TileObjectsPositioningInTileMap() 
            {
                ClearTileMap();
            }
            
            public void SetSpecialTile(GameObject specialTile, Index2 specialTileIndex) 
            {
                if (_tileMap != null && specialTile != null) 
                {
                    if (_specialTile.TileObject == null) 
                    {
                        _specialTile.TileObject = specialTile;

                        _specialTile.TilePosition = GetTileMapPosition(specialTileIndex);
                    }
                }
            }

            public void NewGameObjectInTile(Index2 tileIndex, GameObject tileObject)
            {
                if (_tileMap != null && tileObject != null)
                {
                    if (!IsGameObjectInTileMap(tileObject) && IsGameObjectTilable(tileObject))
                    {
                        LocateGameObjectInTile(tileIndex, tileObject);
                    }
                }
            }

            public void MoveGameObjectToTileX(Index2 destinyIndex, GameObject tileObject)
            {
                if (_tileMap != null && tileObject != null)
                {
                    Index2 auxIndex = GetTileObjectIndex(tileObject);

                    if (IsGameObjectInTileMap(tileObject) && auxIndex != destinyIndex)
                    {
                        LocateGameObjectInTile(destinyIndex, auxIndex, tileObject);
                    }
                }
            }

            public void DestroyGameObject(GameObject tileObject)
            {
                if (_tileMap != null)
                {
                    if (IsGameObjectInTileMap(tileObject)) 
                    {
                        Index2 auxIndex2 = GetTileObjectIndex(tileObject);

                        IDestroyable aux = tileObject.GetComponent<IDestroyable>();

                        if (aux != null)
                        {
                            aux.ObjectAboutToBeDestroyed();                           
                        }

                        GameObject.Destroy(tileObject);
                    }                   
                }
            }            

            public void ClearTileMap()
            {
                if (_tileMap != null)
                {
                    for (short i = 0; i < _tileMap.MaxRows; i++)
                    {
                        for (short v = 0; v < _tileMap.MaxRows; v++)
                        {
                            if (_tileMap.TileArray2D[i, v].IsEmpty)
                            {
                                GameObject.Destroy(_tileMap.TileArray2D[i, v].TileObject);

                                _tileMap.TileArray2D[i, v].TileObject = null;
                            }
                        }
                    }

                    DestroySpecialTile();
                }
            }

            public Index2 GetTileObjectIndexPlusOtherIndex(GameObject tileObject, Index2 otherIndex)
            {
                if (_tileMap != null)
                {
                    Index2 index = GetTileObjectIndex(tileObject);

                    index += otherIndex;

                    if (_tileMap.IsIndexValid(index))
                    {
                        index = Index2.IndexNull;
                    }

                    return index;
                }

                return Index2.IndexNull;
            }
            private void DestroySpecialTile()
            {
                if (_specialTile != null)
                {
                    if (_specialTile.TileObject != null)
                    {
                        GameObject.Destroy(_specialTile.TileObject);

                        _specialTile.TileObject = null;
                    }
                }
            }

            private Index2 GetTileObjectIndex(GameObject tileObject)
            {
                for (short i = 0; i < _tileMap.MaxRows; i++)
                {
                    for (short v = 0; v < _tileMap.MaxColumns; v++)
                    {
                        if (_tileMap.TileArray2D[i, v].TileObject == tileObject)
                        {
                            return new Index2(v, i);
                        }
                    }
                }

                return Index2.IndexNull;
            }

            private bool IsGameObjectTilable(GameObject tileObject) 
            {
                ITileleable auxITileleable = tileObject.GetComponent<ITileleable>();

                if (auxITileleable != null) 
                {
                    return true;
                }

                return false;
            }

            private bool IsGameObjectInTileMap(GameObject tileObject)
            {
                if (_tileMap != null) 
                {
                    for (short i = 0; i < _tileMap.MaxRows; i++)
                    {
                        for (short v = 0; v < _tileMap.MaxColumns; v++)
                        {
                            if (_tileMap.TileArray2D[i, v].TileObject == tileObject)
                            {
                                return true;
                            }
                        }
                    }
                }                

                return false;
            }

            private void LocateGameObjectInTile(Index2 newIndex, GameObject tileObject)
            {
                if (_tileMap.IsIndexValid(newIndex))
                {
                    if (_tileMap.TileArray2D[newIndex.Y, newIndex.X].IsEmpty)
                    {
                        _tileMap.TileArray2D[newIndex.Y, newIndex.X].TileObject = tileObject;                        
                    }
                }
            }

            private void LocateGameObjectInTile(Index2 newIndex, Index2 oldIndex, GameObject tileObject)
            {
                if (_tileMap.IsIndexValid(newIndex))
                {
                    if (_tileMap.TileArray2D[newIndex.Y, newIndex.X].IsEmpty)
                    {
                        _tileMap.TileArray2D[oldIndex.Y, oldIndex.X].TileObject = null;
                        _tileMap.TileArray2D[newIndex.Y, newIndex.X].TileObject = tileObject;
                    }
                }
            }

            private Vector3 GetTileMapPosition(Index2 targetIndex)
            {
                return _tileMap.TileArray2D[targetIndex.Y, targetIndex.X].TilePosition;
            }
        }
    }
}