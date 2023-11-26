using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform Player;
    public float MoveSpeed = 1f;

    private Rigidbody2D rb;
    private Vector2 movement;
    //protected override void HandleInput()
    //{
    //    if (Target == null) 
    //    {
    //        Target = GameObject.FindWithTag("Player").transform;
    //    }

    //    if(Target == null) 
    //    {
    //        return;
    //    }

    //    _inputDirection = (Target.position - transform.position).normalized;
    //}

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Vector3 direction = Player.position - transform.position;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        MoveCharacter(movement);
    }
    void MoveCharacter(Vector2 direction) 
    { 
        rb.MovePosition((Vector2)transform.position + (direction * MoveSpeed * Time.deltaTime));
    }
}
