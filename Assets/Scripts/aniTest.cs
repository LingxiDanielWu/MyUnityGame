using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aniTest : MonoBehaviour
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
        if (Input.GetMouseButton(1))
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isAttack", false);
        }
        else if (Input.GetMouseButton(0))
        {
            animator.SetBool("isAttack", true);
            animator.SetBool("isWalking", false);
        }
        else if (Input.GetMouseButtonDown(2)) {
            animator.SetTrigger("DeadBitch");
        }
    }
}
