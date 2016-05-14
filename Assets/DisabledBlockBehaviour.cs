using UnityEngine;
using System.Collections;

public class DisabledBlockBehaviour : ActivatedBlockBehaviour
{
    protected override void Init()
    {
        base.Init();
        IsActivated = false;
    }

    protected override void OnMouseDown()
    {
        // do nothing
    }
}
