using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject Weapon;

    public GameObject[] PickupFeedbacks;

    public LayerMask TargetLayerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!((TargetLayerMask.value & (1 << collision.gameObject.layer)) > 0))
            return;

        WeaponHnadler weaponHandler = collision.GetComponent<WeaponHnadler>();

        if (weaponHandler == null)
            return;

        weaponHandler.EquipWeapon(Weapon);

        foreach (var feedback in PickupFeedbacks) 
        { 
            GameObject.Instantiate(feedback,transform.position,transform.rotation);
        }

        Destroy(gameObject);
    }
}
