using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrapEffect : TrapEffect
{
    private BombTrapProperties TrapProperties
    {
        get
        {
            return (BombTrapProperties)_trapProperties;
        }
    }
    private BombTrapExplosion _bombTrapExposion;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
