using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Movement
{
    public Transform Target;

    protected override void HandleInput()
    {
        if (Target == null)
        {
            Target = GameObject.FindWithTag("Enemy").transform;
        }

        if (Target == null)
        {
            return;
        }

        _inputDirection = (Target.position - transform.position).normalized;
    }
}
