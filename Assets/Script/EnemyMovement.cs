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

    bool isDead = false;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Player == null)
        {
            GameObject player = GameObject.FindWithTag("Player");

            Player = player.transform;
        }

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
        if (isDead) return;
        rb.MovePosition((Vector2)transform.position + (direction * MoveSpeed * Time.deltaTime));
    }

    public void StopMoving()
    {
        isDead = true;
    }
}
