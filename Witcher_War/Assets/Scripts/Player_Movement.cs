using UnityEngine;

public class Player_Movement : ICanMove
{
    Vector3 movement;
    
    private const float speed = 10;

    public void Move(Rigidbody rb) 
    {
        movement.x = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        movement.z = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        rb.AddForce(movement, ForceMode.Force);
    }
}
