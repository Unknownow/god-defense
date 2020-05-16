using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrapCollision : TrapCollision
{
    private BombTrapProperties TrapProperties
    {
        get
        {
            return (BombTrapProperties)_trapProperties;
        }
    }
    private BombTrapController TrapController
    {
        get
        {
            return (BombTrapController)_trapController;
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
            TrapController.DetonateBomb();
        }
    }
}
