using UnityEngine;
using WizardWar.Tile;
using WizardWar.Witcher;


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

            public LevelCreator LevelCreator 
            {
                get 
                {
                    return _levelCreator;
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
                _levelCreator = GetComponent<LevelCreator>();                
            }
            
            void Start()
            {
                SetMapWithTextFileChars();

                _tileObjectsPositioningInTileMap = new TileObjectsPositioningInTileMap(_levelCreator.TileMap);

                InstanciateObjects(_map);

                PrefabPotionRescale();
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

            public void ResetTileObjects() 
            {
                _level = 1;

                _tileObjectsPositioningInTileMap.ClearTileMap();

                SetMapWithTextFileChars();

                InstanciateObjects(_map);
            }

            public bool OnLastLevel() 
            {
                return _level == _maxLevel;
            }

            private void PrefabPotionRescale() 
            {
                RescaleTool.RescaleGameObjectBasedOnPercentageSize(_potionPrefab, potionSizePercentage, LevelCreator.TileMap.TilesSize.x);
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

            private void NewPlayer(short index) //Guardar los tags en un archivo + Posicionar objeto correctamente 
            {
                Debug.Log(LevelCreator.TileMapSize.x);

                if (_witcherPrefab != null) 
                {
                    GameObject player = Instantiate(_witcherPrefab);

                    WitcherController witcherController = player.GetComponent<WitcherController>();

                    if (witcherController != null) 
                    {
                        witcherController.WitcherType = WitcherType.Player;

                        witcherController.PotionPrefab = _potionPrefab;                        

                        Index2 arrayIndex2D = MapReader.CovertArrayIndexIntoArray2DIndex(index, _levelCreator.TileMap.MaxRows);

                        _tileObjectsPositioningInTileMap.NewGameObjectInTile(arrayIndex2D, player);

                        player.transform.position = _tileObjectsPositioningInTileMap.GetTileMapPosition(arrayIndex2D);

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

            private void NewCauldron(short index) //Posicionar objeto correctamente
            {
                if (_cauldronPrefab != null)
                {
                    GameObject cauldron = Instantiate(_cauldronPrefab);

                    GameObjectRotateRandomly(cauldron);

                    RescaleTool.RescaleGameObjectBasedOnPercentageSize(cauldron, cauldronSizePercentage, LevelCreator.TileMap.TilesSize.x);

                    Index2 arrayIndex2D = MapReader.CovertArrayIndexIntoArray2DIndex(index, _levelCreator.TileMap.MaxRows);

                    _tileObjectsPositioningInTileMap.NewGameObjectInTile(arrayIndex2D, cauldron);

                    cauldron.transform.position = _tileObjectsPositioningInTileMap.GetTileMapPosition(arrayIndex2D);
                }
                else 
                {
                    Debug.LogError("No cauldron prefab.");
                }                
            }

            private void NewBookshelf(short index) //Posicionar objeto correctamente
            {
                if (_bookshelfPrefab != null) 
                {
                    GameObject boockshelf = Instantiate(_bookshelfPrefab);

                    GameObjectRotateRandomly(boockshelf);
                    
                    RescaleTool.RescaleGameObjectBasedOnPercentageSize(boockshelf, bookshelfSizePercentage, LevelCreator.TileMap.TilesSize.x);

                    Index2 arrayIndex2D = MapReader.CovertArrayIndexIntoArray2DIndex(index, _levelCreator.TileMap.MaxRows);

                    _tileObjectsPositioningInTileMap.NewGameObjectInTile(arrayIndex2D, boockshelf);

                    boockshelf.transform.position = _tileObjectsPositioningInTileMap.GetTileMapPosition(arrayIndex2D);
                }
                else 
                {
                    Debug.LogError("No bookshelf prefab.");
                }
            }

            private void NewEnemy(short index) //Posicionar objeto correctamente
            {
                if (_witcherPrefab != null) 
                {
                    GameObject enemy = Instantiate(_witcherPrefab);

                    WitcherController witcherController = enemy.GetComponent<WitcherController>();

                    if (witcherController != null) 
                    {
                        witcherController.WitcherType = WitcherType.Cpu;

                        witcherController.PotionPrefab = _potionPrefab;                        

                        Index2 arrayIndex2D = MapReader.CovertArrayIndexIntoArray2DIndex(index, _levelCreator.TileMap.MaxRows);

                        _tileObjectsPositioningInTileMap.NewGameObjectInTile(arrayIndex2D, enemy);

                        witcherController.transform.position = _tileObjectsPositioningInTileMap.GetTileMapPosition(arrayIndex2D);
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