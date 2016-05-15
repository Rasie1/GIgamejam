using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Ball;

public class BallCamera : MonoBehaviour {

	[SerializeField] private Rigidbody m_Rigidbody;
	private Vector3 newPos; 

	// Use this for initialization
	void Start () {
	
	} 
	
	// Update is called once per frame
	void Update () {
		if (FindObjectOfType<Ball>().getHealth() <= 0 || !FindObjectOfType<Ball>().hope) 
			return;
		newPos.x = 5.2f;
		newPos.y = m_Rigidbody.position.y;
		newPos.z = -0.05f;
		transform.position = newPos; 
	}
}
