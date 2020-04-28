using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour
{
    [Header("Detail")]
    [SerializeField]
    private int _size;
    public int Size
    {
        get
        {
            return this._size;
        }
    }

    [Header("Movement")]
    [SerializeField]
    private float _movementSpeed;
    public float MovementSpeed
    {
        get
        {
            return this._movementSpeed;
        }
    }
    [SerializeField]
    private float _angularSpeed;
    public float AngularSpeed
    {
        get
        {
            return this._angularSpeed;
        }
    }
    [SerializeField]
    private float _acceleration;
    public float Acceleration
    {
        get
        {
            return this._acceleration;
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
        set
        {
            this._laneIndex = value;
        }
    }

    public void Initialize(int laneIndex)
    {
        this._laneIndex = laneIndex;
    }
}
