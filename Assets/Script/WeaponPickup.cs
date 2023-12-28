using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup
{
    public GameObject Weapon;
    public GameObject PickUpSound;

    protected override void PickedUp(Collider2D col)
    {
        WeaponHnadler weaponHandler = col.GetComponent<WeaponHnadler>();   

        if (weaponHandler == null)
            return;

        PickUpSound = GameObject.FindGameObjectWithTag("Pickup Weapon");
        if (PickUpSound.GetComponent<AudioSource>() != null)
        {
            PickUpSound.GetComponent<AudioSource>().Play();
        }

        weaponHandler.EquipWeapon(Weapon);
    }
}
