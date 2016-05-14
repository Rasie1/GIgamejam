﻿using UnityEngine;
using System.Collections;

public class BlockBehaviour : MonoBehaviour {

    private bool isActivated;
    private Vector3 deactivatedPosition;
    private Vector3 activatedPosition;

    public float ActivationMoveSpeed = 10;
    public float ActivationOffset = 1;

    public bool IsActivated {
        get
        {
            return isActivated;
        }
        set
        {
            if (value)
            {
                if (!isActivated)
                {
                    isActivated = true;
                    Activate();
                }
            }
            else
            {
                if (isActivated)
                {
                    isActivated = false;
                    Deactivate();
                }
            }
        }
    }

    protected virtual void Activate() {
        Debug.Log("Block activated");
        //this.transform.Translate(2, 0, 0);
    }

    protected virtual void Deactivate()
    {
        Debug.Log("Block deactivated");
        //this.transform.Translate(-2, 0, 0);
    }

	void Start () {
        IsActivated = false;
        deactivatedPosition = this.transform.position;
        activatedPosition.Set(ActivationOffset, this.transform.position.y, this.transform.position.z);
	}

    protected void UpdateBlock()
    {
        if (IsActivated)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, activatedPosition, ActivationMoveSpeed * Time.deltaTime);
        }
        if (!IsActivated)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, deactivatedPosition, ActivationMoveSpeed * Time.deltaTime);
        }
    }

    void Update()
    {
        UpdateBlock();
	}   

    void OnMouseDown() {
        IsActivated = true;
    }
}
