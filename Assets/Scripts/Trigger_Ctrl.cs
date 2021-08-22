using UnityEngine;

public class Trigger_Ctrl : MonoBehaviour
{

    [SerializeField]CrowdSpawning crowdSpawning;

    [SerializeField] GameObject Color_Check_End;

    [SerializeField] Animator crowd_Move_Anim;

    [SerializeField] GameObject crowd;
    enum Trigger_type { colorCheck, colorpanelCheckEnd, endpoint, text_color_check , move_Crowd}

    [SerializeField] Trigger_type trigger_type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (trigger_type)
            {
                case Trigger_type.colorCheck:

                    VariableHolder.instance.Colorchange = false;

                    if (PlayerCTRL.instance.Material_Index != crowdSpawning.Material_Index)
                    {
                        if (PlayerCTRL.instance.Players.Count != 0)
                        {
                            PlayerCTRL.instance.Set_Speed(1);
                            Color_Check_End.SetActive(true);
                        }
                    }
                    crowd.GetComponent<RandomCrowdNumber>().crowd_Anim(false);
                    break;

                case Trigger_type.colorpanelCheckEnd:
                    PlayerCTRL.instance.Set_Speed(0);
                    PlayerCTRL.instance.Leave_Oneplayer();
                    Cop_Group.instance.leave_onepolice();
                    Color_Check_End.SetActive(false);
                    break;

                case Trigger_type.text_color_check:

                    set_text();

                    VariableHolder.instance.Colorchange = true;

                    break;

                case Trigger_type.endpoint:

                    VariableHolder.instance.IsPlaying = false;
                    
                    UiManager.instance.ColorButtons_panelSetActive(false);
                    StartCoroutine(GameManager._instance.LevelEnd());

                    break;

                case Trigger_type.move_Crowd:

                    crowd_Move_Anim.SetBool("move", true);
                    crowd.GetComponent<RandomCrowdNumber>().crowd_Anim(true);

                    break;


            }
        }
    }


    public void set_text()
    {
        if (PlayerCTRL.instance.Material_Index != crowdSpawning.Material_Index)
        {
            if (PlayerCTRL.instance.Players.Count != 0)
            {
                UiManager.instance.Play_text(1, crowdSpawning.Material_Index);
            }
        }
        else
        {
            UiManager.instance.Play_text(0, crowdSpawning.Material_Index);
        }
    }
}
