using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHumanController : MonoBehaviour
{

    CharacterController controller;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey (KeyCode.G)) {
            animator.SetBool("runFlag", true);
        } else if (Input.GetKey (KeyCode.H)) {
            animator.SetBool("runFlag", false);
        }
    }
}
