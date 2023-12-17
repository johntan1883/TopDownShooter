using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void HitEvent(GameObject source);
    public HitEvent OnHit;

    public delegate void ResetEvent();
    public ResetEvent OnHitReset;

    public float MaxHealth = 10f;
    public Cooldown Invulnerable;

    //Animation for dead 
    public Animator Animator;

    public float CurrentHealth 
    {
        get { return _currentHealth; }
    }

    private float _currentHealth = 10f;
    private bool _canDamage = true;

    void FixedUpdate()
    {
        ResetInvulnerable();
    }

    private void ResetInvulnerable() 
    {
        if (_canDamage)
            return;

        if (Invulnerable.IsOnCooldown && _canDamage == false)
            return;

        _canDamage = true;
        OnHitReset?.Invoke();
    }
    public void Damage(float damage, GameObject source) 
    {
        if (!_canDamage)
        {
            Debug.Log("Cannot damage.");
            return;
        }

        _currentHealth -= damage;

        if(_currentHealth <= 0f) 
        { 
            _currentHealth = 0f;
            SetDie();
        }

        Invulnerable.StartCooldown();
        _canDamage = false;

        Debug.Log("Damage taken " + damage + "from " + source.name);
        OnHit?.Invoke(source);
    }

    public void SetDie() 
    {
        Debug.Log("Set bool to dead");
        Animator.SetTrigger("Death");
    }

    public void DestroyObject()
    {
        Debug.Log("Destroying object after animation.");
        Destroy(this.gameObject);
    }
}
