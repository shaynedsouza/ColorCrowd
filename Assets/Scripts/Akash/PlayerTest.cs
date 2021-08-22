using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTest : MonoBehaviour
{
    float speed = 10;

    #region Level_Progress
    float total_Distance;
    float player_Current_Pos;
    float distance_Travelled;
    public static float sliderValue;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xp = Input.GetAxis("Vertical") * speed * Time.deltaTime ;
        transform.position += new Vector3(0, 0, xp);

        LevelProgress();
    }

    void LevelProgress()
    {
        total_Distance = LevelGeneration.instance.endpoint.transform.position.z - LevelGeneration.instance.startpoint.transform.position.z;
        player_Current_Pos = transform.position.z + 2.36f;
        distance_Travelled = total_Distance - (LevelGeneration.instance.endpoint.transform.position.z - transform.position.z);
        float percentage_Travelled = (distance_Travelled * 100) / total_Distance;
        sliderValue = percentage_Travelled / 100;
    }
}
