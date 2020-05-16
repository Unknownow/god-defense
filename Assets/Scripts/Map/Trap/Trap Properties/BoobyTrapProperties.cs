using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoobyTrapProperties : TrapProperties
{
    [Header("Booby Trap")]
    [SerializeField]
    private float _hitDamage;
    [SerializeField]
    private float _buffedHitDamage;
    private float _currentHitDamage;
    public float HitDamage
    {
        get
        {
            return this._currentHitDamage;
        }
    }
    public override bool BuffTrap
    {
        set
        {
            if (value)
            {
                _currentHitDamage = _buffedHitDamage;
                StartCoroutine(BuffingTrapCoroutine());
            }
            else
            {
                _currentHitDamage = _hitDamage;
                StopAllCoroutines();
            }
        }
    }

    [SerializeField]
    private float _timeInterval;
    public float TimeInterval
    {
        get
        {
            return this._timeInterval;
        }
    }

    public override void Initialize(Vector3 position, TrapType trapType = TrapType.Booby)
    {
        this._trapType = trapType;
        transform.position = position;
        BuffTrap = false;
    }
}
