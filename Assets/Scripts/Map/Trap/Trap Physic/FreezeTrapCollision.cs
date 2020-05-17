using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTrapCollision : TrapCollision
{
    private FreezeTrapProperties TrapProperties
    {
        get
        {
            return (FreezeTrapProperties)_trapProperties;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.transform.CompareTag("Enemy"))
        {
            ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);
            other.gameObject.GetComponent<EnemyTrapInteraction>().StepOnFreezeTrap(gameObject, TrapProperties.SlowPercentage);
        }

    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            ReliableOnTriggerExit.NotifyTriggerExit(other, gameObject);
            other.gameObject.GetComponent<EnemyTrapInteraction>().StepOutFreezeTrap(gameObject);
        }
    }
}
