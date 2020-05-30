using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrapVisualEffect : MonoBehaviour, ITrapVisualEffect
{
    [SerializeField]
    private ParticleSystem _explosion;

    private TrapProperties _trapProperties;
    private Color _defaultColor;

    private void Awake()
    {
        _trapProperties = gameObject.GetComponent<TrapProperties>();
        _defaultColor = transform.GetComponent<MeshRenderer>().material.color;
    }

    public void Init()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.GetComponent<MeshRenderer>().material.color = _defaultColor;
        _explosion.transform.localScale = new Vector3(.5f, .5f, .5f);
    }

    public void Explode()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        _explosion.Play();
    }

    public void BuffTrap()
    {
        transform.localScale *= ((BombTrapProperties)_trapProperties).BuffedSizeMultiply;
        _defaultColor = transform.GetComponent<MeshRenderer>().material.color;
        transform.GetComponent<MeshRenderer>().material.color = Color.red;
        _explosion.transform.localScale *= ((BombTrapProperties)_trapProperties).BuffedSizeMultiply;
    }

    public void UnbuffTrap()
    {
        transform.localScale /= ((BombTrapProperties)_trapProperties).BuffedSizeMultiply;
        transform.GetComponent<MeshRenderer>().material.color = _defaultColor;
        _explosion.transform.localScale /= ((BombTrapProperties)_trapProperties).BuffedSizeMultiply;
    }
}
