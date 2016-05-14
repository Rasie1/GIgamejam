using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Ball
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.


        private void Start()
        {
            GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;
        }
    }
}
