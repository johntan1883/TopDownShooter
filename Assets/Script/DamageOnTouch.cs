using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    //think of it like radio broadcasting
    public delegate void OnHitSomething();
    public OnHitSomething OnHit;

    public float Damage = 1f;
    public float PushForce = 10f;

    public LayerMask TargetLayerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if we hit something that doesn't belong in our TargetLayerMask
        if (!((TargetLayerMask.value & (1 << collision.gameObject.layer)) > 0))
            return;

        Debug.Log("Hit target");
        Health targetHealth = collision.gameObject.GetComponent<Health>();

        Debug.Log(targetHealth);

        if (targetHealth == null)
            return;

        Rigidbody2D targetRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

        if(targetRigidbody != null) 
        {
            targetRigidbody.AddForce((collision.transform.position - transform.position).normalized * PushForce);
        }

        TryDamage(targetHealth);
    }

    private void TryDamage(Health targetHealth)
    {
        targetHealth.Damage(Damage, transform.gameObject);
        Debug.Log("hit " + targetHealth);
        OnHit?.Invoke();
    }
}
