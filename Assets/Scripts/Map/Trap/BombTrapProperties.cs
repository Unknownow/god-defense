using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrapProperties : TrapProperties
{
    [SerializeField]
    private float _hitDamage;
    [SerializeField]
    private float _buffedHitDamage;
    private float _currentHitDamage;
    public float HitDamage
    {
        get
        {
            return this._currentHitDamage;
        }
    }

    [SerializeField]
    private float _explosionRadius;
    [SerializeField]
    private float _buffedExplosionRadius;
    [SerializeField]
    private Transform _explosionCenter;
    private float _currentExplosionRadius;
    public float ExplosionRadius
    {
        get
        {
            return this._currentExplosionRadius;
        }
    }
    public Vector3 ExplosionCenterPosition
    {
        get
        {
            return _explosionCenter.position;
        }
    }
    public override bool BuffTrap
    {
        set
        {
            if (value)
            {
                _currentHitDamage = _buffedHitDamage;
                _currentExplosionRadius = _buffedExplosionRadius;
                StartCoroutine(BuffingTrapCoroutine());
            }
            else
            {
                _currentHitDamage = _hitDamage;
                _currentExplosionRadius = _explosionRadius;
                StopAllCoroutines();
            }
        }
    }

    public override void Initialize(Vector3 position, TrapType trapType = TrapType.Bomb)
    {
        this._trapType = trapType;
        transform.position = position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_explosionCenter.position, _explosionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_explosionCenter.position, _buffedExplosionRadius);
    }
}
