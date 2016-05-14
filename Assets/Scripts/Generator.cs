using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generator : MonoBehaviour {
    
    private float height = 0;
    private Queue<int> ids;

	// Use this for initialization
	void Start ()
    {
        ids = new Queue<int>();
        for (int i = 0; i < 6; ++i)
            GenerateChunk();
    }
	
    void GenerateChunk()
    {
        for (int j = 0; j < 4; ++j) 
        {
            for (int i = 0; i < 8; ++i)
            {
                ids.Enqueue(Instantiate(Resources.Load("Cube") as GameObject, new Vector3(-3.5f, -5 + height, -2.5f + 0.7f * i), Quaternion.identity).GetInstanceID());
                //some random shit
            }
            height += 0.7f;
        }
    }

    void DestroyChunk()
    {
        for (int i = 0; i < 32; ++i)
            Destroy(UnityEditor.EditorUtility.InstanceIDToObject(ids.Dequeue()));
    }

    // Update is called once per frame
    void Update () {
        if (GameObject.Find("Ball").transform.position.y > height - 15 * 0.7f)
        {
            GenerateChunk();
            DestroyChunk();
        }
    }
}
