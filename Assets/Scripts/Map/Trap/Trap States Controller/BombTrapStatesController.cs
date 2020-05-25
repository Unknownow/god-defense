using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrapStatesController : TrapStatesController
{
    private Color _defaultColor;
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

    public override void BuffingTrap()
    {
        if (_trapProperties.BuffTrap)
            return;
        base.BuffingTrap();
        transform.localScale *= ((BombTrapProperties)_trapProperties).BuffedSizeMultiply;
        _defaultColor = transform.GetComponent<MeshRenderer>().material.color;
        transform.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    protected override void UnbuffingTrap()
    {
        base.UnbuffingTrap();
        transform.localScale /= ((BombTrapProperties)_trapProperties).BuffedSizeMultiply;
        transform.GetComponent<MeshRenderer>().material.color = _defaultColor;
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
