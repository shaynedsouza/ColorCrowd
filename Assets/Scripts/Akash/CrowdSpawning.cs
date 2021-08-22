using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSpawning : MonoBehaviour
{
    [SerializeField] GameObject crowd;
    [SerializeField] GameObject ColorCheck_Trigger;
    [SerializeField] GameObject CrowdEnd_Trigger;

    public int Material_Index;
    public  void Spwan_Crowd()
    {
        crowd.SetActive(true);
        ColorCheck_Trigger.SetActive(true);
        CrowdEnd_Trigger.SetActive(true);
    }
}
