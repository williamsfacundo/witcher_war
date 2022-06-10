using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Witcher_Controller : MonoBehaviour, IDestroyable
{
    [SerializeField] private WITCHER_TYPE witcherType = WITCHER_TYPE.CPU;

    [SerializeField] private float moveSpeed;

    [SerializeField] private GameObject potionPrefab;

    [SerializeField] [Range(0, Tile_Map_Generator.maxColumns)] private int initialXPosIndex;
    [SerializeField] [Range(0, Tile_Map_Generator.maxRows)] private int initialYPosIndex;

    private ICanMove movementMechanic;

    private ICanUsePotion usePotionMechanic;

    private Rigidbody rigidBody;

    Vector2 posIndex;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();        
    }

    private void Start()
    {
        SetWitcher(witcherType);

        //transform.position = Tile_Map_Generator.TileMap[initialYPosIndex, initialXPosIndex];
    }

    private void Update()
    {
        usePotionMechanic?.InstanciatePotion(potionPrefab, transform.position);
    }

    private void FixedUpdate()
    {
        movementMechanic?.Move(ref posIndex);
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
