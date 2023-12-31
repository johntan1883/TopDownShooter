using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 100f; //Bullet Speed
    public Cooldown NewLifeTime;

    private Rigidbody2D _rigidbody;
    private DamageOnTouch _damageOnTouch;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.AddRelativeForce(new Vector2(0f, Speed));

        _damageOnTouch = GetComponent<DamageOnTouch>();

        //subscribing
        if (_damageOnTouch != null)
        {
            _damageOnTouch.OnHit += Die;
            Debug.Log("Subscribed Die method to OnHit event.");
        }

        NewLifeTime.StartCooldown();
        
    }
    
    void Update()
    {
        if (NewLifeTime.CurrentProgress != Cooldown.Progress.Finished) //To check the current status
            return;

        Die();
        
    }

    void Die() 
    {
        //unsubscribing
        if (_damageOnTouch !=null)
            _damageOnTouch.OnHit -= Die;

        NewLifeTime.StopCooldown();
        Destroy(gameObject);
    }
}
