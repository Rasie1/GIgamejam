using UnityEngine;
using System.Collections;

public class AlternatingBlockBehaviour : DisabledBlockBehaviour {

    float DelayRandomization;
    private float AlternateDelay = 2f;
    private float nextTime;

    protected override void Deactivate()
    {
        base.Deactivate();
        DelayRandomization = Random.value;
        nextTime = UnityEngine.Time.time + AlternateDelay + DelayRandomization;
    }

    protected override void Activate()
    {
        base.Activate();
        DelayRandomization = Random.value;
        nextTime = UnityEngine.Time.time + AlternateDelay + DelayRandomization;
    }

    void Update()
    {
        base.UpdateBlock();
        if (Time.time > nextTime)
            IsActivated = !IsActivated;
    }   
}
