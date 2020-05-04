using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapProperties : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private float _hitDamage;
    public float HitDamage
    {
        get
        {
            return this._hitDamage;
        }
    }
    [SerializeField]
    private float _duration;
    public float Duration
    {
        get
        {
            return this.Duration;
        }
    }
}
