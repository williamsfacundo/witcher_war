using UnityEngine;
using WizardWar.Tile;
using WizardWar.Witcher;
using WizardWar.Enums;

namespace WizardWar 
{
    namespace TileObjects 
    {
        public class TileObjectsInstanciator
        {
            private string _bookshelfPrefabResourcesPath = "Gameplay/Objects/Bookshelf";
            private string _cauldronPrefabResourcesPath = "Gameplay/Objects/Cauldron";
            private string _witcherPrefabResourcesPath = "Gameplay/Objects/Witcher";
            private string _potionPrefabResourcesPath = "Gameplay/Objects/Potion";

            private const char bookshelfChar = 'W';
            private const char cauldronChar = 'X';
            private const char playerChar = 'P';
            private const char enemyChar = 'E';

            private const float potionSizePercentage = 0.35f;
            private const float bookshelfSizePercentage = 0.80f;
            private const float cauldronSizePercentage = 0.65f;            

            private char[] _map;

            private GameObject _potionPrefab;

            private LevelCreator _levelCreator;

            private TileObjectsPositioningInTileMap _tileObjectsPositioningInTileMap;

            private const short initialLevel = 1;

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
            
            public TileObjectsInstanciator(LevelCreator levelCreator, TileObjectsPositioningInTileMap tileObjectsPositioningInTileMap)
            {
                _levelCreator = levelCreator;

                _tileObjectsPositioningInTileMap = tileObjectsPositioningInTileMap;

                SetMapWithTextFileChars(initialLevel);              

                SetUpPotion();

                InstanciateObjects();                
            }         

            public void SetMapWithTextFileChars(short level) 
            {
                _map = MapReader.GetMapArrayChar(level, _levelCreator.TileMap.MaxRows);
            }

            public void InstanciateObjects()
            {
                for (short i = 0; i < _map.Length; i++)
                {
                    switch (_map[i])
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

            private void SetUpPotion()
            {
                _potionPrefab = (GameObject)Resources.Load(_potionPrefabResourcesPath);

                RescaleTool.RescaleGameObjectBasedOnPercentageSize(_potionPrefab, potionSizePercentage, LevelCreator.TileMap.TilesSize.x);
            }

            private void NewPlayer(short index) 
            {
                GameObject player = (GameObject)GameObject.Instantiate(Resources.Load(_witcherPrefabResourcesPath));

                WitcherController witcherController = player.GetComponent<WitcherController>();

                witcherController.WitcherType = WitcherType.Player;

                witcherController.PotionPrefab = _potionPrefab;

                Index2 arrayIndex2D = MapReader.CovertArrayIndexIntoArray2DIndex(index, _levelCreator.TileMap.MaxRows);

                _tileObjectsPositioningInTileMap.NewGameObjectInTile(arrayIndex2D, player);

                GameObjectPositioningCorrectly(player, arrayIndex2D);

                player.transform.tag = "Player";
            }

            private void NewCauldron(short index) 
            {
                GameObject cauldron = (GameObject)GameObject.Instantiate(Resources.Load(_cauldronPrefabResourcesPath));

                GameObjectRotateRandomly(cauldron);

                RescaleTool.RescaleGameObjectBasedOnPercentageSize(cauldron, cauldronSizePercentage, LevelCreator.TileMap.TilesSize.x);

                Index2 arrayIndex2D = MapReader.CovertArrayIndexIntoArray2DIndex(index, _levelCreator.TileMap.MaxRows);

                _tileObjectsPositioningInTileMap.NewGameObjectInTile(arrayIndex2D, cauldron);

                GameObjectPositioningCorrectly(cauldron, arrayIndex2D);
            }

            private void NewBookshelf(short index) 
            {
                GameObject boockshelf = (GameObject)GameObject.Instantiate(Resources.Load(_bookshelfPrefabResourcesPath));

                GameObjectRotateRandomly(boockshelf);

                RescaleTool.RescaleGameObjectBasedOnPercentageSize(boockshelf, bookshelfSizePercentage, LevelCreator.TileMap.TilesSize.x);

                Index2 arrayIndex2D = MapReader.CovertArrayIndexIntoArray2DIndex(index, _levelCreator.TileMap.MaxRows);

                _tileObjectsPositioningInTileMap.NewGameObjectInTile(arrayIndex2D, boockshelf);

                GameObjectPositioningCorrectly(boockshelf, arrayIndex2D);
            }

            private void NewEnemy(short index)
            {
                GameObject enemy = (GameObject)GameObject.Instantiate(Resources.Load(_witcherPrefabResourcesPath));

                WitcherController witcherController = enemy.GetComponent<WitcherController>();

                witcherController.WitcherType = WitcherType.Cpu;

                witcherController.PotionPrefab = _potionPrefab;

                Index2 arrayIndex2D = MapReader.CovertArrayIndexIntoArray2DIndex(index, _levelCreator.TileMap.MaxRows);

                _tileObjectsPositioningInTileMap.NewGameObjectInTile(arrayIndex2D, enemy);

                GameObjectPositioningCorrectly(enemy, arrayIndex2D);
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

            public void GameObjectPositioningCorrectly(GameObject gameObject, Index2 index) 
            {
                Vector3 halfTileMapYSize = Vector3.up * (LevelCreator.TileMap.TilesSize.y / 2f);
                Vector3 playerHalfSize = Vector3.up * (GetGameObjectHeight(gameObject) / 2f);
                Vector3 tileMapPosition = _tileObjectsPositioningInTileMap.GetTileMapPosition(index);

                gameObject.transform.position = tileMapPosition + halfTileMapYSize + playerHalfSize;
            }

            private float GetGameObjectHeight(GameObject gameObject) 
            {
                Renderer renderer = gameObject.GetComponent<Renderer>();

                if (renderer == null)
                {
                    GameObject child;

                    for (short i = 0; i < gameObject.transform.childCount; i++)
                    {
                        child = gameObject.transform.GetChild(i).gameObject;

                        renderer = child.GetComponent<Renderer>();

                        if (renderer != null)
                        {
                            return renderer.bounds.size.y;                            
                        }
                    }
                }
                else 
                {
                    return renderer.bounds.size.y;
                }

                return 0f;
            }
        }
    }    
}