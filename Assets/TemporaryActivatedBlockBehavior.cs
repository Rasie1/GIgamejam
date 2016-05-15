using UnityEngine;
using System.Collections;

public class TemporaryActivatedBlockBehavior : ActivatedBlockBehaviour
{
    private float ActivateDelay = 1.5f;
    private float nextTime;

    protected override void Deactivate()
    {
        base.Deactivate();
        nextTime = UnityEngine.Time.time + ActivateDelay;
    }

    protected override void Activate()
    {
        base.Activate();
    }

    void Update()
    {
        base.UpdateBlock();
        if (Time.time > nextTime && !IsActivated)
            IsActivated = true;
    }   
}
