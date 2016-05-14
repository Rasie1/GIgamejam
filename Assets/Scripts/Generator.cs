using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generator : MonoBehaviour {
    
    private float height = 0;
    private List<List<KeyValuePair<bool, int>>> blocks;

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
        KeyValuePair<bool, int> def = new KeyValuePair<bool, int>(false, 0);
        blocks = new List<List<KeyValuePair<bool, int>>>(24);
        for (int i = 0; i < 24; ++i)
            blocks.Add(new List<KeyValuePair<bool, int>> { def, def, def, def, def, def, def, def });
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
                if (!blocks[j][i].Key)
                {
                    Object obj = Instantiate(Resources.Load("Cube") as GameObject, new Vector3(-3.5f, -5 + height, -2.5f + 0.7f * i), Quaternion.identity);
                    id = obj.GetInstanceID();
                    blocks[j][i] = new KeyValuePair<bool, int>(true, id);
                    //Debug.Log(blocks[20][i].ToString() + " " + blocks[21][i].ToString() + " " + blocks[22][i].ToString() + " " + blocks[23][i].ToString() + "\n DEBUG");
                    if (r > 0.75 && r <= 0.85 && i != 7 && !blocks[j][i + 1].Key)
                    {
                        blocks[j][i + 1] = new KeyValuePair<bool, int>(true, id);
                        (obj as GameObject).transform.localScale = new Vector3(1.7f, 0.7f, 1.4f);
                        (obj as GameObject).transform.position += new Vector3(0, 0, 0.35f);
                    }
                    if (r > 0.85 && r <= 0.95 && j != 23)
                    {
                        blocks[j + 1][i] = new KeyValuePair<bool, int>(true, id);
                        (obj as GameObject).transform.localScale = new Vector3(1.7f, 1.4f, 0.7f);
                        (obj as GameObject).transform.position += new Vector3(0, 0.35f, 0);
                    }
                    if (r > 0.95 && j != 23 && i != 7 && !blocks[j][i + 1].Key)
                    {
                        blocks[j + 1][i + 1] = new KeyValuePair<bool, int>(true, id);
                        blocks[j + 1][i] = new KeyValuePair<bool, int>(true, id);
                        blocks[j][i + 1] = new KeyValuePair<bool, int>(true, id);
                        (obj as GameObject).transform.localScale = new Vector3(1.7f, 1.4f, 1.4f);
                        (obj as GameObject).transform.position += new Vector3(0, 0.35f, 0.35f);
                    }
                    if(r > 0.9){
                        if(rColor < 0.25){
                            (obj as GameObject).GetComponent<Renderer>().material = greenMat;
                            //(obj as GameObject).AddComponent<TemporaryBlockBehaviour>();
                        }
                        else if(rColor < 0.5){
                            (obj as GameObject).GetComponent<Renderer>().material = blueMat;
                        }
                        else if(rColor < 0.75){
                            (obj as GameObject).GetComponent<Renderer>().material = redMat;
                            (obj as GameObject).AddComponent<ActivatedBlockBehaviour>();
                        }
                        else{
                            (obj as GameObject).GetComponent<Renderer>().material = yellowMat;
                        }

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
                Destroy(UnityEditor.EditorUtility.InstanceIDToObject(blocks[i][j].Value));
            }
    }

    void UpdateInfo()
    {
        for (int i = 4; i < 24; ++i)
            for (int j = 0; j < 8; ++j)
                blocks[i - 4][j] = blocks[i][j];
        for (int i = 20; i < 24; ++i)
            for (int j = 0; j < 8; ++j)
                blocks[i][j] = new KeyValuePair<bool, int>(false, 0);
    }

    // Update is called once per frame
    void Update () {
        if (GameObject.Find("Ball").transform.position.y > height - 15 * 0.7f)
        {
            DestroyChunk();
            UpdateInfo();
            GenerateChunk();
        };
        Vector3 wallLeftPos = wallLeft.transform.position;
        wallLeftPos.y = GameObject.Find("Ball").transform.position.y;
        wallLeftPos.x = GameObject.Find("Ball").transform.position.x;
        Vector3 wallRightPos = wallRight.transform.position;
        wallRightPos.y = GameObject.Find("Ball").transform.position.y;
        wallRightPos.x = GameObject.Find("Ball").transform.position.x;

        wallLeft.transform.position = wallLeftPos;
        wallRight.transform.position = wallRightPos;

    }
}
