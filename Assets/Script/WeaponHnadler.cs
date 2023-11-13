using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHnadler : MonoBehaviour
{
    public Weapon CurrentWeapon;
    public Transform GunPosition;

    protected bool _tryShoot = false;

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleWeapon();
    }

    protected virtual void HandleInput() 
    { 
       
    }

    protected virtual void HandleWeapon()
    {
        if (CurrentWeapon == null) 
        return;

        CurrentWeapon.transform.position = GunPosition.position;
        CurrentWeapon.transform.rotation = GunPosition.rotation;

        if (_tryShoot)
        {
            CurrentWeapon.Shoot();
        }
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (weapon == null)
            return;

        CurrentWeapon = weapon;
    }
}


