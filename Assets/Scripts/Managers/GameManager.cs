using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using underDOGS.SDKEvents;
public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    [SerializeField] public GameObject player;
    [SerializeField] GameObject player_Pos;

    //police variables
    [SerializeField] GameObject police;
    [SerializeField] GameObject police_pos;
    [SerializeField] GameObject[] Color_buttons;

    GameObject[] confetti;
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    [SerializeField] AudioSource run;
    [SerializeField] public AudioSource win_audio;
    [SerializeField] public AudioSource WinEnd_audio;
    private void Awake()
    {

        _instance = this;
    }
    public List<Material> _mCrowd;

    private void Start()
    {

    }
    private void Update()
    {
        confetti = GameObject.FindGameObjectsWithTag("Confetti");

        if (VariableHolder.instance.Level_Count == 1)
        {
            Color_buttons[0].SetActive(true);
            Color_buttons[1].SetActive(true);
            Color_buttons[2].SetActive(false);
        }
        else if (VariableHolder.instance.Level_Count == 2)
        {
            Color_buttons[0].SetActive(false);
            Color_buttons[1].SetActive(true);
            Color_buttons[2].SetActive(true);
        }
        else if (VariableHolder.instance.Level_Count >= 3)
        {
            Color_buttons[0].SetActive(true);
            Color_buttons[1].SetActive(true);
            Color_buttons[2].SetActive(true);
        }
    }

    public void Set_PlayerColor(int type)
    {
        if (VariableHolder.instance.Colorchange)
        {
            PlayerCTRL.instance.Set_playerColor(type);
        }
    }

    public void OnStart()
    {




        int tilesToSpwan = Random.Range(VariableHolder.instance.Min_Tiles, VariableHolder.instance.Max_Tiles);

        LevelGeneration.instance.Instantiate_Tiles(tilesToSpwan);

        VariableHolder.instance.Min_Tiles++;

        VariableHolder.instance.Max_Tiles++;

        if (VariableHolder.instance.Min_Tiles >= 11) VariableHolder.instance.Min_Tiles = 4;

        if (VariableHolder.instance.Max_Tiles >= 16) VariableHolder.instance.Max_Tiles = 8;

        GameObject obj = Instantiate(player, player_Pos.transform.position, Quaternion.identity);

        //Camera.main.transform.parent = obj.transform.Find("Thief(Player)_1").gameObject.transform;
        cam1.m_Follow = obj.transform.Find("Thief(Player)_1").gameObject.transform;
        cam2.m_Follow = obj.transform.Find("Thief(Player)_1").gameObject.transform;
        VariableHolder.instance.SaveValues();

        //Spawning Police
        Instantiate(police, police_pos.transform.position, Quaternion.identity);



    }

    public void GameOver()
    {

        if (SDKManager.instance != null)
        {
            SDKEventData endData;
            endData.level = VariableHolder.instance.Level_Count;
            endData.status = "fail";
            SDKManager.instance.SendEvent(endData);
        }

        VariableHolder.instance.IsPlaying = false;
        run.Stop();
        UiManager.instance.GameOver_panelSetActive(true);
        PlayerCTRL.instance.Stop_Animation();
    }

    public void TapHereToStart()
    {
        VariableHolder.instance.IsPlaying = true;
        run.Play();
        if (SDKManager.instance != null)
        {
            SDKEventData endData;
            endData.level = VariableHolder.instance.Level_Count;
            endData.status = "start";
            SDKManager.instance.SendEvent(endData);
        }
    }

    public void NextLevel()
    {
        VariableHolder.instance.Level_Count++;
        VariableHolder.instance.SaveValues();
        // SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
        SceneManager.LoadScene("GamePlay");
    }

    public void Retry()
    {
        SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }


    public IEnumerator LevelEnd()
    {

        if (SDKManager.instance != null)
        {
            SDKEventData endData;
            endData.level = VariableHolder.instance.Level_Count;
            endData.status = "complete";
            SDKManager.instance.SendEvent(endData);
        }



        yield return new WaitForSeconds(0.1f);
        //Stopping the sound
        run.Stop();//
        WinEnd_audio.Play();

        //Play Confettie
        Police_CTRL._instance.speed = 0;
        foreach (GameObject obj in confetti)
        {
            obj.GetComponent<ParticleSystem>().Play();
        }
        Cop_Group.instance.cop_run();
        //Play dancing Animations
        PlayerCTRL.instance.Start_Player_dance();

        //Animate the camera
        cam1.m_Priority = 0;
        cam2.m_Lens.FieldOfView = 91;
        cam2.m_Priority = 1;

        UiManager.instance.Set_Text_Finish();

        yield return new WaitForSeconds(5);
        UiManager.instance.NextLevel_panelSetActive(true);
    }
}
