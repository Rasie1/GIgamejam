using UnityEngine;
using System.Collections;
//using UnityEditor;
using UnityStandardAssets.Vehicles.Ball;

public class healthTrigger : MonoBehaviour {
    public AudioSource[] source; 

    // Use this for initialization
    void Start () {
        source = GameObject.Find("BallVisualMesh").GetComponents<AudioSource>();
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    [SerializeField] private static float healthPackValue = 25f;
    void OnTriggerEnter(Collider myTrigger)
    {
        AddHealth(healthPackValue);
        source[3].Play();
        Destroy(gameObject);
    }

    public static void AddHealth(float value)
    {
        Ball.Health += healthPackValue;
        if (Ball.Health > 100)
            Ball.Health = 100;
        
        
    }
}
