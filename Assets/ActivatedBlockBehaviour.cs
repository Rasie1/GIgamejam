using UnityEngine;
using System.Collections;

public class ActivatedBlockBehaviour : BlockBehaviour
{
    protected override void Init()
    {
        base.Init();
        IsActivated = true;
    }
    private float DeactivateDelay = 1;
    private float nextTime;

    protected override void Activate()
    {
        base.Activate();
        nextTime = UnityEngine.Time.time + DeactivateDelay;
        IsActivated = false;
    }

    void Update()
    {
        base.UpdateBlock();
        if (Time.time > nextTime && !IsActivated)
            IsActivated = true;
    }   
}
