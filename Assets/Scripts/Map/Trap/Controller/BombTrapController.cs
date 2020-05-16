using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrapController : TrapController
{
    [SerializeField]
    private LayerMask _enemyLayermask;
    private BombTrapProperties TrapProperties
    {
        get
        {
            return (BombTrapProperties)_trapProperties;
        }
    }

    private bool _isDetonated;

    protected override void Awake()
    {
        _trapProperties = gameObject.GetComponent<BombTrapProperties>();
        Initialize();
    }
    protected override void OnEnable()
    {
        Initialize();
    }

    public override void OnPlaced()
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

    protected override IEnumerator DestroyTrap(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _trapProperties.Destroy();
    }

    private IEnumerator WaitBeforeDetonation()
    {
        yield return new WaitForSeconds(TrapProperties.TimeBeforeDetonation);
        Collider[] enemies = Physics.OverlapSphere(TrapProperties.ExplosionCenterPosition, TrapProperties.ExplosionRadius, _enemyLayermask);
        foreach (Collider enemy in enemies)
        {
            bool isDead = enemy.transform.GetComponent<EnemyTrapInteraction>().StepOnBombTrap(TrapProperties.HitDamage);
            if (isDead)
                enemy.transform.GetComponent<EnemyPhysics>().AddExplosionForce(TrapProperties.ForceMagnitude, TrapProperties.ExplosionCenterPosition, TrapProperties.ExplosionRadius);
        }
    }

    protected override void Initialize()
    {
        _isDetonated = false;
    }
}
