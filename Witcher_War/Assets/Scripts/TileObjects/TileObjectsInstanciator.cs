using UnityEngine;
using WizardWar.Tile;

namespace WizardWar 
{
    namespace TileObjects 
    {
        public class TileObjectsInstanciator : MonoBehaviour
        {
            [SerializeField] private GameObject _bookshelfPrefab;
            [SerializeField] private GameObject _cauldronPrefab;
            [SerializeField] private GameObject _witcherPrefab;
            [SerializeField] private GameObject _potionPrefab;

            [SerializeField] [Range(1, _maxLevel)] private short _level;

            private const short _maxLevel = 5;

            private const char bookshelfChar = 'W';
            private const char cauldronChar = 'X';
            private const char playerChar = 'P';
            private const char enemyChar = 'E';

            private const float potionSizePercentage = 0.35f;
            private const float bookshelfSizePercentage = 0.80f;
            private const float cauldronSizePercentage = 0.65f;            

            private char[] _map;

            private LevelCreator _levelCreator;

            private TileObjectsPositioningInTileMap _tileObjectsPositioningInTileMap;

            private void Awake()
            {
                _levelCreator = GetComponent<LevelCreator>();                
            }

            // Start is called before the first frame update
            void Start()
            {
                SetMapWithTextFileChars();

                _tileObjectsPositioningInTileMap = new TileObjectsPositioningInTileMap(_levelCreator.TileMap);

                InstanciateObjects(_map);
            }

            public void NextLevel()
            {
                if (_level < _maxLevel)
                {
                    _level++;

                    _tileObjectsPositioningInTileMap.ClearTileMap();

                    SetMapWithTextFileChars();

                    InstanciateObjects(_map);
                }
            }

            private void SetMapWithTextFileChars() 
            {
                _map = MapReader.GetMapArrayChar(_level, _levelCreator.TileMap.MaxRows);
            }

            private void InstanciateObjects(char[] map)
            {
                for (short i = 0; i < map.Length; i++)
                {
                    switch (map[i])
                    {
                        case bookshelfChar:

                            NewBookshelf(i);

                            break;
                        case cauldronChar:

                            NewCauldron(i);

                            break;
                        case playerChar:

                            NewPlayer(i);

                            break;
                        case enemyChar:

                            NewEnemy(i);

                            break;
                        default:
                            break;
                    }
                }
            }

            private void NewPlayer(short index)
            {
                if (_witcherPrefab != null) 
                {
                    GameObject player = Instantiate(_witcherPrefab);

                    Witcher_Controller witcher_Controller = player.GetComponent<Witcher_Controller>();

                    if (witcher_Controller != null) 
                    {
                        witcher_Controller.WitcherType = WITCHER_TYPE.PLAYER;

                        witcher_Controller.PotionPrefab = _potionPrefab;

                        RescaleTool.RescaleGameObjectBasedOnPercentageSize(witcher_Controller.PotionPrefab, potionSizePercentage, _levelCreator.TileMap.TilesSize.x);

                        _tileObjectsPositioningInTileMap.NewGameObjectInTile(MapReader.CovertArrayIndexIntoArray2DIndex(index, _levelCreator.TileMap.MaxRows), player);

                        player.transform.tag = "Player";
                    }
                    else 
                    {
                        Debug.LogError("No witcher controller in game object.");
                    }                   
                }
                else 
                {
                    Debug.LogError("No witcher prefab.");
                }                
            }

            private void NewCauldron(short index)
            {
                if (_cauldronPrefab != null)
                {
                    GameObject cauldron = Instantiate(_cauldronPrefab);

                    GameObjectRotateRandomly(cauldron);

                    TileMap.RescaleGameObjectDependingTileSize(cauldron, cauldronSizePercentage, TileMap.TileSize.x);

                    _tileObjectsPositioningInTileMap.NewGameObjectInTile(MapReader.CovertArrayIndexIntoArray2DIndex(index, _levelCreator.TileMap.MaxRows), cauldron);
                }
                else 
                {
                    Debug.LogError("No cauldron prefab.");
                }                
            }

            private void NewBookshelf(short index)
            {
                if (_bookshelfPrefab != null) 
                {
                    GameObject boockshelf = Instantiate(_bookshelfPrefab);

                    GameObjectRotateRandomly(boockshelf);

                    TileMap.RescaleGameObjectDependingTileSize(boockshelf, bookshelfSizePercentage, TileMap.TileSize.x);

                    _tileObjectsPositioningInTileMap.NewGameObjectInTile(MapReader.CovertArrayIndexIntoArray2DIndex(index, _levelCreator.TileMap.MaxRows), boockshelf);
                }
                else 
                {
                    Debug.LogError("No bookshelf prefab.");
                }
            }

            private void NewEnemy(short index)
            {
                if (_witcherPrefab != null) 
                {
                    GameObject enemy = Instantiate(_witcherPrefab);

                    Witcher_Controller witcher_Controller = enemy.GetComponent<Witcher_Controller>();

                    if (witcher_Controller != null) 
                    {
                        witcher_Controller.WitcherType = WITCHER_TYPE.CPU;

                        witcher_Controller.PotionPrefab = _potionPrefab;

                        TileMap.RescaleGameObjectDependingTileSize(witcher_Controller.PotionPrefab, potionSizePercentage, TileMap.TileSize.x);

                        _tileObjectsPositioningInTileMap.NewGameObjectInTile(MapReader.CovertArrayIndexIntoArray2DIndex(index, _levelCreator.TileMap.MaxRows), enemy);
                    }
                    else 
                    {                        
                        Debug.LogError("No witcher controller in game object.");
                    }                    
                }
                else 
                {
                    Debug.LogError("No witcher prefab.");
                }                
            }

            private void GameObjectRotateRandomly(GameObject gameObject)
            {
                switch ((short)Random.Range(1f, 4.9f))
                {
                    case 1:
                        gameObject.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                        break;
                    case 2:
                        gameObject.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                        break;
                    case 3:
                        gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                        break;
                    default:
                        break;
                }
            }
        }
    }    
}