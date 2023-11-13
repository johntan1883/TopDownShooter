using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Projectile;
    public Transform SpawnPos;
    public float Porjectile_Interval = 0.1f;

    private float _timer = 0f;
    private bool _canShoot = true;

    // Update is called once per frame
    void Update()
    {
        if(_timer < Porjectile_Interval) 
        {
            _timer += Time.deltaTime;
            _canShoot = false;
            return;
        }

        _timer = 0f;
        _canShoot = true;
    }

    public void Shoot() 
    { 
        if(Projectile == null)
        {
            Debug.LogWarning("Missing Projectile prefab");
            return;
        }

        if (SpawnPos == null) 
        {
            Debug.LogWarning("Missing SpawnPosition transornm");
            return;
        }

        if (!_canShoot) 
        {
            Debug.LogWarning("Cant shoot yet");
            return;
        }

        Debug.Log("spawning bullet");
        GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation);
    }
}
