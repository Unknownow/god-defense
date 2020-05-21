using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointProperties : MonoBehaviour
{
    [SerializeField]
    private bool _isLastWaypoint;
    public bool IsLastWaypoint
    {
        get
        {
            return this._isLastWaypoint;
        }
    }

    [SerializeField]
    private int _laneIndex;
    public int LaneIndex
    {
        get
        {
            return this._laneIndex;
        }
    }

    [SerializeField]
    private Transform _nextTarget;
    public Transform NextTarget
    {
        get
        {
            return this._nextTarget;
        }
    }


}
