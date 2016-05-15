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
		newPos.x = 5.2f;
		newPos.y = m_Rigidbody.position.y;
		newPos.z = -0.05f;
		transform.position = newPos; 
	}
}
