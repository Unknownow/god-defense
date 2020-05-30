
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoobyTrapStatesController : TrapStatesController
{
    private Color _defaultColor;
    public override void BuffingTrap()
    {
        if (_trapProperties.BuffTrap)
            return;
        base.BuffingTrap();
        _defaultColor = transform.GetComponent<MeshRenderer>().materials[0].color;
        transform.GetComponent<MeshRenderer>().materials[0].color = Color.red;
    }

    protected override void UnbuffingTrap()
    {
        base.UnbuffingTrap();
        transform.GetComponent<MeshRenderer>().material.color = _defaultColor;
    }
}
