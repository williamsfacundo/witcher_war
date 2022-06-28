using UnityEngine;
using WizardWar.Tile;
using WizardWar.GameplayObjects;

namespace WizardWar 
{
    namespace Witcher 
    {
        namespace Potion 
        {
            public class PotionExplotion : MonoBehaviour
            {
                private PotionAnimationController _potionAnimationController;

                private Gameplay _gameplay;

                private const float _explotionTime = 1.5f;

                private float _explotionTimer;

                private Index2 _explosionIndex;               

                public Index2 ExplosionIndex
                {
                    set
                    {
                        _explosionIndex = value;
                    }
                }

                private void Awake()
                {
                    _potionAnimationController = GetComponent<PotionAnimationController>();

                    _gameplay = GameObject.FindWithTag("Manager").GetComponent<Gameplay>();
                }
                
                private void Start()
                {
                    _explotionTimer = _explotionTime;
                }
                
                private void Update()
                {
                    Explotion();

                    DecreaseTimer();
                }

                private void Explotion()
                {
                    if (_explotionTimer <= 0f)
                    {
                        DestroyAdjacentObjectsInTileMap();

                        _gameplay?.TileObjectsPositioningInTileMap.DestroyGameObject(gameObject);
                    }
                }

                private void DestroyAdjacentObjectsInTileMap() //Terminar la mecanica 
                {
                    DestroyObjectInGameObjectPlusIndex(Index2.Up);

                    DestroyObjectInGameObjectPlusIndex(-Index2.Up);

                    DestroyObjectInGameObjectPlusIndex(Index2.Right);
                    
                    DestroyObjectInGameObjectPlusIndex(-Index2.Right);
                }

                private void DestroyObjectInGameObjectPlusIndex(Index2 additionIndex) 
                {
                    if (_explosionIndex != Index2.IndexNull) 
                    {
                        Index2 _explosionIndexAux = _explosionIndex;

                        _explosionIndexAux = _gameplay.TileObjectsPositioningInTileMap.GetTileObjectIndexPlusOtherIndex(gameObject, additionIndex);
                        
                        _gameplay.TileObjectsPositioningInTileMap.DestroyGameObject(_explosionIndexAux, true);
                    }                    
                }

                private void DecreaseTimer()
                {
                    _explotionTimer -= Time.deltaTime;
                }
            }
        }
    }
}