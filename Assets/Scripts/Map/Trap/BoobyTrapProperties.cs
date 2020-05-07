using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoobyTrapProperties : TrapProperties
{
    [SerializeField]
    private float _timeInterval;
    public float TimeInterval
    {
        get
        {
            return this._timeInterval;
        }
    }

    public new void Initialize(Vector3 position, TrapType trapType = TrapType.Booby)
    {
        this._trapType = trapType;
        transform.position = position;
    }
}
