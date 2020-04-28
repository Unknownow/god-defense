using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerProperties : MonoBehaviour
{
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
    private Direction _spawnerDirection;
    public Vector3 SpawnerDirection
    {
        get
        {
            Vector3 dir = Vector3.zero;
            switch (this._spawnerDirection)
            {
                case Direction.North:
                    dir = -Vector3.forward;
                    break;
                case Direction.South:
                    dir = Vector3.forward;
                    break;
                case Direction.East:
                    dir = Vector3.right;
                    break;
                case Direction.West:
                    dir = -Vector3.right;
                    break;
            }
            return dir;
        }
    }
    [SerializeField]
    private Transform _target;
    public Vector3 Target
    {
        get
        {
            return this._target.position;
        }
    }
}
