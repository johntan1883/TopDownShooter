using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    private bool _fireReset = true;

    private bool _burstFiring = false;
    private float _lastShootRequestAt;

    private KeyCode _cycleFireModeKey = KeyCode.B;

    [Header("Sound Effects")]
    public GameObject[] Feedbacks;
    public GameObject[] ReloadFeedbacks;

    [Header("Reload")]
    public int ProjectileCount = 1;
    public Cooldown ReloadCooldown;
    public int MaxBulletCount = 12;
    public int CurrentBulletCount
    {
        get { return currentBulletCount; }
    }

    protected int currentBulletCount;

    private void Start()
    {
        currentBulletCount = MaxBulletCount;
    }

    void Update()
    {
        //Swithing fireMode
        if (Input.GetKeyDown(_cycleFireModeKey))
        {
            Debug.Log("test");
            CycleFireMode();
        }

        UpdateReloadCooldown();
        UpdateShootCooldown();
    }

    private void UpdateReloadCooldown()
    {
        if (ReloadCooldown.CurrentProgress != Cooldown.Progress.Finished)
            return;

        if (ReloadCooldown.CurrentProgress == Cooldown.Progress.Finished)
        {
            currentBulletCount = MaxBulletCount;
        }

        ReloadCooldown.CurrentProgress = Cooldown.Progress.Ready;
    }
    private void UpdateShootCooldown()
    {
        if (AutoFireShootInterval.CurrentProgress != Cooldown.Progress.Finished)
            return;

        AutoFireShootInterval.CurrentProgress = Cooldown.Progress.Ready;
    }

    public void Shoot()
    {
        if (Projectile == null)
        {
            Debug.LogWarning("Missing Projectile prefab");
            return;
        }

        if (SpawnPos == null)
        {
            Debug.LogWarning("Missing SpawnPosition transornm");
            return;
        }

        if (ReloadCooldown.IsOnCooldown || ReloadCooldown.CurrentProgress != Cooldown.Progress.Ready)
            return;

        switch (FireMode) 
        {
            case FireModes.Auto:
                {
                    AutoFireShoot();
                    break;
                }
            case FireModes.SingleFire:
                {
                    SingleFireShoot();
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

    void ShootProjectile() 
    {
        float randomRot = Random.Range(-Spread, Spread);

        //Spawn Projectile
        GameObject bullet = GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation * Quaternion.Euler(0,0,randomRot));
        SpawnFeedbacks();
    }
    void AutoFireShoot() 
    {
        if (!_canShoot)
            return;

        if (AutoFireShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;

        ShootProjectile();

        currentBulletCount--;
        AutoFireShootInterval.StartCooldown();

        StartReloading();
    }

    void SingleFireShoot() 
    {
        if (!_canShoot)
            return;

        if (!_fireReset)
            return;

        ShootProjectile();

        currentBulletCount--;

        _fireReset = false;
    }

    void BurstFireShoot() 
    {
        if (!_canShoot)
            return;

        if (_burstFiring)
            return;

        if (!_fireReset)
            return;

        if (AutoFireShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;

        StartCoroutine(BurstFireCo(1f));
        StartReloading();
    }

    IEnumerator BurstFireCo(float time = 3f) 
    {
        _burstFiring = true;
        _fireReset = false;

        int remaingShots = BurstFireAmount;

        while(remaingShots > 0) 
        {
            float randomRot = Random.Range(-Spread, Spread);

            ShootProjectile();
            currentBulletCount--;
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
        _fireReset = true;
    }

    private void CycleFireMode()
    {

        Debug.Log("test fire cycle");
        if ((int)FireMode < 2)
            FireMode += 1;
        else
            FireMode = 0;
    }

    void StartReloading() 
    {
        if (currentBulletCount <= 0 && !ReloadCooldown.IsOnCooldown)
        {
            foreach (var feedback in ReloadFeedbacks)
            {
                GameObject.Instantiate(feedback, transform.position, transform.rotation);
            }

            if (GetComponent<AudioSource>() != null)
            {
                GetComponent<AudioSource>().Play();
            }

            ReloadCooldown.StartCooldown();
        }
    }
}
