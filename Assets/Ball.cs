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
        private GameObject eye;
        private AudioSource[] source;
        private int score;
        private int Hscore;
        int deathCounter = 0;

        float r;

        public float CurrentHealth = 100f;
        public bool hope = true;

        UnityEngine.UI.Text Scores;

        private void Start()
        {
            score = 0;
            Hscore = PlayerPrefs.GetInt("Hscore");
            GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;
            //collider = gameObject.AddComponent<SphereCollider>();

            ballVisualMesh = GameObject.Find("BallVisualMesh");
            eye = GameObject.Find("Eye");

            source = ballVisualMesh.GetComponents<AudioSource>();

            UnityEngine.UI.Image image = GameObject.Find("ImageDied").GetComponent<UnityEngine.UI.Image>();
            GameObject.Find("ImageLastChance").GetComponent<UnityEngine.UI.Image>().enabled = false;
            float scale = Screen.width / image.rectTransform.rect.width;
            Vector3 newScale = image.transform.localScale;
            newScale.x = scale;
            newScale.y = scale;
            //image.transform.localScale = newScale;

            image.enabled = false;
            Scores = GameObject.Find("ScoresText").GetComponent<UnityEngine.UI.Text>();
        }

        public void Die()
        {
            if (deathCounter == 0)
            {
                Instantiate(Resources.Load("WaterDrop"), this.transform.position + new Vector3(0, 0.3f, 0), new Quaternion(180, 90, 180, 0));
                hope = false;
                GameObject.Find("ImageDied").GetComponent<UnityEngine.UI.Image>().enabled = true;
                deathCounter = 1;
                if (score>Hscore) PlayerPrefs.SetInt("Hscore", score);
            }

            //ballVisualMesh.transform.localScale = new Vector3(0, 0, 0);
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
                    CurrentHealth -= 3;
                }
            }
            if (deathCounter > 0) 
                ++deathCounter;
            if (deathCounter > 60)
            {
                hope = true;
                deathCounter = 0;
                CurrentHealth = 100f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
  
        private void Update()
        {
            if (CurrentHealth < 1)
            {
                Die();
                isLastChanceActive = false;
            }
            ballVisualMesh.transform.position = this.transform.position;
            
            eye.transform.position = new Vector3(transform.position.x + 1f, transform.position.y + 0.1f, transform.position.z);


            var vel = GetComponent<Rigidbody>().velocity;
            ballVisualMesh.transform.up = vel;
            var mag = vel.magnitude;
            var width = System.Math.Min(5 / mag, 1);
            var hpCoeff = 0.1f + CurrentHealth / 100f;
            ballVisualMesh.transform.localScale = new Vector3(width * hpCoeff, (mag / 10 + 1) * hpCoeff, width * hpCoeff);
            width = System.Math.Max(width, 0.5f);
            GameObject.Find("Ball").GetComponent<SphereCollider>().radius = width * hpCoeff * 0.55f;
            ballVisualMesh.GetComponent<TrailRenderer>().startWidth = width;

            //eye.transform.LookAt(vel);
            //eye.transform.rotation = new Quaternion(eye.transform.rotation.x, eye.transform.rotation.y, 0, 0);
            eye.transform.rotation = new Quaternion();
            //eye.transform.rotation.SetEulerAngles(eye.transform.rotation.x, 90, eye.transform.rotation.z);
            eye.transform.Rotate(0, 90, 0);

            float offset = Convert.ToSingle(Time.time);
            GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset,0));
        }

        void OnCollisionEnter(Collision collision)
        {
            
            if (collision.gameObject.GetComponent<BouncingBlockBehaviour>()){
                Vector3 newVelocity;
                newVelocity.x = GetComponent<Rigidbody>().velocity.x*1.5f;
                newVelocity.y = GetComponent<Rigidbody>().velocity.y*1.5f;
                newVelocity.z = GetComponent<Rigidbody>().velocity.z*1.5f;
                GetComponent<Rigidbody>().velocity =  newVelocity;
                //Debug.Log(newVelocity);
            };
            if (collision.relativeVelocity.magnitude<5){
                source[0].Play();
            }
            else {
                r = UnityEngine.Mathf.Round(UnityEngine.Random.value) + 1;
                source[(int)r].Play();
            }
            //Debug.Log(collision.relativeVelocity.magnitude);
        }

        void OnTriggerEnter(Collider collider)
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
        void  OnGUI (){
            if (score < (int)ballVisualMesh.transform.position.y)
            {
                score = (int)ballVisualMesh.transform.position.y;
            }
            
            Scores.text = "Best: " + Hscore + "  Scores: " + score;
        }
        public float getHealth()
        {
            return CurrentHealth;
        }

        private bool isLastChanceActive = false;

        public void ActivateLastChanceMode()
        {
            CurrentHealth = 25;
            isLastChanceActive = true;
            lastChanceDamageCounter = lastChanceDamageDelay;
        }

        public void SucceedLastChance()
        {
            isLastChanceActive = false;
            GameObject.Find("ImageLastChance").GetComponent<UnityEngine.UI.Image>().enabled = false;
        }
    }
}
