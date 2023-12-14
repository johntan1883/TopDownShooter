using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthbar : MonoBehaviour
{
    public Image Healthbar;

    private Transform _player;
    private Health _playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        if (_player == null)
            return;

        _playerHealth = _player.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Healthbar == null || _playerHealth == null)
            return;

        if(_playerHealth == null)
        {
            Healthbar.fillAmount = 0;
            return;
        }

        float fillAmount = _playerHealth.CurrentHealth/ _playerHealth.MaxHealth;

        Healthbar.fillAmount = fillAmount;
    }
}
