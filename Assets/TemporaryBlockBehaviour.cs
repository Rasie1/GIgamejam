﻿using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TemporaryBlockBehaviour : BlockBehaviour
{
    public float DeactivateDelay;
    private float nextTime;

    protected override void Activate()
    {
        base.Activate();
        nextTime = UnityEngine.Time.time + DeactivateDelay;
    }

    protected override void Deactivate()
    {
        base.Deactivate();
    }

    void Update()
    {
        base.UpdateBlock();
        if (Time.time > nextTime && IsActivated)
            IsActivated = false;
    }   
}

