using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Ball
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.

        private Vector3 originalScale;

        private void Start()
        {
            GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;

            originalScale = GetComponent<Rigidbody>().transform.localScale;
        }

        void OnCollisionEnter(Collision collision)
        {
            float sqrMagnitude = collision.relativeVelocity.sqrMagnitude;
            ContactPoint contact = collision.contacts[0];
            Quaternion normalRotation = Quaternion.FromToRotation(Vector3.up, contact.normal);

            var ball = GameObject.Find("Ball");
            ball.transform.rotation = Quaternion.Inverse(normalRotation);
            animateBounce(sqrMagnitude);
            Debug.Log("www");
//            ball.transform.localScale = originalScale;
        }


        private void animateBounce(float sqrMagnitude)
        {
            Vector3 newScale = originalScale;

            float amount = Mathf.Min(1, sqrMagnitude / 1000);
            newScale.x = (float) (originalScale.x * (1 + (3.5 * amount)));
            newScale.y = (float) (originalScale.y * (1 + (-2.4 * amount)));
            newScale.z = (float) (originalScale.z * (1 + (3.5 * amount)));
            GetComponent<Rigidbody>().transform.localScale = newScale;

//            scaleParent.transform.localScale = originalScale;
            //        Ani.Mate.From(scaleParent.transform, 1, {"localScale": newScale, "easing": Ani.Easing.Elastic, "direction": Ani.Easing.Out});
        }
    }
}
