using UnityEngine;
using System.Collections;

public class BlockBehaviour : MonoBehaviour {

    private bool isActivated;
    public bool IsActivated {
        get
        {
            return IsActivated;
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
	}
	
	void Update () {
	
	}   

    void OnMouseDown() {
        IsActivated = true;
    }
}
