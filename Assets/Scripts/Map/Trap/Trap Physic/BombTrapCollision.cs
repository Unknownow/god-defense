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
    private BombTrapExplosion _bombTrapExposion;

    protected override void Awake()
    {
        base.Awake();
        Debug.Log(_trapProperties.GetType()); 
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.transform.CompareTag("Enemy"))
        {
            _bombTrapExposion.DetonateBomb();
        }
    }
}
