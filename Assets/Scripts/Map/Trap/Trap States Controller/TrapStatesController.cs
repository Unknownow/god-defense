using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapStatesController : MonoBehaviour
{
    protected TrapProperties _trapProperties;
    protected TrapInteraction _trapCollision;

    protected virtual void Awake()
    {
        _trapProperties = gameObject.GetComponent<TrapProperties>();
        _trapCollision = gameObject.GetComponent<TrapInteraction>();
    }
    public virtual void Initialize(Vector3 position)
    {
        _trapProperties.Init();
        transform.position = position;
        StopAllCoroutines();
        StartCoroutine(DestroyTrapCoroutine());
    }

    public virtual void BuffTrap()
    {
        if (_trapProperties.BuffTrap)
            return;
        _trapProperties.BuffTrap = true;
        _trapCollision.ReapplyWhenBuffed();
        StartCoroutine(BuffingTrapCoroutine());
    }

    protected virtual void UnbuffTrap()
    {
        _trapProperties.BuffTrap = false;
    }

    protected IEnumerator BuffingTrapCoroutine()
    {
        yield return new WaitForSeconds(_trapProperties.BuffedDuration);
        UnbuffTrap();
    }

    protected IEnumerator DestroyTrapCoroutine()
    {
        yield return new WaitForSeconds(_trapProperties.Duration);
        TrapFactory.DestroyTrap(_trapProperties.Type, this.gameObject);
    }
}
