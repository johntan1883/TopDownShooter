using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void HitEvent(GameObject source);
    public HitEvent OnHit;

    public delegate void ResetEvent();
    public ResetEvent OnHitReset;

    public delegate void HealEvent(GameObject source);
    public HealEvent OnHeal;

    public float MaxHealth = 10f;
    public Cooldown Invulnerable;

    //Animation for dead 
    public Animator Animator;
    public SpriteRenderer sprite;

    public float CurrentHealth 
    {
        get { return _currentHealth; }
    }

    public delegate void DeathEvent();
    public DeathEvent OnDeath;

    private float _currentHealth = 10f;
    private bool _canDamage = true;
    private void Start()
    {
        _currentHealth = MaxHealth;
    }
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

        if (sprite != null)
        {
            FlashingRed();
            if (GetComponent<AudioSource>() != null)
            {
                GetComponent<AudioSource>().Play();
            }
        }

        if (_currentHealth <= 0f) 
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
        Collider2D collider = GetComponent<Collider2D>();

        if (collider != null)
            collider.enabled = false;

        Debug.Log("Set bool to dead");
        Animator.SetTrigger("Death");
    }

    public void DestroyObject()
    {
        Debug.Log("Destroying object after animation.");
        OnDeath?.Invoke();
        Destroy(this.gameObject);
    }

    public void Heal (float healAmount)
    {
        _currentHealth += healAmount;

        _currentHealth = Mathf.Clamp( _currentHealth, 0f, MaxHealth );

        OnHeal?.Invoke(gameObject);
    }

    private void FlashingRed()
    {
        StartCoroutine(DoFlashingRed());
    }

    private IEnumerator DoFlashingRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
