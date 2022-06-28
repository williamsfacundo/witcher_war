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

                private const float _explotionTime = 1.5f;

                private float _explotionTimer;

                private Index2 _explosionIndex;

                 Gameplay _gameplay;

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
                
                void Start()
                {
                    _explotionTimer = _explotionTime;
                }
                
                void Update()
                {
                    Explotion();

                    DecreaseTimer();
                }

                void Explotion()
                {
                    if (_explotionTimer <= 0f)
                    {
                        DestroyAdjacentObjectsInTileMap();

                        _gameplay?.TileObjectsInstanciator.TileObjectsPositioningInTileMap.DestroyGameObject(gameObject);
                    }
                }

                void DestroyAdjacentObjectsInTileMap() //Terminar la mecanica 
                {

                }

                void DecreaseTimer()
                {
                    _explotionTimer -= Time.deltaTime;
                }
            }
        }
    }
}