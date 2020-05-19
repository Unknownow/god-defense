using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrapCollision : MonoBehaviour
{
    protected TrapProperties _trapProperties;
    protected TrapController _trapController;
    protected List<EnemyTrapInteraction> _affectedEnemies;

    protected virtual void Awake()
    {
        _trapProperties = gameObject.GetComponent<TrapProperties>();
        _trapController = gameObject.GetComponent<TrapController>();
        _affectedEnemies = new List<EnemyTrapInteraction>();
    }

    protected virtual void OnDisable()
    {
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Bullet"))
        {
            _trapProperties.BuffTrap = true;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
    }
}
