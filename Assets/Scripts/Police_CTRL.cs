using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police_CTRL : MonoBehaviour
{
    public static Police_CTRL _instance;
    [SerializeField] Animator police_Anim;
    private Rigidbody police_RB;
    public float speed = 3.5f;
    public bool run = true;
    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        police_RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (VariableHolder.instance.IsPlaying)
        {
            if (run)
            {
                police_Anim.SetBool("Run", true);
                police_RB.velocity = Vector3.forward * speed;
                police_RB.isKinematic = false;
            }
            else
            {
                police_Anim.SetBool("Run", false);
                police_RB.isKinematic = true;
            }
            
        }
        else
        {
            police_Anim.SetBool("Run", false);
            police_RB.isKinematic = true;
            
        }
        
    }
}
