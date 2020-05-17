using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrapExplosion : MonoBehaviour
{
    [SerializeField]
    private LayerMask _enemyLayermask;
    private BombTrapProperties _trapProperties;
    private bool _isDetonated;

    private void Awake()
    {
        _trapProperties = gameObject.GetComponent<BombTrapProperties>();
        Initialize();
    }
    private void OnEnable()
    {
        Initialize();
    }

    private void OnPlaced()
    {
        Initialize();
    }

    public void DetonateBomb()
    {
        if (_isDetonated)
            return;
        _isDetonated = true;
        gameObject.GetComponent<Collider>().enabled = false;
        StartCoroutine(WaitBeforeDetonation());
        StartCoroutine(DestroyTrap(_trapProperties.Duration));
    }

    private IEnumerator DestroyTrap(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _trapProperties.Destroy();
    }

    private IEnumerator WaitBeforeDetonation()
    {
        yield return new WaitForSeconds(_trapProperties.TimeBeforeDetonation);
        Collider[] enemies = Physics.OverlapSphere(_trapProperties.ExplosionCenterPosition, _trapProperties.ExplosionRadius, _enemyLayermask);
        foreach (Collider enemy in enemies)
        {
            bool isDead = enemy.transform.GetComponent<EnemyTrapInteraction>().StepOnBombTrap(_trapProperties.HitDamage);
            if (isDead)
                enemy.transform.GetComponent<EnemyPhysics>().AddExplosionForce(_trapProperties.ForceMagnitude, _trapProperties.ExplosionCenterPosition, _trapProperties.ExplosionRadius);
        }
    }

    private void Initialize()
    {
        _isDetonated = false;
    }
}
