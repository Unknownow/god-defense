using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointService : MonoBehaviour
{
    private WaypointProperties _waypointProperties;

    private void Start()
    {
        _waypointProperties = gameObject.GetComponent<WaypointProperties>();
    }

    public Vector3 GetNextDestination()
    {
        return _waypointProperties.Target.position;
    }

    public int GetLaneIndex()
    {
        return _waypointProperties.LaneIndex;
    }
}
