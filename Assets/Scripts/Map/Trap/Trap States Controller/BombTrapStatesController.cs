using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrapStatesController : TrapStatesController
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize(Vector3 position)
    {
        _trapProperties.Init();
        _trapProperties.BuffTrap = false;
        transform.position = position;
        gameObject.GetComponent<Collider>().enabled = true;
        StopAllCoroutines();
    }

    public void Detonate()
    {
        if (((BombTrapProperties)_trapProperties).IsDetonated)
            return;
        ((BombTrapProperties)_trapProperties).Detonate();
        gameObject.GetComponent<Collider>().enabled = false;
        StartCoroutine(DestroyTrapCoroutine());
    }
}
