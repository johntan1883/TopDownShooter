using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    public float MoveSpeed = 5;
    public Rigidbody2D RB;
    public Animator Animator;
    public Camera Cam;

    //Vector2 store value of X & Y
    Vector2 Movement;
    Vector2 MousePos;
    private void Update()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");

        //MousePos = Cam.ScreenToWorldPoint(Input.mousePosition); 

        //Prevent the double speed when moving diagonally
        Movement = Vector2.ClampMagnitude(Movement, 1);

        Animator.SetFloat("Speed", Movement.sqrMagnitude);
    }
    private void FixedUpdate()
    {
        RB.MovePosition(RB.position + Movement * MoveSpeed * Time.fixedDeltaTime);
    }
}
