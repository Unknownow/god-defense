using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrapProperties : TrapProperties
{
    [Header("Bomb Trap")]
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
    private float _timeBeforeDetonation;
    public float TimeBeforeDetonation
    {
        get
        {
            return this._timeBeforeDetonation;
        }
    }

    [Header("Explosion")]
    [SerializeField]
    private Transform _explosionCenter;
    public Vector3 ExplosionCenterPosition
    {
        get
        {
            return _explosionCenter.position;
        }
    }
    [SerializeField]
    private float _explosionRadius;
    [SerializeField]
    private float _buffedExplosionRadius;
    private float _currentExplosionRadius;
    public float ExplosionRadius
    {
        get
        {
            return this._currentExplosionRadius;
        }
    }
    [Header("Force Apply")]
    [SerializeField]
    private float _forceMagnitude;
    [SerializeField]
    private float _buffedForceMagnitude;
    private float _currentForceMagnitude;
    public float ForceMagnitude
    {
        get
        {
            return this._currentForceMagnitude;
        }
    }
    [SerializeField]
    private float _buffedSizeMultiply;
    public float BuffedSizeMultiply
    {
        get
        {
            return this._buffedSizeMultiply;
        }
    }
    private bool _isDetonated;
    public bool IsDetonated
    {
        get
        {
            return this._isDetonated;
        }
    }

    public override bool BuffTrap
    {
        set
        {
            if (value && !BuffTrap)
            {
                _currentHitDamage = _buffedHitDamage;
                _currentExplosionRadius = _buffedExplosionRadius;
                _currentForceMagnitude = _buffedForceMagnitude;
                // StartCoroutine(BuffingTrapCoroutine());
            }
            else if (!value)
            {
                _currentHitDamage = _hitDamage;
                _currentExplosionRadius = _explosionRadius;
                _currentForceMagnitude = _forceMagnitude;
                // StopAllCoroutines();
            }
        }
        get
        {
            if (_currentHitDamage == _hitDamage)
            {
                return false;
            }
            return true;
        }
    }

    // public override void Initialize(Vector3 position, TrapType trapType = TrapType.Bomb)
    // {
    //     this._trapType = trapType;
    //     transform.position = position;
    //     BuffTrap = false;
    //     gameObject.GetComponent<Collider>().enabled = true;
    //     _isDetonated = false;
    //     StopAllCoroutines();
    // }

    public override void Init()
    {
        base.Init();
        _isDetonated = false;
    }

    // public void Detonate()
    // {
    //     if (_isDetonated)
    //         return;
    //     _isDetonated = true;
    //     gameObject.GetComponent<Collider>().enabled = false;
    //     StartCoroutine(DestroyBomb());
    // }

    public void Detonate()
    {
        if (_isDetonated)
            return;
        _isDetonated = true;
    }

    // private IEnumerator DestroyBomb()
    // {
    //     yield return new WaitForSeconds(_timeBeforeDetonation);
    //     TrapFactory.DestroyTrap(_trapType, gameObject);
    // }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_explosionCenter.position, _explosionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_explosionCenter.position, _buffedExplosionRadius);
    }
}
