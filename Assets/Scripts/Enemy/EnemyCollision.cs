using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private IEnemyMovement _enemyMovement;
    private EnemyProperties _enemyProperties;
    private LayerMask _trapLayerMask;

    private void Start()
    {
        _enemyMovement = gameObject.GetComponent<IEnemyMovement>();
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // if (other.transform.CompareTag("Waypoint"))
        // {
        //     WaypointProperties waypointProperties = other.GetComponent<WaypointProperties>();
        //     WaypointService waypointService = other.GetComponent<WaypointService>();
        //     // if (waypointProperties.Size != _enemyProperties.Size)
        //     //     return;
        //     if (_enemyProperties.LaneIndex == -1)
        //         _enemyProperties.LaneIndex = waypointProperties.LaneIndex;
        //     else if (_enemyProperties.LaneIndex != waypointProperties.LaneIndex)
        //         return;
        //     _enemyMovement.SetDestination(waypointService.GetNextDestination());
        // }
        // if (other.transform.CompareTag("Finish Line"))
        // {
        //     _enemyMovement.StopMoving();
        // }
    }

    private Collider[] CheckTrap()
    {
        Collider[] trapColliders = Physics.OverlapSphere(transform.position, _enemyProperties.TrapRadius, _trapLayerMask);
        return trapColliders;
    }
}
