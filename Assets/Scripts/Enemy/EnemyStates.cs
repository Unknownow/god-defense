using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour
{
    private NavMeshAgent _enemyAgent;
    private IEnemyMovement _enemyMovement;
    private IEnemyAttack _enemyAttack;


    private void Start()
    {
        _enemyAgent = gameObject.GetComponent<NavMeshAgent>();
        _enemyMovement = gameObject.GetComponent<IEnemyMovement>();
        _enemyAttack = gameObject.GetComponent<IEnemyAttack>();
    }

    private void Update()
    {
        if (_enemyAgent.isStopped)
        {
            _enemyAttack.StartAttack();
        }
        else
        {
            _enemyAttack.StopAttack();
        }
    }
}
