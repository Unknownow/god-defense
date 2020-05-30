
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTrapStatesController : TrapStatesController
{
    private Color _defaultColor;
    // private FreezeTrapVisualEffect _visual;

    protected override void Awake()
    {
        base.Awake();
        // _visual = gameObject.GetComponent<FreezeTrapVisualEffect>();
    }

    public override void BuffTrap()
    {
        if (_trapProperties.BuffTrap && !_buffable)
            return;
        base.BuffTrap();
        _visual.BuffTrap();
        // _defaultColor = transform.GetComponent<MeshRenderer>().materials[1].color;
        // transform.GetComponent<MeshRenderer>().materials[1].color = new Color(0, 151, 230);
    }

    protected override void UnbuffTrap()
    {
        base.UnbuffTrap();
        _visual.UnbuffTrap();
        // transform.GetComponent<MeshRenderer>().material.color = _defaultColor;
    }
}
