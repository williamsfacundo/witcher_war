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

        Tile_Map.NewGameObjectInTile(initialPosIndex, gameObject);        

        transform.position = Tile_Map.GetTileMapPosition(initialPosIndex);

        transform.rotation = Quaternion.identity;

        WitcherDirection = WITCHER_DIRECTION.DOWN;        
    }

    private void Update()
    {
        if (!movementMechanic.IsObjectMoving()) 
        {
            usePotionMechanic?.InstanciatePotion(potionPrefab, transform.position);
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
                usePotionMechanic = new Player_Instanciate_Potion(initialBombsCarried, generateNewPotionTime);                

                break;
            case WITCHER_TYPE.CPU:

                movementMechanic = new Cpu_Movement();
                usePotionMechanic = new Cpu_Instanciate_Potion();
                
                break;
        }
    }

    public void ObjectAboutToBeDestroyed()
    {
        //Tile_Map.DestroyGameObjectInTileX(Tile_Map.GetGameObjectDownIndex(gameObject));
    }
}
