using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Firemode {Single, Burst, Auto}
public class Weapon : MonoBehaviour
{
    [Header("Weapon Sources")]
    public GameObject Projectile;
    public Transform SpawnPos;
    
    public float Porjectile_Interval = 0.1f;
    public Cooldown AutoFireShootInterval; //Cooldown timer

    [Header("Sound Effects")]
    public GameObject[] Feedbacks;
    
    void Update()
    {
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

        SpawnFeedbacks();
    }

    void SpawnFeedbacks() 
    { 
        foreach (var feedback in Feedbacks) 
        {
            GameObject.Instantiate(feedback, SpawnPos.position, SpawnPos.rotation);
        }
    }
}
