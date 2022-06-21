using UnityEngine;

public class Witcher_Controller : MonoBehaviour, IDestroyable
{      
    private WITCHER_TYPE witcherType;

    public WITCHER_TYPE WitcherType
    {        
        set
        {
            witcherType = value;
        }
    }

    private WITCHER_DIRECTION witcherDirection;  

    public WITCHER_DIRECTION WitcherDirection 
    {
        set 
        {
            witcherDirection = value;
        }
        get 
        {
            return witcherDirection;
        }
    }    

    private GameObject potionPrefab;

    public GameObject PotionPrefab 
    {
        set 
        {
            potionPrefab = value;
        }
        get 
        {
            return potionPrefab;
        }
    }

    private Vector2 initialPosIndex;

    public Vector2 InitialPosIndex 
    {
        set 
        {
            initialPosIndex = value;
        }
    }   

    private IMovable movementMechanic;

    private ICanUsePotion usePotionMechanic;         

    const short initialBombsCarried = 2;
    const float generateNewPotionTime = 3f;    

    private void Start()
    {
        SetWitcher(witcherType);

        TileMap.NewGameObjectInTile(initialPosIndex, gameObject);        

        transform.position = TileMap.GetTileMapPosition(initialPosIndex);

        transform.position = TileMap.GetGameObjectRightYPosition(gameObject);

        transform.rotation = Quaternion.identity;

        WitcherDirection = WITCHER_DIRECTION.DOWN;        
    }

    private void Update()
    {
        if (!movementMechanic.IsObjectMoving()) 
        {
            usePotionMechanic?.InstanciatePotion(potionPrefab, TileMap.GetGameObjectIndex(gameObject), witcherDirection);
        }        

        movementMechanic?.MoveInput();

        movementMechanic?.Timer();
    }

    private void FixedUpdate()
    {
        movementMechanic?.Move(gameObject, ref witcherDirection);
    }   

    void SetWitcher(WITCHER_TYPE witcherType) 
    {
        switch (witcherType) 
        {
            case WITCHER_TYPE.PLAYER:

                movementMechanic = new Player_Movement();
                usePotionMechanic = new PlayerInstanciatePotion();                

                break;
            case WITCHER_TYPE.CPU:

                Cpu_Movement auxCpuMovement = new Cpu_Movement(gameObject);
                movementMechanic = auxCpuMovement;
                usePotionMechanic = new CpuInstanciatePotion(auxCpuMovement);
                
                break;
        }
    }

    public void ObjectAboutToBeDestroyed()
    {
        if (witcherType == WITCHER_TYPE.PLAYER) 
        {
            ScenesManagement.ChangeToEndGameScene();
        }        
    }
}
