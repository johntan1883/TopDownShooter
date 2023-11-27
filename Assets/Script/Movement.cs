using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Movement : MonoBehaviour
{
    public float Speed = 1f;
    protected const float m_MovementSmothing = 0.05f;

    protected Vector2 _inputDirection;
    protected Vector2 m_Velocity = Vector2.zero;
    protected bool _isMoving = false;

    protected Rigidbody2D _rigidBody;
    protected Collider2D _collider2D;
    protected Vector2 _targetVelocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();
    }

    protected virtual void HandleRotation() 
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _targetVelocity);
    }
    protected virtual void HandleInput()
    {

    }
    protected virtual void HandleMovement()
    {
        if (_rigidBody == null || _collider2D == null)
            return;

        Vector2 targetVelocity = Vector2.zero;
        targetVelocity = new Vector2(_inputDirection.x * Speed, _inputDirection.y * Speed);

        _rigidBody.velocity = 
            Vector2.SmoothDamp(_rigidBody.velocity, targetVelocity,
            ref m_Velocity, m_MovementSmothing);

        transform.rotation = Quaternion.LookRotation(Vector3.forward,targetVelocity);

        _targetVelocity = targetVelocity;
    }

}
