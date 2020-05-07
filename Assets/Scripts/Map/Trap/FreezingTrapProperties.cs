using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingTrapProperties : TrapProperties
{
    [SerializeField]
    private float _slowPercentage;
    public float SlowPercentage
    {
        get
        {
            return this._slowPercentage;
        }
    }

    public new void Initialize(Vector3 position, TrapType trapType = TrapType.Freeze)
    {
        this._trapType = trapType;
        transform.position = position;
    }
}
