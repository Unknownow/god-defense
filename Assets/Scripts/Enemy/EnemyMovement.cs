using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private EnemyProperties _enemyProperties;

    private NavMeshAgent _enemyAgent;
    // private NavMeshObstacle _enemyObstacle;

    private void Awake()
    {
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
        _enemyAgent = gameObject.GetComponent<NavMeshAgent>();
        // _enemyObstacle = gameObject.GetComponent<NavMeshObstacle>();
        _enemyAgent.speed = _enemyProperties.MovementSpeed;
        _enemyAgent.angularSpeed = _enemyProperties.AngularSpeed;
        _enemyAgent.acceleration = _enemyProperties.Acceleration;
    }

    public void SetDestination(Vector3 target)
    {
        _enemyAgent.SetDestination(target);
        // _enemyObstacle.enabled = false;
        _enemyAgent.enabled = true;
    }

    public void StopMoving()
    {
        // _enemyAgent.isStopped = true;
        _enemyAgent.enabled = false;
        // _enemyObstacle.enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}
