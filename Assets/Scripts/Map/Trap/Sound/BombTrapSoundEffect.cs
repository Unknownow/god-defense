using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrapSoundEffect : MonoBehaviour, ITrapSoundEffect
{
    [SerializeField]
    private AudioSource _explosion;

    public void Explode()
    {
        _explosion.Play();
    }
}
