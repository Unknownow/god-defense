
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoobyTrapStatesController : TrapStatesController
{
    private Color _defaultColor;
    public override void BuffTrap()
    {
        if (_trapProperties.BuffTrap)
            return;
        base.BuffTrap();
        _defaultColor = transform.GetComponent<MeshRenderer>().materials[0].color;
        transform.GetComponent<MeshRenderer>().materials[0].color = Color.red;
    }

    protected override void UnbuffTrap()
    {
        base.UnbuffTrap();
        transform.GetComponent<MeshRenderer>().material.color = _defaultColor;
    }
}
