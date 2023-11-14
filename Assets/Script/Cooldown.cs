using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cooldown
{
    //Name of replacement for integer
    public enum Progress 
    { 
        Ready,      //== 0
        Started,    //== 1
        Inprogress, //== 2
        Finished    //== 3
    }

    public Progress CurrentProgress = Progress.Ready;

    public float Duration = 1.0f;
    public float CurrentDuration
    {
        get 
        {
            return _currentDuration;
        }
    }

    public bool IsOnCooldown 
    {
        get { return _isOnCoolDown; }
    }
    
    private float _currentDuration;
    private bool _isOnCoolDown;

    private Coroutine _coroutine;

    public void StartCooldown() 
    {
        if (CurrentProgress is Progress.Started or Progress.Inprogress)
            return;

        _coroutine = CoroutineHost.Instance.StartCoroutine(DoCooldown());
    }

    public void StopCooldown() 
    { 
        if(_coroutine != null) 
            CoroutineHost.Instance.StopCoroutine(_coroutine);

        _currentDuration = 0.0f;
        _isOnCoolDown = false;
        CurrentProgress = Progress.Ready;
    }

    IEnumerator DoCooldown()
    {
        CurrentProgress = Progress.Started;
        _currentDuration = Duration;
        _isOnCoolDown = true;

        while(_currentDuration > 0) 
        {
            _currentDuration -= Time.deltaTime;
            CurrentProgress = Progress.Inprogress;

            yield return null;
        }

        _currentDuration = 0.0f;
        _isOnCoolDown = false;

        CurrentProgress = Progress.Finished;
    }
}
