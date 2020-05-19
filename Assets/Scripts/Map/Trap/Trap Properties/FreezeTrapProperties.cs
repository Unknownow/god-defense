using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTrapProperties : TrapProperties
{
    [Header("Freezing Trap")]
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
                _currentSlowPercentage = _slowPercentage;
                StopAllCoroutines();
            }
        }
        get
        {
            if (_currentSlowPercentage == _slowPercentage)
            {
                return false;
            }
            return true;
        }
    }

    public override void Initialize(Vector3 position, TrapType trapType = TrapType.Freeze)
    {
        base.Initialize(position, TrapType.Freeze);
    }
}
