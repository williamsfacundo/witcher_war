using UnityEngine;

public class Witcher_Controller : MonoBehaviour
{
    [SerializeField] WITCHER_TYPE witcherType = WITCHER_TYPE.CPU;

    private ICanMove movement;
    
    private void Start()
    {
        SetMovement(witcherType);                
    }

    private void Update()
    {
        movement.Move();
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
