using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    #region Instance
    public static UiManager instance;
    #endregion

    #region Variables
    [SerializeField] GameObject ColourButtons_Panel;
    [SerializeField] GameObject GameOver_Panel;
    [SerializeField] GameObject NextLevel_Panel;
    [SerializeField] Slider progress_bar;
    [SerializeField] Text levelCount_Display;

    [SerializeField] GameObject[] Postive_Text; 
    [SerializeField] GameObject Negative_Text;
    [SerializeField] GameObject busted_Text;
    [SerializeField] GameObject finish_text;

    [SerializeField] List<GameObject> Icons_Jailed;

    int index;
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        progress_bar.value = PlayerCTRL.sliderValue;
        levelCount_Display.text = VariableHolder.instance.Level_Count.ToString();
    }

    public void ColorButtons_panelSetActive(bool status)
    {
        ColourButtons_Panel.SetActive(status);
    }

    public void GameOver_panelSetActive(bool status)
    {
        GameOver_Panel.SetActive(status);
    }

    public void NextLevel_panelSetActive(bool status)
    {
        NextLevel_Panel.SetActive(status);
    }

    public void Play_text(int type,int mattype)
    {
        switch (type)
        {
            case 0:

                Postive_Text[mattype - 1].SetActive(true);
                Postive_Text[mattype - 1].GetComponent<TweenManager>().OnOpen();
                GameManager._instance.win_audio.Play();
                break;

            case 1:

                Negative_Text.SetActive(true);
                busted_Text.SetActive(true);//
                Negative_Text.GetComponent<TweenManager>().OnOpen();
                busted_Text.GetComponent<TweenManager>().OnOpen();//


                break;
        }

        Invoke("disable_Text", 1.2f);
    }

    public void Set_Text_Finish()
    {
        finish_text.SetActive(true);
        finish_text.GetComponent<TweenManager>().OnOpen();
    }

    public void disable_Text()
    {
        foreach(GameObject obj in Postive_Text)
        {
            obj.GetComponent<TweenManager>().OnClose();
            //obj.SetActive(false);
        }

        Negative_Text.GetComponent<TweenManager>().OnClose();
        busted_Text.GetComponent<TweenManager>().OnClose();//
        //Negative_Text.SetActive(false);
    }

    public void theif_iconChange()
    {

        Icons_Jailed[index].GetComponent<Image>().enabled = true;

        index++;
    }

}
