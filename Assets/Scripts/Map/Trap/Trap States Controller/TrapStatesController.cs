using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapStatesController : MonoBehaviour
{
    protected TrapProperties _trapProperties;
    protected TrapInteraction _trapCollision;
    protected ITrapVisualEffect _visual;
    protected ITrapSoundEffect _sound;
    protected bool _buffable;

    protected virtual void Awake()
    {
        _trapProperties = gameObject.GetComponent<TrapProperties>();
        _trapCollision = gameObject.GetComponent<TrapInteraction>();
        _visual = gameObject.GetComponent<ITrapVisualEffect>();
        _sound = gameObject.GetComponent<ITrapSoundEffect>();
    }
    public virtual void Initialize(Vector3 position)
    {
        _trapProperties.Init();
        _visual.Init();
        transform.position = position;
        _buffable = true;
        StopAllCoroutines();
        StartCoroutine(DestroyTrapCoroutine());
    }

    public virtual void BuffTrap()
    {
        if (_trapProperties.BuffTrap && !_buffable)
            return;
        _trapProperties.BuffTrap = true;
        _buffable = false;
        _trapCollision.ReapplyWhenBuffed();
        StartCoroutine(BuffingTrapCoroutine());
    }

    protected virtual void UnbuffTrap()
    {
        _trapProperties.BuffTrap = false;
        StartCoroutine(BuffTrapCooldown());
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

    protected IEnumerator BuffTrapCooldown()
    {
        yield return new WaitForSeconds(_trapProperties.BuffingCooldown);
        _buffable = true;
    }
}
