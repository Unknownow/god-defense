using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_First_Controller : MonoBehaviour
{
    CharacterController controller;
    Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag("Bullet"))
        {
            animator.SetBool("runFlag", true);
        }
    }
}
