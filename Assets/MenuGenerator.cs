using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Vehicles.Ball;
using UnityEngine.SceneManagement;

public class MenuGenerator : MonoBehaviour {
    
    private float height = 0;
    private List<List<GameObject>> blocks;
    public List<GameObject> pickups = null;

    [SerializeField] public Material greenMat;
    [SerializeField] public Material blueMat;
    [SerializeField] public Material yellowMat;
    [SerializeField] public Material redMat;
    [SerializeField] public Material greyMat;

    private int numOfBioms;
    private int[][] blockPrs;
    //private Queue<int> ids;

    // Use this for initialization
    void Start ()
    {
        blockPrs = new int[18][];
        numOfBioms = 0;

        //D.N.O - use for testing
        NewBiom(
            0, //percentage of white blocks
            0, //green
            0, //blue
            0, //red
            0, // yellow
            0,//gray1
            100, //gray2
            ref blockPrs,ref numOfBioms);

        //    ids = new Queue<int>();
        blocks = new List<List<GameObject>>(24);
        pickups = new List<GameObject>();
        for (int i = 0; i < 24; ++i)
            blocks.Add(new List<GameObject> { null, null, null, null, null, null, null, null });
        for (int i = 0; i < 6; ++i)
        {
            GenerateChunk();
        }
    }

    void GenerateChunk()
    {
        int wasButton = 4;
        //Debug.Log("Current level: "+level+", Biom: "+biom);
        for (int j = 0; j < 24; ++j) 
        {
            for (int i = 0; i < 8; ++i)
            {
                
                //Debug.Log(Instantiate(Resources.Load("Cube") as GameObject, new Vector3(-3.5f, -5 + height, -2.5f + 0.7f * i), Quaternion.identity).GetInstanceID());
                int id = 0;
                float r = Random.value;
                if (!blocks[j][i])
                {
                    Object obj = Instantiate(Resources.Load("Cube") as GameObject, new Vector3(-3.5f, -5 + height, -2.5f + 0.7f * i), Quaternion.identity);

                    blocks[j][i] = obj as GameObject;
                    //Debug.Log(blocks[20][i].ToString() + " " + blocks[21][i].ToString() + " " + blocks[22][i].ToString() + " " + blocks[23][i].ToString() + "\n DEBUG");
                    if((j != 9 && j != 11 && j != 13 && j != 15) || (i < 2 || i > 5)){
                        (obj as GameObject).GetComponent<Renderer>().material = greyMat;
                        (obj as GameObject).AddComponent<DisabledBlockBehaviour>();
                    }
                    else{
                        blocks[j][i + 1] = blocks[j][i + 2] = blocks[j][i + 3] = obj as GameObject;
                        (obj as GameObject).transform.localScale = new Vector3(1.7f, 0.7f, 2.8f);
                        (obj as GameObject).transform.position += new Vector3(0, 0, 1.05f);
                        --wasButton;
                        if(wasButton == 0){
                            (obj as GameObject).GetComponent<Renderer>().material = redMat;
                            (obj as GameObject).AddComponent<ButtonBlockBehaviour>();
                            Object text = Instantiate(Resources.Load("Button") as GameObject, new Vector3(-3.5f, -4.82f + height, -2.4f + 0.7f * i), Quaternion.identity);
                            (text as GameObject).GetComponent<RectTransform>().transform.Rotate(0, -90, 0);
                            
                        }
                        else{
                            (obj as GameObject).GetComponent<Renderer>().material = greyMat;
                            (obj as GameObject).AddComponent<DisabledBlockBehaviour>();
                        }

                     }
                    if( j < 8 || j >16 || i == 0 || i > 5){
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
                    }
                    /*if(j%2 != 0){
                        (obj as GameObject).GetComponent<Renderer>().material = yellowMat;
                    }
                    else{
                        (obj as GameObject).GetComponent<Renderer>().material = redMat;
                    }*/

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
