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
        // if (Input.GetKeyDown(KeyCode.Mouse0)) {
        //     animator.SetBool("runFlag", true);
        // } else if (Input.GetKeyDown(KeyCode.Mouse1)) {
        //     animator.SetBool("runFlag", false);
        // }
    }

    // private void OnTriggerEnter(Collider other) {
    //     if (other.transform.CompareTag("Bullet"))
    //     {
    //         Debug.Log("Getting hit");
    //         animator.SetBool("runFlag", true);
    //     }
    // }

    private void OnCollisionEnter(Collision other) {
        if (other.transform.CompareTag("Bullet"))
        {
            Debug.Log("Getting hit");
            animator.SetBool("runFlag", true);
        }  
    }
}
