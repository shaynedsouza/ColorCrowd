using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCTRL : MonoBehaviour
{
    #region Variables
    public static PlayerCTRL instance;
     
    [SerializeField] public List<GameObject> Players;
    [SerializeField] List<Rigidbody> Players_rigidbody;
    [SerializeField] List<Material> Materials;
    [SerializeField] ParticleSystem[] Parti;
    public GameObject busted_player;

    //Spped Value
    float speed;

    //Horizontal speed
    float mh;

    //Vertical Movement Spped
    public float[] mV = new float[5];

    [SerializeField] float[] mV_values;

    [SerializeField]float timer;

    public int Material_Index;

    //Level Progress Bar
    [SerializeField] GameObject player;
    float total_Distance;
    float player_Current_Pos;
    float distance_Travelled;
    public static float sliderValue;

   
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Set_Speed(0);

        foreach(GameObject obj in Players)
        {
            obj.GetComponent<Animator>().SetBool("Run", false);
            obj.GetComponent<Animator>().SetBool("Dance", false);
        }
    }

    //This Function Changes Color Of theifs
    public void Set_playerColor(int type)
    {
        StartCoroutine(Player_ColorChange(type));
    }

    public void Set_Speed(int type)
    {
        switch (type)
        {
            case 0:
                speed = 3.6f;
                break;
            case 1:
                speed = 2;
                break;
        }
    }


    private void Update()
    {
        if (VariableHolder.instance.IsPlaying && Players.Count!=0)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                timer = 5;
                Set_Group_Speed();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Leave_Oneplayer();
            }

            Start_Animation();
        }
        else
        {
            VariableHolder.instance.IsPlaying = false; 
        }

        LevelProgress();

    }


    private void FixedUpdate()
    {
        if (VariableHolder.instance.IsPlaying && Players.Count != 0)
        {
            if (Players_rigidbody.Count >= 1) Players_rigidbody[0].velocity = new Vector3(mh * speed, Players_rigidbody[0].velocity.y, mV[0] * speed);
            if (Players_rigidbody.Count >= 2) Players_rigidbody[1].velocity = new Vector3(mh * speed, Players_rigidbody[1].velocity.y, mV[1] * speed);
            if (Players_rigidbody.Count >= 3) Players_rigidbody[2].velocity = new Vector3(mh * speed, Players_rigidbody[2].velocity.y, mV[2] * speed);
            if (Players_rigidbody.Count >= 4) Players_rigidbody[3].velocity = new Vector3(mh * speed, Players_rigidbody[3].velocity.y, mV[3] * speed);
            if (Players_rigidbody.Count >= 5) Players_rigidbody[4].velocity = new Vector3(mh * speed, Players_rigidbody[4].velocity.y, mV[4] * speed);
        }

        //transform.GetComponent<Rigidbody>().velocity = new Vector3(mh * speed, transform.GetComponent<Rigidbody>().velocity.y, mV[0] * speed);
    }

    public void Leave_Oneplayer()
    {
        int count = Players.Count - 1;

        Players[count].transform.parent = null;

        Players[count].GetComponent<Animator>().SetBool("Run", false);
        Players[count].GetComponent<Collider>().enabled = false;
        Players[count].GetComponent<Rigidbody>().isKinematic = true;

        UiManager.instance.theif_iconChange();

        busted_player = Players[count];

        Players.Remove(Players[count]);

        Players_rigidbody.Remove(Players_rigidbody[count]);

        if (Players.Count <= 0)
        {
            GameManager._instance.GameOver();
        }
    }

    

    public void Start_Player_dance()
    {
        foreach (GameObject obj in Players)
        {
           // obj.transform.Rotate(0, 180, 0);
            obj.GetComponent<Animator>().SetBool("Run", true);
            obj.GetComponent<Animator>().SetBool("Dance", true);
        }
    }

    private void Set_Group_Speed()
    {
        int speed_change_1 = Random.Range(0, 2);
        int speed_change_2 = Random.Range(0, 2);
        int speed_change_3 = Random.Range(0, 2);
        int speed_change_4 = Random.Range(0, 2);
        int speed_change_5 = Random.Range(0, 2);

        mV[0] = mV_values[speed_change_1];
        mV[1] = mV_values[speed_change_2];
        mV[2] = mV_values[speed_change_3];
        mV[3] = mV_values[speed_change_4];
        mV[4] = mV_values[speed_change_5];

       // mV[0] = 1;
    }

    private void Start_Animation()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].GetComponentInChildren<Animator>().SetBool("Run", true);
        }
    }

    public void Stop_Animation()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].GetComponentInChildren<Animator>().SetBool("Run", false);
        }
    }

    private IEnumerator Player_ColorChange(int type)
    {
        Material_Index = type;

        for (int i = 0; i < Players.Count; i++)
        {

            if (i > 1)
            {
                yield return new WaitForSeconds(0.2f);
            }

            Players[i].GetComponentInChildren<SkinnedMeshRenderer>().material = Materials[type];

            Parti[type - 1].Play();
        }

        
    }

    void LevelProgress()
    {
        total_Distance = LevelGeneration.instance.endpoint.transform.position.z - LevelGeneration.instance.startpoint.transform.position.z;
        player_Current_Pos = player.transform.position.z - 10;
        distance_Travelled = total_Distance - (LevelGeneration.instance.endpoint.transform.position.z - player_Current_Pos);
        float percentage_Travelled = (distance_Travelled * 100) / total_Distance;
        sliderValue = percentage_Travelled / 100;
    }


}
