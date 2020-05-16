using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCollision : MonoBehaviour
{
    private TrapProperties _trapProperties;

    private void Awake()
    {
        _trapProperties = gameObject.GetComponent<TrapProperties>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Bullet"))
        {
            _trapProperties.BuffTrap = true;
        }
    }
}
