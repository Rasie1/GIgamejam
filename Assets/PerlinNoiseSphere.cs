using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets
{

    public class PerlinNoiseSphere : MonoBehaviour
    {
        [SerializeField] private float _scaleField = 0.25f;
        [SerializeField] private float _speedField = 1;
        [SerializeField] private bool _recalculateNormalsField = true;

        // Other code.

        public void ScaleInput()
        {
            float scaleInput = _scaleField;
        }

        public void SpeedInput()
        {
            float speedInput = _speedField;
        }

        public void RecalcNormalsInput()
        {
            bool recalculateNormalsInput = _recalculateNormalsField;
        }


        private GameObject _sphere;
        private Vector3[] _baseVertices;
        private Perlin noise = new Perlin();

        // Use this for initialization
        void Start()
        {
            _sphere = GameObject.Find("BallVisualMesh");
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(-1.83f, transform.position.y, transform.position.z);
            Mesh mesh = _sphere.GetComponent<MeshFilter>().mesh;

            if (_baseVertices == null)
                _baseVertices = mesh.vertices;

            var vertices = new Vector3[_baseVertices.Length];

            var timex = Time.time*_speedField + 0.1365143;
            var timey = Time.time*_speedField + 1.21688;
            var timez = Time.time*_speedField + 2.5564;
            for (var i = 0; i < vertices.Length; i++)
            {
                var vertex = _baseVertices[i];

                vertex.x +=
                    noise.Noise((float) (timex + vertex.x), (float) (timex + vertex.y), (float) (timex + vertex.z))*
                    _scaleField;
                vertex.y +=
                    noise.Noise((float) (timey + vertex.x), (float) (timey + vertex.y), (float) (timey + vertex.z))*
                    _scaleField;
                vertex.z +=
                    noise.Noise((float) (timez + vertex.x), (float) (timez + vertex.y), (float) (timez + vertex.z))*
                    _scaleField;

                vertices[i] = vertex;
            }

            mesh.vertices = vertices;

            if (_recalculateNormalsField)
                mesh.RecalculateNormals();
            mesh.RecalculateBounds();
        }
    }

}