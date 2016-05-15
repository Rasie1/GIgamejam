using UnityEngine;
using System.Collections;

public class BouncingBlockBehaviour : BlockBehaviour
{
    protected override void Init()
    {
        base.Init();
        IsActivated = true;
        //GetComponent<Renderer>().material = (PhysicMaterial)Resources.Load("Materials/BouncyBlockMaterial");
        GetComponent<Collider>().material.bounciness = 1f;
    }
}
