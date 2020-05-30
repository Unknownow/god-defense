using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrapVisualEffect : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _explosion;

    private TrapProperties _trapProperties;

    private void Start()
    {
        _trapProperties = gameObject.GetComponent<TrapProperties>();
    }

    public void Explode()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        _explosion.Play();
    }

    public void BuffTrap()
    {
        _explosion.transform.localScale *= ((BombTrapProperties)_trapProperties).BuffedSizeMultiply;
    }

    public void UnbuffTrap()
    {
        _explosion.transform.localScale /= ((BombTrapProperties)_trapProperties).BuffedSizeMultiply;
    }
}
