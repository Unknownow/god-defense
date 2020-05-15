using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingTrapProperties : TrapProperties
{
    [SerializeField]
    private float _slowPercentage;
    [SerializeField]
    private float _buffedSlowPercentage;
    private float _currentSlowPercentage;
    public float SlowPercentage
    {
        get
        {
            return this._currentSlowPercentage;
        }
    }
    public override bool BuffTrap
    {
        set
        {
            if (value)
            {
                _currentSlowPercentage = _buffedSlowPercentage;
                StartCoroutine(BuffingTrapCoroutine());
            }
            else
            {
                _currentSlowPercentage = SlowPercentage;
                StopAllCoroutines();
            }
        }
    }

    public override void Initialize(Vector3 position, TrapType trapType = TrapType.Freeze)
    {
        this._trapType = trapType;
        transform.position = position;
        BuffTrap = false;
    }
}
