using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //private Vector3 offset = new Vector3 (0f, 0f, -10f);
    //private float smoothTime = 0.25f;
    //private Vector3 velocity = Vector3.zero;

    //[SerializeField] private Transform player;
    //private void Start()
    //{
    //    player = GameObject.FindWithTag("Player").transform;
    //}
    //// Update is called once per frame
    //void Update()
    //{
    //    if(player == null)
    //        return;

    //    Vector3 targetPosition = player.position + offset;
    //    transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    //}

    public Vector3 PositionOffSet = Vector3.zero;
    public float LerpSpeed = 5f;

    protected Vector2 targetPos = Vector2.zero;
    protected Vector2 _initialOffset = Vector2.zero;

    private PlayerWeaponHandler _playerWeaponHandler;

    private void Start()
    {
        _playerWeaponHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeaponHandler>();
    }

    private void FixedUpdate()          
    {                                       
        if (_playerWeaponHandler == null)                   
            return;

        targetPos = Vector2.Lerp(targetPos, _playerWeaponHandler.AimPosition(),Time.deltaTime *LerpSpeed);;

        transform.position = new Vector3(targetPos.x, targetPos.y +  PositionOffSet.y, PositionOffSet.z);
    }
}
