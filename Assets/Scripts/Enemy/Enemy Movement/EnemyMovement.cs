using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour, IEnemyMovement
{
    protected EnemyProperties _enemyProperties;
    protected NavMeshAgent _enemyAgent;
    // protected NavMeshObstacle _enemyObstacle;
    protected Transform _currentTarget;
    protected float _currentAvoidanceRadius;

    protected void Awake()
    {
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
        _enemyAgent = gameObject.GetComponent<NavMeshAgent>();
        // _enemyObstacle = gameObject.GetComponent<NavMeshObstacle>();
        _enemyAgent.speed = _enemyProperties.MovementSpeed;
        _enemyAgent.angularSpeed = _enemyProperties.AngularSpeed;
        _enemyAgent.acceleration = _enemyProperties.Acceleration;
        _currentAvoidanceRadius = _enemyAgent.radius;
    }

    private void Update()
    {
        float distanceToTarget = Utils.DistanceInXZ(transform.position, _currentTarget.position);
        if (_currentTarget.CompareTag("Waypoint"))
        {
            if (distanceToTarget <= Random.Range(0.05f, 0.1f))
            {
                Transform nextTarget = _currentTarget.GetComponent<WaypointService>().GetNextDestination();
                StartMoving(nextTarget);
            }
        }
        else if (_currentTarget.CompareTag("Finish Line"))
        {
            if (distanceToTarget <= _enemyProperties.AttackRange + Random.Range(0, 0.05f))
                StopMoving();
        }
    }

    public void SetDestination(Vector3 target)
    {
        _enemyAgent.SetDestination(target);
        // _enemyObstacle.enabled = false;
        _enemyAgent.enabled = true;
    }

    public void StartMoving(Transform target)
    {
        _enemyAgent.radius = _currentAvoidanceRadius;
        _currentTarget = target;
        _enemyAgent.SetDestination(target.position);
        // _enemyObstacle.enabled = false;
        // _enemyAgent.enabled = true;
        _enemyAgent.isStopped = false;
    }

    public void StopMoving()
    {
        _enemyAgent.isStopped = true;
        _enemyAgent.radius = 0;
        // _enemyAgent.enabled = false;
        // _enemyObstacle.enabled = true;
    }

    public void SlowDown(float slowPercentage)
    {
        _enemyAgent.speed  = _enemyProperties.MovementSpeed *  (1.0f - slowPercentage / 100);
        _enemyAgent.angularSpeed = _enemyProperties.AngularSpeed * (1.0f - slowPercentage / 100);
        _enemyAgent.acceleration = _enemyProperties.Acceleration / (1.01f - slowPercentage / 100);
    }

    public void BackToNormalSpeed()
    {
        // _currentSlowPercentage = 0;
        _enemyAgent.speed = _enemyProperties.MovementSpeed;
        _enemyAgent.angularSpeed = _enemyProperties.AngularSpeed;
        _enemyAgent.acceleration = _enemyProperties.Acceleration;
    }


    protected void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * .3f);
    }


}
