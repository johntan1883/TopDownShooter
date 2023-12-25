using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public GameObject[] PickupFeedbacks;

    public LayerMask TargetLayerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!((TargetLayerMask.value & (1 << collision.gameObject.layer)) > 0))
            return;

        WeaponHnadler weaponHandler = collision.GetComponent<WeaponHnadler>();

        PickedUp(collision);

        foreach (var feedback in PickupFeedbacks) 
        { 
            GameObject.Instantiate(feedback,transform.position,transform.rotation);
        }

        Destroy(gameObject);
    }

    protected virtual void PickedUp(Collider2D col)
    {

    }
}
