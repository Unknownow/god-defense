using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoobyTrapCollision : TrapCollision
{
    private BoobyTrapProperties TrapProperties
    {
        get
        {
            return (BoobyTrapProperties)_trapProperties;
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
            other.gameObject.GetComponent<EnemyTrapInteraction>().StepOnBoobyTrap(gameObject, TrapProperties.HitDamage, TrapProperties.TimeInterval);
        }

    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            ReliableOnTriggerExit.NotifyTriggerExit(other, gameObject);
            other.gameObject.GetComponent<EnemyTrapInteraction>().StepOutBoobyTrap(gameObject);
        }
    }
}
