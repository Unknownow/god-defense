using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointProperties : MonoBehaviour
{
    // [SerializeField]
    // private int _size;
    // public int Size
    // {
    //     get
    //     {
    //         return this._size;
    //     }
    // }

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
