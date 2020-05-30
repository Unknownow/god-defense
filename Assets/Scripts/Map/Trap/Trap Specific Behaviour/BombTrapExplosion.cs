using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrapExplosion : MonoBehaviour
{
    [SerializeField]
    private LayerMask _enemyLayermask;
    private BombTrapProperties _trapProperties;
    private BombTrapStatesController _trapStates;

    private void Awake()
    {
        _trapProperties = gameObject.GetComponent<BombTrapProperties>();
        _trapStates = gameObject.GetComponent<BombTrapStatesController>();
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
        StartCoroutine(WaitBeforeDetonation());
        _trapStates.Detonate();
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
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void Initialize()
    {
    }
}
