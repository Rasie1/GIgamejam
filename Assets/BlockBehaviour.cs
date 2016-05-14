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

    protected virtual void Activate() {
        Debug.Log("Block activated");
        this.transform.Translate(2, 0, 0);
    }

    protected virtual void Deactivate()
    {
        Debug.Log("Block deactivated");
        this.transform.Translate(-2, 0, 0);
    }

	void Start () {
        IsActivated = false;
        //deactivatedPosition = this.transform.position;
        //activatedPosition = this.transform.position;

        //activatedPosition.Set(0, 0, -200);
        //Debug.Log("1:");
        //Debug.Log(activatedPosition);
        //Debug.Log("2:");
        //Debug.Log(transform.position);
	}

    void Update()
    {
        //if (IsActivated)
        //{
        //    Vector3.MoveTowards(this.transform.position, activatedPosition, activationMoveSpeed * Time.deltaTime);
        //}
	}   

    void OnMouseDown() {
        IsActivated = true;
    }
}
