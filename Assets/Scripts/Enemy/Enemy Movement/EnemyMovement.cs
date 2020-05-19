using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour, IEnemyMovement
{
    private EnemyProperties _enemyProperties;
    private NavMeshAgent _enemyAgent;
    // protected NavMeshObstacle _enemyObstacle;
    public Transform _currentTarget;
    private float _currentAvoidanceRadius;
    private bool _isAtFinishLine;
    public bool IsAtFinishLine
    {
        get
        {
            return this._isAtFinishLine;
        }
        set
        {
            _isAtFinishLine = value;
        }
    }

    protected void Awake()
    {
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
        _enemyAgent = gameObject.GetComponent<NavMeshAgent>();
        _currentAvoidanceRadius = _enemyAgent.radius;
        _isAtFinishLine = false;
    }

    private void Update()
    {
        if (!_enemyProperties.IsAlive || _isAtFinishLine)
            return;
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
            if (distanceToTarget <= _enemyProperties.AttackRange - Random.Range(0, 0.05f))
            {
                _isAtFinishLine = true;
                StopMoving();
            }
        }
    }

    public void StartMoving(Transform target)
    {
        IsAtFinishLine = false;
        _enemyAgent.radius = _currentAvoidanceRadius;
        _currentTarget = target;
        _enemyAgent.SetDestination(target.position);
        _enemyAgent.isStopped = false;
    }

    public void StopMoving()
    {
        _enemyAgent.isStopped = true;
        _enemyAgent.radius = 0;
    }

    public void SlowDown(float slowPercentage)
    {
        _enemyAgent.speed = _enemyProperties.MovementSpeed * (1.0f - slowPercentage / 100);
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
