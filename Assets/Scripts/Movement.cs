using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
            transform.position += Time.deltaTime * new Vector3(0, 2, 0);
        if (Input.GetKey(KeyCode.DownArrow))
            transform.position += Time.deltaTime * new Vector3(0, -2, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.position += Time.deltaTime * new Vector3(-2, 0, 0);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.position += Time.deltaTime * new Vector3(2, 0, 0);
    }
}
