using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour
{
    private EnemyProperties _enemyProperties;
    private NavMeshAgent _enemyAgent;
    private IEnemyMovement _enemyMovement;
    private IEnemyAttack _enemyAttack;


    private void Start()
    {
        _enemyAgent = gameObject.GetComponent<NavMeshAgent>();
        _enemyMovement = gameObject.GetComponent<IEnemyMovement>();
        _enemyAttack = gameObject.GetComponent<IEnemyAttack>();
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
    }

    private void Update()
    {
        // if (_enemyAgent.isStopped)
        // {
        //     _enemyAttack.StartAttack();
        // }
        // else
        // {
        //     _enemyAttack.StopAttack();
        // }
    }

    public void OnEnemyDie()
    {
        // disable NavMeshAgent
        _enemyAgent.enabled = false;

        // using gravity and unfreeze rotation, position.
        Rigidbody enemyBody = gameObject.GetComponent<Rigidbody>();
        enemyBody.useGravity = true;
        enemyBody.constraints = RigidbodyConstraints.None;

        // uncheck isTrigger on collider.
        gameObject.GetComponent<Collider>().isTrigger = false;

        // start countdown destroy enemy
        StartCoroutine(DestroyEnemy(_enemyProperties.TimeBeforeDestroy));
    }

    IEnumerator DestroyEnemy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _enemyProperties.Destroy();
    }
}
