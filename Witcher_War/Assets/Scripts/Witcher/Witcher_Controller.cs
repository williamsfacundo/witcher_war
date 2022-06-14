using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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

    private ICanMove movementMechanic;

    private ICanUsePotion usePotionMechanic; 

    private Rigidbody rigidBody;
    
    private Tile tile;

    public Tile Tile 
    {
        set 
        {
            tile = value;
        }
        get 
        {
            return Tile;
        }
    }

    const short initialBombsCarried = 2;
    const float generateNewPotionTime = 3f;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();        
    }

    private void Start()
    {
        SetWitcher(witcherType);

        Tile_Map.SetObjectTile(initialPosIndex, ref tile);        

        transform.position = Tile_Map.TileMap[(int)initialPosIndex.y, (int)initialPosIndex.x].Position;

        transform.rotation = Quaternion.identity;

        WitcherDirection = WITCHER_DIRECTION.DOWN;
    }

    private void Update()
    {        
        usePotionMechanic?.InstanciatePotion(potionPrefab, transform.position);
        movementMechanic.Timer();
    }

    private void FixedUpdate()
    {
        movementMechanic.Move(ref tile, rigidBody, ref witcherDirection);
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

    public void ObjectHit()
    {
        tile.IsEmpty = true;

        Destroy(gameObject);
    }
}
