using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class move : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //animator.SetBool("texSwimming", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1)) {
            animator.SetBool("texSwimming", true);
        }
        else if (Input.GetMouseButton(0))
        {
            animator.SetBool("texSwimming", false);
        }
    }
}
