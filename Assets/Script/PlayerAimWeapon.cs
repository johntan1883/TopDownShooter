using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    private Transform aimTransfrorm;

    private void Awake()
    {
        aimTransfrorm = transform.Find("Aim");
    }

    private void Update()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y,aimDirection.x)*Mathf .Rad2Deg;
        aimTransfrorm.eulerAngles = new Vector3(0,0,angle);
    }

    //Get Mouse Position in World with Z=0f
    public static Vector3 GetMouseWorldPosition() 
    { 
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition,Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ() 
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera) 
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition,Camera worldCamera) 
    { 
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
