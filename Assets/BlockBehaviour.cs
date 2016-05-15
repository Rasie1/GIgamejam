using UnityEngine;
using System.Collections;

public class BlockBehaviour : MonoBehaviour {

    private bool isActivated;
    public Vector3 deactivatedPosition;
    public Vector3 activatedPosition;
    private AudioSource sound;

    public bool bDontActivate;

    public float ActivationMoveSpeed = 10;
    private float ActivationOffset = -2;

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
                    if(!bDontActivate)
                        Activate();
                }
            }
            else
            {
                if (isActivated)
                {
                    isActivated = false;
                    if(!bDontActivate)
                        Deactivate();
                }
            }
        }
    }

    protected virtual void Activate() {
        sound.Play();
    }

    protected virtual void Deactivate()
    {
        sound.Play();
    }

    protected virtual void Init()
    {
        IsActivated = false;
        deactivatedPosition = this.transform.position;
        activatedPosition.Set(ActivationOffset, this.transform.position.y, this.transform.position.z);
        sound = GetComponent<AudioSource>();
    }

	void Start () {
        Init();
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

    protected virtual void OnMouseDown() {
        IsActivated = !IsActivated;
    }
}
