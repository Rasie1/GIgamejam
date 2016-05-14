using UnityEngine;
using System.Collections;

public class ActivatedBlockBehaviour : BlockBehaviour
{
    protected override void Init()
    {
        base.Init();
        IsActivated = true;
    }
}
