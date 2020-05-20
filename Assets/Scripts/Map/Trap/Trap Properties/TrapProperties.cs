using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrapProperties : MonoBehaviour
{
    [Header("Stats")]
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
    private float _buffedDuration;
    public float BuffedDuration
    {
        get
        {
            return this._buffedDuration;
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

    [SerializeField]
    protected TrapType _trapType;
    public TrapType Type
    {
        get
        {
            return this._trapType;
        }
    }

    public abstract bool BuffTrap { get; set; }

    protected void Awake()
    {
        Init();
    }

    // public virtual void Initialize(Vector3 position, TrapType trapType)
    // {
    //     this._trapType = trapType;
    //     transform.position = position;
    //     BuffTrap = false;
    //     StopAllCoroutines();
    //     StartCoroutine(DestroyTrapCoroutine());
    // }

    public virtual void Init()
    {
        BuffTrap = false;
    }

    // public void Destroy()
    // {
    //     TrapFactory.DestroyTrap(_trapType, this.gameObject);
    // }

    // protected IEnumerator BuffingTrapCoroutine()
    // {
    //     yield return new WaitForSeconds(_buffedDuration);
    //     BuffTrap = false;
    // }

    // protected virtual IEnumerator DestroyTrapCoroutine()
    // {
    //     yield return new WaitForSeconds(_duration);
    //     TrapFactory.DestroyTrap(_trapType, this.gameObject);
    // }
}
