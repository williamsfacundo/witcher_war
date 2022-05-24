using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Witcher_Controller : MonoBehaviour
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
        if(movement != null)
            movement.Move(rigidBody, moveSpeed);
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
}
