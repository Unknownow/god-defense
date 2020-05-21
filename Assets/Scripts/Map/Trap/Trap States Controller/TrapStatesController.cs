using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapStatesController : MonoBehaviour
{
    protected TrapProperties _trapProperties;
    protected TrapEffect _trapCollision;

    protected virtual void Awake()
    {
        _trapProperties = gameObject.GetComponent<TrapProperties>();
        _trapCollision = gameObject.GetComponent<TrapEffect>();
    }
    public virtual void Initialize(Vector3 position)
    {
        _trapProperties.Init();
        transform.position = position;
        StopAllCoroutines();
        StartCoroutine(DestroyTrapCoroutine());
    }

    public virtual void BuffingTrap()
    {
        if (_trapProperties.BuffTrap)
            return;
        _trapProperties.BuffTrap = true;
        _trapCollision.ReapplyWhenBuffed();
        StartCoroutine(BuffingTrapCoroutine());
    }

    protected void UnbuffingTrap()
    {
        _trapProperties.BuffTrap = false;
    }

    protected IEnumerator BuffingTrapCoroutine()
    {
        yield return new WaitForSeconds(_trapProperties.BuffedDuration);
        UnbuffingTrap();
    }

    protected IEnumerator DestroyTrapCoroutine()
    {
        yield return new WaitForSeconds(_trapProperties.Duration);
        TrapFactory.DestroyTrap(_trapProperties.Type, this.gameObject);
    }
}
