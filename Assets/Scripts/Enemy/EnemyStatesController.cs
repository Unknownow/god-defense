using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatesController : MonoBehaviour
{
    private EnemyProperties _enemyProperties;
    private IEnemyMovement _enemyMovement;
    private IEnemyAttack _enemyAttack;


    private void Awake()
    {
        _enemyMovement = gameObject.GetComponent<IEnemyMovement>();
        _enemyAttack = gameObject.GetComponent<IEnemyAttack>();
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
    }

    private void Update()
    {
        // if (_enemyMovement.IsAtFinishLine)
        //     _enemyAttack.StartAttack();
        // else
        //     _enemyAttack.StopAttack();
    }

    public void Initialize(Vector3 position, int laneIndex)
    {
        _enemyProperties.Initialize(position, laneIndex);
        StopAllCoroutines();
        // Reset physical components:
        transform.rotation = Quaternion.identity;
        // freeze gravity, rotation and position
        Rigidbody enemyBody = gameObject.GetComponent<Rigidbody>();
        enemyBody.velocity = Vector3.zero;
        enemyBody.angularVelocity = Vector3.zero;
        enemyBody.useGravity = false;
        enemyBody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY;

        // check isTrigger on collider.
        gameObject.GetComponent<Collider>().isTrigger = true;

        // enable NavMeshAgent
        NavMeshAgent enemyAgent = gameObject.GetComponent<NavMeshAgent>();
        enemyAgent.enabled = true;
        enemyAgent.speed = _enemyProperties.MovementSpeed;
        enemyAgent.angularSpeed = _enemyProperties.AngularSpeed;
        enemyAgent.acceleration = _enemyProperties.Acceleration;
    }

    public void Die()
    {
        _enemyProperties.Die();

        // disable NavMeshAgent
        gameObject.GetComponent<NavMeshAgent>().enabled = false;

        // using gravity and unfreeze rotation, position.
        Rigidbody enemyBody = gameObject.GetComponent<Rigidbody>();
        enemyBody.useGravity = true;
        enemyBody.constraints = RigidbodyConstraints.None;

        // uncheck isTrigger on collider.
        gameObject.GetComponent<Collider>().isTrigger = false;

        StartCoroutine(DestroyEnemy(_enemyProperties.TimeBeforeDestroy));
    }

    private IEnumerator DestroyEnemy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        EnemyFactory.DestroyEnemy(_enemyProperties.Type, this.gameObject);
    }
}
