using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoobyTrapVisualEffect : MonoBehaviour, ITrapVisualEffect
{
    private TrapProperties _trapProperties;
    private Color _defaultColor;

    private void Awake()
    {
        _trapProperties = gameObject.GetComponent<TrapProperties>();
        _defaultColor = transform.GetComponent<MeshRenderer>().materials[0].color;
    }

    public void Init()
    {
        transform.GetComponent<MeshRenderer>().material.color = _defaultColor;
    }

    public void BuffTrap()
    {
        _defaultColor = transform.GetComponent<MeshRenderer>().materials[0].color;
        transform.GetComponent<MeshRenderer>().materials[0].color = Color.red;
    }

    public void UnbuffTrap()
    {
        transform.GetComponent<MeshRenderer>().material.color = _defaultColor;
    }
}
