using UnityEngine;
using System.Collections;

public class BallCamera : MonoBehaviour {

	[SerializeField] private Rigidbody m_Rigidbody;
	private Vector3 newPos; 

	// Use this for initialization
	void Start () {
	
	} 
	
	// Update is called once per frame
	void Update () {
		newPos.x = transform.position.x;
		newPos.y = m_Rigidbody.position.y;
		newPos.z = 0;
		transform.position = newPos; 
	}
}
