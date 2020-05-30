
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTrapStatesController : TrapStatesController
{
    private Color _defaultColor;
    public override void BuffTrap()
    {
        if (_trapProperties.BuffTrap)
            return;
        base.BuffTrap();
        _defaultColor = transform.GetComponent<MeshRenderer>().materials[1].color;
        transform.GetComponent<MeshRenderer>().materials[1].color = new Color(0, 151, 230);
    }

    protected override void UnbuffTrap()
    {
        base.UnbuffTrap();
        transform.GetComponent<MeshRenderer>().material.color = _defaultColor;
    }
}
