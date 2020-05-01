using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    protected EnemyProperties _enemyProperties;
    protected NavMeshAgent _enemyAgent;
    // protected NavMeshObstacle _enemyObstacle;
    [SerializeField]
    protected Transform _currentTarget;

    protected void Awake()
    {
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
        _enemyAgent = gameObject.GetComponent<NavMeshAgent>();
        // _enemyObstacle = gameObject.GetComponent<NavMeshObstacle>();
        _enemyAgent.speed = _enemyProperties.MovementSpeed;
        _enemyAgent.angularSpeed = _enemyProperties.AngularSpeed;
        _enemyAgent.acceleration = _enemyProperties.Acceleration;
    }

    private void Update()
    {
        float distanceToTarget = Utils.DistanceInXZ(transform.position, _currentTarget.position);
        if (_currentTarget.CompareTag("Waypoint"))
        {
            if (distanceToTarget <= 0.05f)
            {
                Transform nextTarget = _currentTarget.GetComponent<WaypointService>().GetNextDestination();
                SetDestination(nextTarget);
            }
        }
        else if (_currentTarget.CompareTag("Finish Line"))
        {
            if (distanceToTarget <= _enemyProperties.AttackRange + 0.05f)
                StopMoving();
        }
    }

    public void SetDestination(Vector3 target)
    {
        _enemyAgent.SetDestination(target);
        // _enemyObstacle.enabled = false;
        _enemyAgent.enabled = true;
    }

    public void SetDestination(Transform target)
    {
        _currentTarget = target;
        _enemyAgent.SetDestination(target.position);
        // _enemyObstacle.enabled = false;
        _enemyAgent.enabled = true;
    }

    public void StopMoving()
    {
        // _enemyAgent.isStopped = true;
        _enemyAgent.enabled = false;
        // _enemyObstacle.enabled = true;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * .3f);
    }

}
