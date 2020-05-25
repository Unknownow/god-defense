
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTrapStatesController : TrapStatesController
{
    private Color _defaultColor;
    public override void BuffingTrap()
    {
        if (_trapProperties.BuffTrap)
            return;
        base.BuffingTrap();
        _defaultColor = transform.GetComponent<MeshRenderer>().materials[1].color;
        transform.GetComponent<MeshRenderer>().materials[1].color = new Color(0, 151, 230);
    }

    protected override void UnbuffingTrap()
    {
        base.UnbuffingTrap();
        transform.GetComponent<MeshRenderer>().material.color = _defaultColor;
    }
}
