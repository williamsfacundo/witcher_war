using UnityEngine;

public class Player_Movement : ICanMove
{
    private Vector3 movement;
    
    public void Move(ref Vector2 indexPos) 
    {
        /*movement.x = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        movement.z = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        
        rb.MovePosition(rb.position + movement);*/       
    }

    public void Move() 
    {

    }
}
