using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{

    //public Animator animator;
    //public float Horizontal;
    //public float Vertical;
    //public float MoveLimiter = 0.7f;
    //protected override void HandleInput() 
    //{
    //    //Horizontal = Input.GetAxisRaw("Horizontal");
    //    //Vertical = Input.GetAxisRaw("Vertical");

    //    _inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    //}

    ////private void FixedUpdate()
    ////{
    ////    if (Horizontal != 0 && Vertical != 0)
    ////    {
    ////        Horizontal *= MoveLimiter;
    ////        Vertical *= MoveLimiter;
    ////    }
    ////    _rigidBody.velocity = new Vector2(Horizontal * Speed, Vertical * Speed);
    ////}
    ///

    public float MoveSpeed = 5;
    
    public Rigidbody2D RB;

    public Animator Animator;

    //Vector2 store value of X & Y
    Vector2 Movement;
    private void Update()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");

        //Prevent the double speed when moving diagonally
        Movement = Vector2.ClampMagnitude(Movement, 1);

        Animator.SetFloat("Speed", Movement.sqrMagnitude);
    }
    private void FixedUpdate()
    {
        RB.MovePosition(RB.position + Movement * MoveSpeed * Time.fixedDeltaTime);
    }
}
