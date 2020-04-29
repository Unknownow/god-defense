using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private EnemyMovement _enemyMovement;
    private EnemyProperties _enemyProperties;

    private void Start()
    {
        _enemyMovement = gameObject.GetComponent<EnemyMovement>();
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Waypoint"))
        {
            WaypointProperties waypointProperties = other.GetComponent<WaypointProperties>();
            WaypointService waypointService = other.GetComponent<WaypointService>();
            if (waypointProperties.Size != _enemyProperties.Size)
                return;
            if (_enemyProperties.LaneIndex == -1)
                _enemyProperties.LaneIndex = waypointProperties.LaneIndex;
            else if (_enemyProperties.LaneIndex != waypointProperties.LaneIndex)
                return;
            _enemyMovement.SetDestination(waypointService.GetNextDestination());
        }
        if (other.transform.CompareTag("Finish Line"))
        {
            _enemyMovement.StopMoving();
        }
    }
}
