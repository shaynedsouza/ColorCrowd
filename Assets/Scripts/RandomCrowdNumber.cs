using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCrowdNumber : MonoBehaviour
{
    
    [SerializeField] CrowdSpawning crowdSpawning;
    [SerializeField] List<GameObject> CrowbMembers;
    int _RandomCrowdNumber;
    public GameObject parent;
     int mat;
   // Start is called before the first frame update
   void Start()
    {
        CrowbMembers = new List<GameObject>();
        parent = this.gameObject;
       
        foreach(Transform child in parent.transform)
        {
            CrowbMembers.Add(child.gameObject);

            //mat = Random.Range(0, 3);

            if (VariableHolder.instance.Level_Count == 1)
            {
                mat = Random.Range(0, 2);
            }
            else if(VariableHolder.instance.Level_Count == 2)
            {
                mat = Random.Range(1, 3);
            }
            else if (VariableHolder.instance.Level_Count >= 3)
            {
                mat = Random.Range(0, 3);
            }

            //Debug.Log(mat);
            foreach (GameObject childinchild in CrowbMembers)
            {
                childinchild.GetComponentInChildren<SkinnedMeshRenderer>().material = GameManager._instance._mCrowd[mat];
            }

            crowdSpawning.Material_Index = mat+1;
        }

       
        for (int i = 0;i<CrowbMembers.Count;i++)
        {
            _RandomCrowdNumber = Random.Range(0, i);
            CrowbMembers[_RandomCrowdNumber].gameObject.SetActive(true);
        }
    }

    //Running animation for crowd--------
    public void crowd_Anim(bool play)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.GetComponent<Animator>().SetBool("move", play);
        }
    }

    public void z_Pos_Clamp()
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
        }
    }

}
