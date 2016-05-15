using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Vehicles.Ball
{
    public class Ball : MonoBehaviour
    {
        [SerializeField]
        private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.

        private Vector3 originalScale;
        private Collider collider;
        private GameObject ballVisualMesh;
        private AudioSource[] source;
        private int score;
        int counter = 0;

        float r;

        public static float Health = 100f;
        public bool hope = true;
        
        

        private void Start()
        {
            score = 0;
            GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;
            //collider = gameObject.AddComponent<SphereCollider>();
            
            ballVisualMesh = GameObject.Find("BallVisualMesh");

            source = ballVisualMesh.GetComponents<AudioSource>();

            UnityEngine.UI.Image image = GameObject.Find("ImageDied").GetComponent<UnityEngine.UI.Image>();
            GameObject.Find("ImageLastChance").GetComponent<UnityEngine.UI.Image>().enabled = false;
            float scale = Screen.width / image.rectTransform.rect.width;
            Vector3 newScale = image.transform.localScale;
            newScale.x = scale;
            newScale.y = scale;
            image.transform.localScale = newScale;

            image.enabled = false;
        }

        public void Die()
        {
            if (counter == 0)
            {
                hope = false;
                GameObject.Find("ImageDied").GetComponent<UnityEngine.UI.Image>().enabled = true;
                counter = 1;
            }

            ballVisualMesh.transform.localScale = new Vector3(0, 0, 0);
        }

        private int lastChanceDamageCounter = 0;
        private int lastChanceDamageDelay = 15;

        private void FixedUpdate()
        {
            if (isLastChanceActive)
            {
                --lastChanceDamageCounter;
                if (lastChanceDamageCounter == 0)
                {
                    lastChanceDamageCounter = lastChanceDamageDelay;
                    Ball.Health -= 3;
                }
            }
            if (counter > 0) ++counter;
            if (counter > 60)
            {
                hope = true;
                counter = 0;
                Health = 100f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
  
        private void Update()
        {
            if (Ball.Health < 1)
            {
                Die();
                isLastChanceActive = false;
            }
            ballVisualMesh.transform.position = this.transform.position;

            var vel = GetComponent<Rigidbody>().velocity;
            ballVisualMesh.transform.up = vel;
            var mag = vel.magnitude;
            var width = System.Math.Min(5 / mag, 1);
            var hpCoeff = 0.1f + Ball.Health / 100f;
            ballVisualMesh.transform.localScale = new Vector3(width * hpCoeff, (mag / 10 + 1) * hpCoeff, width * hpCoeff);
            width = System.Math.Max(width, 0.5f);
            GameObject.Find("Ball").GetComponent<SphereCollider>().radius = width * hpCoeff * 0.55f;
            ballVisualMesh.GetComponent<TrailRenderer>().startWidth = width;

            float offset = Convert.ToSingle(Time.time);
            GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset,0));
        }

        void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.GetComponent<BouncingBlockBehaviour>()){
                Vector3 newVelocity;
                newVelocity.x = GetComponent<Rigidbody>().velocity.x*1.5f;
                newVelocity.y = GetComponent<Rigidbody>().velocity.y*1.5f;
                newVelocity.z = GetComponent<Rigidbody>().velocity.z*1.5f;
                GetComponent<Rigidbody>().velocity =  newVelocity;
                //Debug.Log(newVelocity);
            };
            if(collision.relativeVelocity.magnitude<5){
                source[0].Play();
            }
            else{
                r = UnityEngine.Mathf.Round(UnityEngine.Random.value) + 1;
                source[(int)r].Play();
            }
            //Debug.Log(collision.relativeVelocity.magnitude);
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
        void  OnGUI (){
            if (score < (int)ballVisualMesh.transform.position.y)
            {
                score = (int)ballVisualMesh.transform.position.y;
            }

            GUI.Box ( new Rect(10, 10, 100, 20), "Scores: " +score);
        }
        public float getHealth()
        {
            return Health;
        }

        private bool isLastChanceActive = false;

        public void ActivateLastChanceMode()
        {
            Ball.Health = 25;
            isLastChanceActive = true;
            lastChanceDamageCounter = lastChanceDamageDelay;
        }
    }
}
