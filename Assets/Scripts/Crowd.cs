using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();

        int type = Random.Range(1, 3);

        switch (type)
        {
            case 0:
                anim.SetFloat("Blend", 2);
                break;

            case 1:
                anim.SetFloat("Blend", 3);
                break;
        }
    }


}
