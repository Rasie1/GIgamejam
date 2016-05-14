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
    [SerializeField] public Material greyMat;

    private int level;
    private int biom;
    private int numOfBioms;
    private int[][] blockPrs;
    //private Queue<int> ids;

	// Use this for initialization
	void Start ()
    {
        level = 0;

        biom = 0;
        blockPrs = new int[8][];
        numOfBioms = 0;

        NewBiom(0,0,0,0,0,100,0,ref blockPrs,ref numOfBioms);

        //BIOM 1
        NewBiom(95,100,0,0,0,0,0,ref blockPrs,ref numOfBioms);

        //BIOM 2
        NewBiom(90,75,0,100,0,0,0,ref blockPrs,ref numOfBioms);

        //BIOM 3
        NewBiom(90,50,0,100,0,0,0,ref blockPrs,ref numOfBioms);

        //BIOM 4
        NewBiom(90,25,0,75,0,0,100,ref blockPrs,ref numOfBioms);

        //BIOM 5
        NewBiom(90,25,0,50,0,0,100,ref blockPrs,ref numOfBioms);

        //BIOM 6
        NewBiom(85,25,50,75,0,0,100,ref blockPrs,ref numOfBioms);
        
        //BIOM 7
        NewBiom(85,20,40,60,80,0,100,ref blockPrs,ref numOfBioms);


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
        if(level == 1 && biom == 0){ 
            ++level;
            ++biom;
        }
        else{
            ++level;
            if(level>=10){
                if(numOfBioms > biom+1)
                    biom++;
                level = 0;
            }
        }
        Debug.Log("Current level: "+level+", Biom: "+biom);
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
                    };

                    //Coloring
                    if(r > (float)blockPrs[biom][0]/100){
                        if(rColor < (float)blockPrs[biom][1]/100){
                            (obj as GameObject).GetComponent<Renderer>().material = greenMat;
                            (obj as GameObject).AddComponent<TemporaryBlockBehaviour>();
                        }
                        else if(rColor < (float)blockPrs[biom][2]/100){
                            (obj as GameObject).GetComponent<Renderer>().material = blueMat;
                            (obj as GameObject).AddComponent<BlockBehaviour>();
                        }
                        else if (rColor < (float)blockPrs[biom][3] / 100)
                        {
                            (obj as GameObject).GetComponent<Renderer>().material = redMat;
                            (obj as GameObject).AddComponent<ActivatedBlockBehaviour>();
                        }
                        else if (rColor < (float)blockPrs[biom][4] / 100)
                        {
                            (obj as GameObject).GetComponent<Renderer>().material = greyMat;
                            (obj as GameObject).AddComponent<DisabledBlockBehaviour>();
                        }
                        else if (rColor < (float)blockPrs[biom][5] / 100)
                        {
                            (obj as GameObject).GetComponent<Renderer>().material = greyMat;
                            (obj as GameObject).AddComponent<DisabledActivatedBlockBehaviour>();
                        }
                        else if (rColor < (float)blockPrs[biom][7] / 100)
                        {
                            (obj as GameObject).GetComponent<Renderer>().material = greyMat;
                            (obj as GameObject).AddComponent<BouncingBlockBehaviour>();
                        }
                        else {
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

    void NewBiom(int overall, int green, int red, int blue, int yellow, int gray1, int gray2, ref int[][] blockPrs, ref int numOfBioms){
        blockPrs[numOfBioms] = new int[7];
        blockPrs[numOfBioms][0] = overall;
        blockPrs[numOfBioms][1] = green;
        blockPrs[numOfBioms][2] = red;
        blockPrs[numOfBioms][3] = blue;
        blockPrs[numOfBioms][4] = yellow;
        blockPrs[numOfBioms][5] = gray1;
        blockPrs[numOfBioms][6] = gray2;
        ++numOfBioms;
    } 
}
