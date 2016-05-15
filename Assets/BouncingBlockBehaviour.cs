using UnityEngine;
using System.Collections;

public class BouncingBlockBehaviour : ActivatedBlockBehaviour
{
    protected override void Init()
    {
        base.Init();
        //GetComponent<Renderer>().material = (PhysicMaterial)Resources.Load("Materials/BouncyBlockMaterial");
        GetComponent<Collider>().material.bounciness = 1f;
    }
}
