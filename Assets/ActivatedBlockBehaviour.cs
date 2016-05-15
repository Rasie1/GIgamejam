using UnityEngine;
using System.Collections;

public class ActivatedBlockBehaviour : BlockBehaviour
{
    protected override void Init()
    {
        base.Init();
        bDontActivate = true;
        IsActivated = true;
        bDontActivate = false;
        transform.position = activatedPosition;
    }
}
