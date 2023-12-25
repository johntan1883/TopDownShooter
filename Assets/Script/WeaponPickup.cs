using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup
{
    public GameObject Weapon;

    protected override void PickedUp(Collider2D col)
    {
        WeaponHnadler weaponHandler = col.GetComponent<WeaponHnadler>();   

        if (weaponHandler == null)
            return;

        weaponHandler.EquipWeapon(Weapon);
    }
}
