using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrapEffect : MonoBehaviour
{
    protected TrapProperties _trapProperties;
    protected TrapStatesController _trapStates;
    protected TrapController _trapController;
    protected List<EnemyTrapInteraction> _affectedEnemies;

    protected virtual void Awake()
    {
        _trapProperties = gameObject.GetComponent<TrapProperties>();
        _trapController = gameObject.GetComponent<TrapController>();
        _trapStates = gameObject.GetComponent<TrapStatesController>();
        _affectedEnemies = new List<EnemyTrapInteraction>();
    }

    protected virtual void OnDisable()
    {
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);
        if (other.transform.CompareTag("Bullet"))
        {
            _trapStates.BuffingTrap();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        ReliableOnTriggerExit.NotifyTriggerExit(other, gameObject);
    }

    public virtual void ReapplyWhenBuffed()
    {
    }
}
