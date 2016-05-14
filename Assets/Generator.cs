using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Vehicles.Ball;

public class Generator : MonoBehaviour {
    
    private float height = 0;
    private List<List<GameObject>> blocks;
    public List<GameObject> pickups = null;

    [SerializeField] public GameObject wallLeft;
    [SerializeField] public GameObject wallRight;
    [SerializeField] public Material greenMat;
    [SerializeField] public Material blueMat;
    [SerializeField] public Material yellowMat;
    [SerializeField] public Material redMat;
    //private Queue<int> ids;

	// Use this for initialization
	void Start ()
    {
        //    ids = new Queue<int>();
        blocks = new List<List<GameObject>>(24);
        pickups = new List<GameObject>();
        for (int i = 0; i < 24; ++i)
            blocks.Add(new List<GameObject> { null, null, null, null, null, null, null, null });
        for (int i = 0; i < 6; ++i)
        {
            UpdateInfo();
            GenerateChunk();
        }
    }

    void GenerateChunk()
    {
        for (int j = 20; j < 24; ++j) 
        {
            for (int i = 0; i < 8; ++i)
            {
                
                //Debug.Log(Instantiate(Resources.Load("Cube") as GameObject, new Vector3(-3.5f, -5 + height, -2.5f + 0.7f * i), Quaternion.identity).GetInstanceID());
                int id = 0;
                float r = Random.value;
                float rColor = Random.value;
                if (!blocks[j][i])
                {
                    var shouldSpawnHealthpack = Random.value > 0.9;
                    for (int pickup = pickups.Count - 1; pickup >= 0; --pickup)
                    {
                        var x = pickups[pickup];
                        if (Vector3.SqrMagnitude(x.transform.position - GameObject.Find("Ball").transform.position) > 256f)
                        {
                            Destroy(x);
                            pickups.RemoveAt(pickup);
                        }
                    }
                    if (shouldSpawnHealthpack)
                    {
                        Object pickup = Instantiate(Resources.Load("HealthPack") as GameObject, new Vector3(-1.7f, -5 + height, -2.5f + 0.7f * i), Quaternion.identity);
                        pickups.Add(pickup as GameObject);
                    }
                    Object obj = Instantiate(Resources.Load("Cube") as GameObject, new Vector3(-3.5f, -5 + height, -2.5f + 0.7f * i), Quaternion.identity);
                    blocks[j][i] = obj as GameObject;
                    //Debug.Log(blocks[20][i].ToString() + " " + blocks[21][i].ToString() + " " + blocks[22][i].ToString() + " " + blocks[23][i].ToString() + "\n DEBUG");
                    if (r > 0.75 && r <= 0.85 && i != 7 && !blocks[j][i + 1])
                    {
                        blocks[j][i + 1] = obj as GameObject;
                        (obj as GameObject).transform.localScale = new Vector3(1.7f, 0.7f, 1.4f);
                        (obj as GameObject).transform.position += new Vector3(0, 0, 0.35f);
                    }
                    if (r > 0.85 && r <= 0.95 && j != 23)
                    {
                        blocks[j + 1][i] = obj as GameObject;
                        (obj as GameObject).transform.localScale = new Vector3(1.7f, 1.4f, 0.7f);
                        (obj as GameObject).transform.position += new Vector3(0, 0.35f, 0);
                    }
                    if (r > 0.95 && j != 23 && i != 7 && !blocks[j][i + 1])
                    {
                        blocks[j + 1][i + 1] = obj as GameObject;
                        blocks[j + 1][i] = obj as GameObject;
                        blocks[j][i + 1] = obj as GameObject;
                        (obj as GameObject).transform.localScale = new Vector3(1.7f, 1.4f, 1.4f);
                        (obj as GameObject).transform.position += new Vector3(0, 0.35f, 0.35f);
                    }
                    if(r > 0.9){
                        if(rColor < 0.25){
                            (obj as GameObject).GetComponent<Renderer>().material = greenMat;
                            (obj as GameObject).AddComponent<TemporaryBlockBehaviour>();
                        }
                        else if(rColor < 0.5){
                            (obj as GameObject).GetComponent<Renderer>().material = blueMat;
                            (obj as GameObject).AddComponent<BlockBehaviour>();
                        }
                        else if(rColor < 0.75){
                            (obj as GameObject).GetComponent<Renderer>().material = redMat;
                            (obj as GameObject).AddComponent<ActivatedBlockBehaviour>();
                        }
                        else{
                            (obj as GameObject).GetComponent<Renderer>().material = yellowMat;
                            (obj as GameObject).AddComponent<BlockBehaviour>();
                        }

                    }
                    else{
                        (obj as GameObject).AddComponent<BlockBehaviour>();
                    }
                }
            }
            height += 0.7f;
        }
    }

    void DestroyChunk()
    {
        for (int i = 0; i < 4; ++i)
            for (int j = 0; j < 8; ++j)
            {
                
                Destroy(blocks[i][j]);
            }
    }

    void UpdateInfo()
    {
        for (int i = 4; i < 24; ++i)
            for (int j = 0; j < 8; ++j)
                blocks[i - 4][j] = blocks[i][j];
        for (int i = 20; i < 24; ++i)
            for (int j = 0; j < 8; ++j)
                blocks[i][j] = null;
    }

    // Update is called once per frame
    void Update () {
        var ball = GameObject.Find("Ball");
        if (ball.transform.position.y > height - 15 * 0.7f)
        {
            DestroyChunk();
            UpdateInfo();
            GenerateChunk();
        } else if (GameObject.Find("Ball").transform.position.y < height - 32 * 0.7f)
        {
            FindObjectOfType<Ball>().Die();
        }
        Vector3 wallLeftPos = wallLeft.transform.position;
        wallLeftPos.y = ball.transform.position.y;
        wallLeftPos.x = ball.transform.position.x;
        Vector3 wallRightPos = wallRight.transform.position;
        wallRightPos.y = ball.transform.position.y;
        wallRightPos.x = ball.transform.position.x;

        wallLeft.transform.position = wallLeftPos;
        wallRight.transform.position = wallRightPos;

    }
}
