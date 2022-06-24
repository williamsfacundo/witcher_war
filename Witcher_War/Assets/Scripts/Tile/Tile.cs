using UnityEngine;

namespace WizardWar 
{
    namespace Tile
    {
        public class Tile
        {
            private GameObject _tileObject; 
            
            private Vector3 _tilePosition;

            public GameObject TileObject
            {
                set
                {
                    _tileObject = value;
                }

                get
                {
                    return _tileObject;
                }
            }      

            public Vector3 TilePosition
            {
                set 
                {
                    _tilePosition = value;
                }

                get 
                {
                    return _tilePosition;
                }
            }

            public bool IsEmpty
            {
                get
                {
                    return _tileObject == null;
                }
            }

            public Tile()
            {
                _tileObject = null;                
                _tilePosition = Vector3.zero;
            }            
        }
    }    
}
