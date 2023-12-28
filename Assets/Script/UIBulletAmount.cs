using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIBulletAmount : MonoBehaviour
{
    public Image BulletCountFillImage;
    
    private Transform weapon;
    private Weapon _playerWeapon;

    // Update is called once per frame
    void Update()
    {
        UpdateBulletCountUI();
    }

    private void FixedUpdate()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon").transform;

        if (weapon == null)
            return;

        _playerWeapon = weapon.GetComponent<Weapon>();
    }

    void UpdateBulletCountUI()
    {
        if (_playerWeapon != null && BulletCountFillImage != null)
        {
            float fillAmount = (float)_playerWeapon.CurrentBulletCount / _playerWeapon.MaxBulletCount;
            BulletCountFillImage.fillAmount = fillAmount;
        }
    }
}
