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

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();        
    }

    private void Start()
    {
        SetWitcher(witcherType);

        Tile_Map.SetObjectTile(initialPosIndex, ref tile);        

        transform.position = Tile_Map.TileMap[(int)initialPosIndex.y, (int)initialPosIndex.x].Position;
    }

    private void Update()
    {        
        usePotionMechanic?.InstanciatePotion(potionPrefab, transform.position);
        movementMechanic.Timer();
    }

    private void FixedUpdate()
    {
        movementMechanic.Move(ref tile, rigidBody);
    }    

    private void OnDestroy()
    {
        objectHit();
    }

    void SetWitcher(WITCHER_TYPE witcherType) 
    {
        switch (witcherType) 
        {
            case WITCHER_TYPE.PLAYER:

                movementMechanic = new Player_Movement();
                usePotionMechanic = new Player_Instanciate_Potion();

                break;
            case WITCHER_TYPE.CPU:

                movementMechanic = new Cpu_Movement();
                usePotionMechanic = new Cpu_Instanciate_Potion();

                break;
        }
    }

    public void objectHit() 
    {
        //Debug.Log("Player hit.");
    }
}
