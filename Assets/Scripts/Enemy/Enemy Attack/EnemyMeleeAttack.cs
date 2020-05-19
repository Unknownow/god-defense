﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour, IEnemyAttack
{
    [SerializeField]
    private LayerMask _attackableLayer;
    [SerializeField]
    private EnemyProperties _enemyProperties;
    private float _timeBetweenAttack;


    private Collider[] _attackTarget;
    private IEnumerator _attackCoroutine;
    private bool _isAttacking;

    private void Start()
    {
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
        _timeBetweenAttack = 1.0f / _enemyProperties.AttackRate;
        _attackTarget = new Collider[1];
        _attackCoroutine = AttackCoroutine();
        _isAttacking = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("START ATTACKING");
            StartCoroutine(_attackCoroutine);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("STOP ATTACKING");
            StopCoroutine(_attackCoroutine);
        }
    }

    public void StartAttack()
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
            StartCoroutine(_attackCoroutine);
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
        int countTarget = Physics.OverlapSphereNonAlloc(transform.forward * _enemyProperties.AttackRange / 2 + transform.position, _enemyProperties.AttackRange / 2f, _attackTarget, _attackableLayer);
        if (countTarget > 0)
        {
            if (_attackTarget[0].transform.CompareTag("Tower"))
            {
                _attackTarget[0].transform.GetComponent<TowerService>().Hit(_enemyProperties.HitDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.forward * _enemyProperties.AttackRange / 2 + transform.position, _enemyProperties.AttackRange / 2f);
    }
}