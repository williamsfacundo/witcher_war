using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Witcher_Controller : MonoBehaviour, IDestroyable
{
    [SerializeField] private WITCHER_TYPE witcherType = WITCHER_TYPE.CPU;

    [SerializeField] private float moveSpeed;

    [SerializeField] private GameObject potionPrefab;

    [SerializeField] [Range(0, Tile_Map.maxColumns)] private int initialXPosIndex;
    [SerializeField] [Range(0, Tile_Map.maxRows)] private int initialYPosIndex;

    private ICanMove movementMechanic;

    private ICanUsePotion usePotionMechanic;

    private Rigidbody rigidBody;

    private Vector2 posIndex;

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

        Tile_Map.SetObjectTile(new Vector2(initialXPosIndex, initialYPosIndex), ref tile);        

        transform.position = Tile_Map.TileMap[initialYPosIndex, initialXPosIndex].Position;
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
        Debug.Log("Player hit.");
    }
}
