using UnityEngine;
using System.Collections;

public class BlockBehaviour : MonoBehaviour {

    private bool isActivated;
    private Vector3 deactivatedPosition;
    private Vector3 activatedPosition;

    public float activationMoveSpeed;

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

    protected void Activate() {
        Debug.Log("Block activated");
    }

    protected void Deactivate()
    {
        Debug.Log("Block deactivated");
    }

	void Start () {
        IsActivated = false;
        deactivatedPosition = this.transform.position;
        activatedPosition = this.transform.position;

        activatedPosition.Set(0, 0, -200);
        Debug.Log("1:");
        Debug.Log(activatedPosition);
        Debug.Log("2:");
        Debug.Log(transform.position);
	}

    void Update()
    {
        if (IsActivated)
        {
            Debug.Log(transform.position);
            Vector3.MoveTowards(this.transform.position, activatedPosition, activationMoveSpeed * Time.deltaTime);
        }
	}   

    void OnMouseDown() {
        IsActivated = true;
    }
}
