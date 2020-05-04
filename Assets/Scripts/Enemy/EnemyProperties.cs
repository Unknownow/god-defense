using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour
{
    [Header("Stats")]

    [SerializeField]
    [Tooltip("Size of this enemy")]
    private int _size;
    public int Size
    {
        get
        {
            return this._size;
        }
    }
    [SerializeField]
    [Tooltip("Enemy's Health")]
    private float _hitPoints;

    public float HitPoint
    {
        get
        {
            return this._hitPoints;
        }
    }

    public float Heal
    {
        set
        {
            if (value > 0)
            {
                this._hitPoints += value;
            }
        }
    }

    public float Hit
    {
        set
        {
            if (value > 0)
            {
                this._hitPoints -= value;
                this._hitPoints = this._hitPoints < 0 ? 0 : this._hitPoints;
            }
        }
    }

    [Header("Movement")]

    [SerializeField]
    [Tooltip("Max movement speed of this enemy")]
    private float _movementSpeed;
    public float MovementSpeed
    {
        get
        {
            return this._movementSpeed;
        }
    }
    [SerializeField]
    [Tooltip("How fast does this enemy turn")]
    private float _angularSpeed;
    public float AngularSpeed
    {
        get
        {
            return this._angularSpeed;
        }
    }
    [SerializeField]
    [Tooltip("Velocity of this enemy")]
    private float _acceleration;
    public float Acceleration
    {
        get
        {
            return this._acceleration;
        }
    }
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

    public void Initialize(int laneIndex, float movementSpeedMul = 1, float accelerationMul = 1, float angularSpeedMul = 1)
    {
        this._laneIndex = laneIndex;
        _movementSpeed *= movementSpeedMul;
        _acceleration *= accelerationMul;
        _angularSpeed *= angularSpeedMul;
    }

    [Header("Attack")]
    [SerializeField]
    [Tooltip("How hard does this enemy hit")]
    private float _hitDamage;
    public float HitDamage
    {
        get
        {
            return this._hitDamage;
        }
    }
    [SerializeField]
    [Tooltip("How many attack this enemy can do in 1 second")]
    private float _attackRate;
    public float AttackRate
    {
        get
        {
            return this._attackRate;
        }
    }
    [SerializeField]
    [Tooltip("How far can this enemy attack")]
    private float _attackRange;
    public float AttackRange
    {
        get
        {
            return this._attackRange;
        }
    }
}
