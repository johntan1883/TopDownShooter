using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public enum Firemode {Single, Burst, Auto}
public class Weapon : MonoBehaviour
{
    
    public enum FireModes 
    { 
        Auto,       // = 0
        SingleFire, // = 1
        BurstFire   // = 2
    }

    [Header("Weapon Sources")]
    public GameObject Projectile;
    public Transform SpawnPos;

    [Header("Fire Mode")]
    public float Porjectile_Interval = 0.1f;
    public Cooldown AutoFireShootInterval; //Cooldown timer

    public FireModes FireMode;
    public float Spread = 0f;

    public int BurstFireAmount = 3;
    public float BurstFireInterval = 0.1f;

    private bool _canShoot = true;
    private bool _singleFireReset = true;

    private bool _burstFiring = false;
    private float _lastShootRequestAt;

    private KeyCode _cycleFireModeKey = KeyCode.B;

    [Header("Sound Effects")]
    public GameObject[] Feedbacks;

    void Update()
    {
        //Swithing fireMode
        if (Input.GetKeyDown(_cycleFireModeKey))
        {
            Debug.Log("test");
            CycleFireMode();
        }

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

        switch (FireMode) 
        {
            case FireModes.Auto:
                {
                    AutoFireShoot();
                    break;
                }
            case FireModes.SingleFire:
                {
                    break;
                }
            case FireModes.BurstFire: 
                {
                    BurstFireShoot();
                    break;
                }
        }
    }

    void SpawnFeedbacks() 
    { 
        foreach (var feedback in Feedbacks) 
        {
            GameObject.Instantiate(feedback, SpawnPos.position, SpawnPos.rotation);
        }
    }

    void AutoFireShoot() 
    {
        if (AutoFireShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;

        //Spawn Projectile
        GameObject bullet = GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation);

        AutoFireShootInterval.StartCooldown();

        SpawnFeedbacks();
    }

    void SingleFireShoot() 
    { 

        
    }

    void BurstFireShoot() 
    {
        if (!_canShoot)
            return;

        if (_burstFiring)
            return;

        if (!_singleFireReset)
            return;

        if (AutoFireShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;

        StartCoroutine(BurstFireCo(1f));
    }

    IEnumerator BurstFireCo(float time = 3f) 
    {
        _burstFiring = true;
        _singleFireReset = false;

        int remaingShots = BurstFireAmount;

        while(remaingShots > 0) 
        {
            float randomRot = Random.Range(-Spread, Spread);

            //Spawn Projectile
            GameObject bullet = GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation);
            SpawnFeedbacks();
            _lastShootRequestAt = Time.time;

            remaingShots--;
            yield return WaitFor(BurstFireInterval); ;
        }

        _burstFiring = false;
        AutoFireShootInterval.StartCooldown();
    }

    IEnumerator WaitFor(float seconds) 
    { 
        for (float timer = 0f; timer < seconds; timer += Time.deltaTime) 
        { 
            yield return null;
        }
    }

    public void StopShoot() 
    { 
        _singleFireReset = true;
    }

    private void CycleFireMode()
    {

        Debug.Log("test fire cycle");
        if ((int)FireMode < 2)
            FireMode += 1;
        else
            FireMode = 0;
    }
}
