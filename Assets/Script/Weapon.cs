using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Firemode {Single, Burst, Auto}
public class Weapon : MonoBehaviour
{
    [Header("Input")]
    //[SerializeField] private KeyCode _shootKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _cycleFireModeKey = KeyCode.B;

    [Header("Weapon Sources")]
    public GameObject Projectile;
    public Transform SpawnPos;

    [Header("Weapon Logic")]
    [SerializeField] private Firemode _fireMode = Firemode.Single;
    
    public float Porjectile_Interval = 0.1f;
    public Cooldown AutoFireShootInterval; //Cooldown timer

    //private float _timer = 0f;
    //private bool _canShoot = true;
    //private bool _shootInput = false;//might be the same as _canShoot
    //private bool _bursting = false;


    // Update is called once per frame
    void Update()
    {
        //Cycle Fire Mode
        if (Input.GetKeyDown(_cycleFireModeKey))
        {
            CycleFireMode();
        }

        //switch (_fireMode)
        //{
        //    case Firemode.Auto:
        //        _shootInput = Input.GetKey(_shootKey);//Hold shooting
        //        break;

        //    default://Single & Burst
        //        _shootInput = Input.GetKeyDown(_shootKey);
        //        break;
        //}



        //**REPLACE THIS WITH COOLDOWN CLASS**
        //Shoot
        //if (_timer < Porjectile_Interval && !_bursting)
        //{
        //    _timer += Time.deltaTime;
        //    _canShoot = false;

        //    //Check & Start Burst
        //    if (_fireMode == Firemode.Burst)
        //    {
        //        _bursting = true;
        //        StartCoroutine(BurstFire());
        //    }
        //}

        //_timer = 0f;
        //_canShoot = true;

        if (AutoFireShootInterval.CurrentProgress != Cooldown.Progress.Finished)
            return;

        AutoFireShootInterval.CurrentProgress = Cooldown.Progress.Ready;
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

        
        if (AutoFireShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;

        //Spawn Projectile
        GameObject bullet = GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation);

        AutoFireShootInterval.StartCooldown();
    }

    //private IEnumerator BurstFire() 
    //{ 
    //    yield return new WaitForSeconds(1);
    //    Shoot();
    //    yield return new WaitForSeconds(1);
    //    Shoot();
    //    yield return new WaitForSeconds(1);
    //    _bursting = false;
    //}

    private void CycleFireMode()
    {
        if ((int)_fireMode < 2)
            _fireMode += 1;
        else
            _fireMode = 0;
    }
}
