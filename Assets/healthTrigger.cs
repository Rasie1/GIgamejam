﻿using UnityEngine;
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
        var ball = FindObjectOfType<Ball>();
        ball.SucceedLastChance();
        source[3].Play();
        gameObject.transform.position = new Vector3(100000, 0, 0);
    }

    public static void AddHealth(float value)
    {
        var ball = GameObject.FindObjectOfType<Ball>();
        var newValue = ball.CurrentHealth + value;
        if (newValue > 100f)
            newValue = 100f;
        ball.CurrentHealth = newValue;
    }
}
