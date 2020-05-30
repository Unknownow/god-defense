
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoobyTrapStatesController : TrapStatesController
{
    private Color _defaultColor;
    // private BoobyTrapVisualEffect _visual;

    protected override void Awake()
    {
        base.Awake();
        // _visual = gameObject.GetComponent<BoobyTrapVisualEffect>();
    }
    public override void BuffTrap()
    {
        if (_trapProperties.BuffTrap && !_buffable)
            return;
        base.BuffTrap();
        _visual.BuffTrap();
        // _defaultColor = transform.GetComponent<MeshRenderer>().materials[0].color;
        // transform.GetComponent<MeshRenderer>().materials[0].color = Color.red;
    }

    protected override void UnbuffTrap()
    {
        base.UnbuffTrap();
        _visual.UnbuffTrap();
        // transform.GetComponent<MeshRenderer>().material.color = _defaultColor;
    }
}
