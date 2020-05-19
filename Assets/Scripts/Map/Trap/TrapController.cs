using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    protected TrapProperties _trapProperties;
    protected Vector3 truePos;

    protected virtual void OnEnable()
    {
        Initialize();
    }

    protected virtual void Awake()
    {
        _trapProperties = gameObject.GetComponent<TrapProperties>();
        Initialize();
    }

    public virtual void OnPlaced()
    {
        Initialize();
    }

    public TrapProperties GetProperties()
    {
        return this._trapProperties;
    }

    protected virtual void Initialize()
    {
    }
}
