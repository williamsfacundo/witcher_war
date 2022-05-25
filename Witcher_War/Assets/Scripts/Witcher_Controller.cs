using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Witcher_Controller : MonoBehaviour, IDestroyable
{
    [SerializeField] private WITCHER_TYPE witcherType = WITCHER_TYPE.CPU;

    [SerializeField] private float moveSpeed;

    [SerializeField] private GameObject potionPrefab;

    private ICanMove movementMethod;

    private ICanUsePotion usePotionMehod;

    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();        
    }

    private void Start()
    {
        SetMovement(witcherType);

        usePotionMehod = new Player_Instanciate_Potion();
    }

    private void Update()
    {
        usePotionMehod.InstanciatePotion(potionPrefab, transform.position);
    }

    private void FixedUpdate()
    {
        movementMethod?.Move(rigidBody, moveSpeed);
    }    

    private void OnDestroy()
    {
        ObjectDestroyed();
    }

    void SetMovement(WITCHER_TYPE witcherType) 
    {
        switch (witcherType) 
        {
            case WITCHER_TYPE.PLAYER:

                movementMethod = new Player_Movement();
                break;
            case WITCHER_TYPE.CPU:

                movementMethod = new CPU_Movement();
                break;
        }
    }

    public void ObjectDestroyed() 
    {
        Debug.Log(name + " was destoyed.");
    }
}
