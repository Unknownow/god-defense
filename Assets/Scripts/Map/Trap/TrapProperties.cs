using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrapProperties : MonoBehaviour
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
            return this._duration;
        }
    }
    [SerializeField]
    private float _cooldown;
    public float Cooldown
    {
        get
        {
            return this._cooldown;
        }
    }

    protected TrapType _trapType;
    public TrapType Type
    {
        get
        {
            return this._trapType;
        }
    }

    public void Initialize(Vector3 position, TrapType trapType)
    {
        this._trapType = trapType;
        transform.position = position;
    }

    public void Destroy()
    {
        TrapFactory.DestroyTrap(_trapType, this.gameObject);
    }
}
