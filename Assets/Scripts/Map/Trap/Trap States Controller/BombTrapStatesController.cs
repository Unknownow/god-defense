using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrapStatesController : TrapStatesController
{
    private Color _defaultColor;
    // private BombTrapVisualEffect _visual;
    // private BombTrapSoundEffect _audio;
    protected override void Awake()
    {
        base.Awake();
        // _visual = gameObject.GetComponent<BombTrapVisualEffect>();
        // _audio = gameObject.GetComponent<BombTrapSoundEffect>();
    }

    public override void Initialize(Vector3 position)
    {
        _trapProperties.Init();
        _visual.Init();
        _trapProperties.BuffTrap = false;
        transform.position = position;
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        StopAllCoroutines();
    }

    public override void BuffTrap()
    {
        if (_trapProperties.BuffTrap && !_buffable)
            return;
        base.BuffTrap();
        // transform.localScale *= ((BombTrapProperties)_trapProperties).BuffedSizeMultiply;
        // _defaultColor = transform.GetComponent<MeshRenderer>().material.color;
        // transform.GetComponent<MeshRenderer>().material.color = Color.red;
        _visual.BuffTrap();
    }

    protected override void UnbuffTrap()
    {
        base.UnbuffTrap();
        // transform.localScale /= ((BombTrapProperties)_trapProperties).BuffedSizeMultiply;
        // transform.GetComponent<MeshRenderer>().material.color = _defaultColor;
        _visual.UnbuffTrap();
    }

    public void Detonate()
    {
        if (((BombTrapProperties)_trapProperties).IsDetonated)
            return;
        ((BombTrapVisualEffect)_visual).Explode();
        ((BombTrapSoundEffect)_sound).Explode();
        ((BombTrapProperties)_trapProperties).Detonate();
        gameObject.GetComponent<Collider>().enabled = false;
        StartCoroutine(DestroyTrapCoroutine());
    }
}
