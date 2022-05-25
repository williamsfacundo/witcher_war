using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Witcher_Controller : MonoBehaviour, IDestroyable
{
    [SerializeField] private WITCHER_TYPE witcherType = WITCHER_TYPE.CPU;

    [SerializeField] private float moveSpeed;
    
    private ICanMove movement;
    
    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();        
    }

    private void Start()
    {
        SetMovement(witcherType);                
    }

    private void FixedUpdate()
    {
        movement?.Move(rigidBody, moveSpeed);
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

                movement = new Player_Movement();
                break;
            case WITCHER_TYPE.CPU:

                movement = new CPU_Movement();
                break;
        }
    }

    public void ObjectDestroyed() 
    {
        Debug.Log(name + " was destoyed.");
    }
}
