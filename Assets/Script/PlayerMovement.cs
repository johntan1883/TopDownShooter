using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    public float Horizontal;
    public float Vertical;
    public float MoveLimiter = 0.7f;
    protected override void HandleInput() 
    {
        //Horizontal = Input.GetAxisRaw("Horizontal");
        //Vertical = Input.GetAxisRaw("Vertical");

        _inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    //private void FixedUpdate()
    //{
    //    if (Horizontal != 0 && Vertical != 0)
    //    {
    //        Horizontal *= MoveLimiter;
    //        Vertical *= MoveLimiter;
    //    }
    //    _rigidBody.velocity = new Vector2(Horizontal * Speed, Vertical * Speed);
    //}
}
