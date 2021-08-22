using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cop_Group : MonoBehaviour
{
    public static Cop_Group instance;

    [SerializeField] List<GameObject> Police_GRP;

    private void Awake()
    {
        instance = this;
    }
    public void leave_onepolice()
    {
        int count = Police_GRP.Count - 1;

        Police_GRP[count].transform.parent = null;

        Police_GRP[count].GetComponent<Animator>().SetBool("beat", true);
        Police_GRP[count].GetComponent<Collider>().enabled = false;//
        Police_GRP[count].GetComponent<Rigidbody>().isKinematic = true;//

        Police_GRP[count].GetComponent<Police_CTRL>().run = false;

        Police_GRP[count].transform.position = new Vector3(PlayerCTRL.instance.busted_player.transform.position.x, PlayerCTRL.instance.busted_player.transform.position.y, PlayerCTRL.instance.busted_player.transform.position.z - 0.5f);

        Police_GRP.Remove(Police_GRP[count]);
    }

    public void cop_run()
    {
        foreach(GameObject obj in Police_GRP)
        {
            obj.GetComponent<Police_CTRL>().run = false;
        }
    }
}
