using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityStandardAssets.Vehicles.Ball;

public class healthTrigger : MonoBehaviour {

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    [SerializeField] private static float healthPackValue = 15f;
    void OnTriggerEnter(Collider myTrigger)
    {
        AddHealth(healthPackValue);
    }

    public static void AddHealth(float value)
    {
        Ball.Health += healthPackValue;
        if (Ball.Health > 100)
            Ball.Health = 100;
    }
}
