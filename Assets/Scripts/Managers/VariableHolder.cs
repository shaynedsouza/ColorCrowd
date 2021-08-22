using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableHolder : MonoBehaviour
{
    #region Instance
    public static VariableHolder instance;

    #endregion


    #region Variables Collection 
    [Header("Bools")]
    public bool IsPlaying = false;
    public bool Colorchange = true;

    #region Level Vars
    public int Min_Tiles;
    public string Playerprefs_MinTiles_Key = "Min_tiles";
    public int Max_Tiles;
    public string Playerprefs_MaxTiles_Key = "Max_tiles";

    public int Level_Count;
    public string Playerprefs_LevelCount_Key = "levelCount";
    #endregion

    #region Player Vars
    [Header("Player Variables")]
    public float colorchange_Delay;
    #endregion


    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        
    }

    private void Start()
    {
        Set_Values();

        GameManager._instance.OnStart();
    }

    public void Set_Values()
    {
        //-----------------------Min Tiles--------------------------------
        if (PlayerPrefs.HasKey(Playerprefs_MinTiles_Key))
        {
            Min_Tiles = PlayerPrefs.GetInt(Playerprefs_MinTiles_Key);
        }
        else
        {
            Min_Tiles = 4;
            PlayerPrefs.SetInt(Playerprefs_MinTiles_Key, Min_Tiles);
        }
        //----------------------Max Tiles--------------------------------
        if (PlayerPrefs.HasKey(Playerprefs_MaxTiles_Key))
        {
            Max_Tiles = PlayerPrefs.GetInt(Playerprefs_MaxTiles_Key);
        }
        else
        {
            Max_Tiles = 8;
            PlayerPrefs.SetInt(Playerprefs_MaxTiles_Key, Max_Tiles);
        }
        //-----------------------LEVEL COUNT--------------------------------
        if (PlayerPrefs.HasKey(Playerprefs_LevelCount_Key))
        {
            Level_Count = PlayerPrefs.GetInt(Playerprefs_LevelCount_Key);
        }
        else
        {
            Level_Count = 1;
            PlayerPrefs.SetInt(Playerprefs_LevelCount_Key, Level_Count);
        }
    }

    public void SaveValues()
    {
        PlayerPrefs.SetInt(Playerprefs_MinTiles_Key, Min_Tiles);
        PlayerPrefs.SetInt(Playerprefs_MaxTiles_Key, Max_Tiles);
        PlayerPrefs.SetInt(Playerprefs_LevelCount_Key, Level_Count);
    }

}
