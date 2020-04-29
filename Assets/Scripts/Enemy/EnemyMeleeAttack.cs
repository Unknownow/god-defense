using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    [SerializeField]
    private Transform _weapon;
    [SerializeField]
    private LayerMask _attackableLayer;
    private EnemyProperties _enemyProperties;
    private float _timeBetweenAttack;

    private void Start()
    {
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
        _timeBetweenAttack = 1.0f / _enemyProperties.AttackRate;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("START ATTACKING");
            StartCoroutine(AttackCoroutine());
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("STOP ATTACKING");
            StopCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine()
    {
        Attack();
        yield return new WaitForSeconds(_timeBetweenAttack);
    }

    private void Attack()
    {
        RaycastHit hit;
        bool isHit = Physics.SphereCast(_weapon.position, _enemyProperties.AttackRange / 2f, transform.forward, out hit, _enemyProperties.AttackRange, _attackableLayer);
        if (isHit)
        {
            Debug.Log("HIT!");
            Debug.Log(hit.transform.name);
        }
    }
}
