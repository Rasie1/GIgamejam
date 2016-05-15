using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TemporaryBlockBehaviour : BlockBehaviour
{
    private float DeactivateDelay = 1;
    private float nextTime;

    protected override void Activate()
    {
        Debug.Log("1");
        base.Activate();
        nextTime = UnityEngine.Time.time + DeactivateDelay;
    }

    protected override void Deactivate()
    {
        Debug.Log("2");
        base.Deactivate();
    }

    void Update()
    {
        base.UpdateBlock();
        if (Time.time > nextTime && IsActivated)
            IsActivated = false;
    }   
}

