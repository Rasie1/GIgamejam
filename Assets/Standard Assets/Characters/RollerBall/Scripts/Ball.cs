using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Ball
{
    public class Ball : MonoBehaviour
    {
        [SerializeField]
        private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.

        private Vector3 originalScale;
        private Collider collider;
        private GameObject ballVisualMesh;

        public static float Health = 100f;
        
        

        private void Start()
        {
            GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;
            //collider = gameObject.AddComponent<SphereCollider>();

            ballVisualMesh = GameObject.Find("BallVisualMesh");

            

            ////originalScale = GetComponent<MeshRenderer>().transform.localScale;
        }

        private void Update()
        {
            ballVisualMesh.transform.position = this.transform.position;

            var vel = GetComponent<Rigidbody>().velocity;
            ballVisualMesh.transform.up = vel;
            var scale = ballVisualMesh.transform.localScale;
            var mag = vel.magnitude;
            var width = System.Math.Min(5 / mag, 1);
            ballVisualMesh.transform.localScale = new Vector3(width, mag / 10 + 1, width);
            ballVisualMesh.GetComponent<TrailRenderer>().startWidth = width;

            float offset = Convert.ToSingle(Time.time);
            GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset,0));

        }

        void OnCollisionEnter(Collision collision)
        {

        }


        private void animateBounce(float sqrMagnitude)
        {
            Vector3 newScale = originalScale;
            
            float amount = Mathf.Min(1, sqrMagnitude / 1000);
            newScale.x = (float)(originalScale.x * (1 + (3.5 * amount)));
            newScale.y = (float)(originalScale.y * (1 + (-2.4 * amount)));
            newScale.z = (float)(originalScale.z * (1 + (3.5 * amount)));
            this.ballVisualMesh.transform.localScale = newScale;

            //            scaleParent.transform.localScale = originalScale;
            //        Ani.Mate.From(scaleParent.transform, 1, {"localScale": newScale, "easing": Ani.Easing.Elastic, "direction": Ani.Easing.Out});
        }
    }
}
