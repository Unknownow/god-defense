﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointService : MonoBehaviour
{
    private WaypointProperties _waypointProperties;

    private void Start()
    {
        _waypointProperties = gameObject.GetComponent<WaypointProperties>();
    }

    public Transform GetNextDestination()
    {
        if (_waypointProperties.IsLastWaypoint)
            return null;
        return _waypointProperties.NextTarget;
    }

    public int GetLaneIndex()
    {
        return _waypointProperties.LaneIndex;
    }
}
