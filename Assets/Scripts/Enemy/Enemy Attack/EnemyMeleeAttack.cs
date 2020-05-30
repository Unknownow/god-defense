using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour, IEnemyAttack
{
    [SerializeField]
    private LayerMask _attackableLayer;
    private EnemyProperties _enemyProperties;
    private float _timeBetweenAttack;


    private Collider[] _attackTarget;
    private Coroutine _attackCoroutine;
    private bool _isAttacking;

    private void Start()
    {
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
        _timeBetweenAttack = 1.0f / _enemyProperties.AttackRate;
        _attackTarget = new Collider[1];
        _isAttacking = false;
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     Debug.Log("START ATTACKING");
        //     _attackCoroutine = StartCoroutine(AttackCoroutine());
        // }
        // else if (Input.GetKeyDown(KeyCode.S))
        // {
        //     Debug.Log("STOP ATTACKING");
        //     StopCoroutine(_attackCoroutine);
        // }
    }

    public void StartAttack()
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
            _attackCoroutine = StartCoroutine(AttackCoroutine());
        }
    }

    public void StopAttack()
    {
        if (_isAttacking)
        {
            _isAttacking = true;
            StopCoroutine(_attackCoroutine);
        }
    }

    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(_timeBetweenAttack);
        }
    }

    private void Attack()
    {
        int countTarget = Physics.OverlapSphereNonAlloc(transform.forward * _enemyProperties.AttackRange / 2 + transform.position + _enemyProperties.AttackHeight * Vector3.up, _enemyProperties.AttackRange / 2f, _attackTarget, _attackableLayer);
        if (countTarget > 0)
        {
            if (_attackTarget[0].transform.CompareTag("Tower"))
            {
                _attackTarget[0].transform.GetComponent<TowerHitPointManager>().Hit(_enemyProperties.HitDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        EnemyProperties enemyProperties = gameObject.GetComponent<EnemyProperties>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.forward * enemyProperties.AttackRange / 2 + transform.position + enemyProperties.AttackHeight * Vector3.up, enemyProperties.AttackRange / 2f);
    }
}
