using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private float _currentHitPoints;

    public float CurrentHitPoints
    {
        get
        {
            return this._currentHitPoints;
        }
    }

    public float MaxHitPoints
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
                this._currentHitPoints += value;
                this._currentHitPoints = this._currentHitPoints > _hitPoints ? _hitPoints : this._currentHitPoints;
            }
        }
    }

    public float Hit
    {
        set
        {
            if (value > 0)
            {
                this._currentHitPoints -= value;
                this._currentHitPoints = this._currentHitPoints < 0 ? 0 : this._currentHitPoints;
            }
        }
    }

    private EnemyType _enemyType;
    public EnemyType Type
    {
        get
        {
            return this._enemyType;
        }
    }

    // [SerializeField]
    // [Tooltip("Radius to calculate when enemy steps into trap")]
    // private float _trapRadius;

    // public float TrapRadius
    // {
    //     get
    //     {
    //         return this._trapRadius;
    //     }
    // }

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

    // public void Initialize(int laneIndex)
    // {
    //     this._laneIndex = laneIndex;
    // }

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

    [Header("Physics")]
    [SerializeField]
    private float _gravityMultiplier;

    public void Initialize(int laneIndex, EnemyType type, Vector3 position, float hitPointMul = 1, float movementSpeedMul = 1, float accelerationMul = 1, float angularSpeedMul = 1)
    {
        // Reset properties:
        this._enemyType = type;
        this._laneIndex = laneIndex;
        transform.position = position;
        _currentHitPoints = _hitPoints * hitPointMul;
        _movementSpeed *= movementSpeedMul;
        _acceleration *= accelerationMul;
        _angularSpeed *= angularSpeedMul;


        // Reset physical components:
        // freeze gravity, rotation and position
        Rigidbody enemyBody = gameObject.GetComponent<Rigidbody>();
        enemyBody.useGravity = false;
        enemyBody.constraints = RigidbodyConstraints.FreezePositionY;
        enemyBody.constraints = RigidbodyConstraints.FreezeRotationX;
        enemyBody.constraints = RigidbodyConstraints.FreezeRotationZ;

        // check isTrigger on collider.
        gameObject.GetComponent<Collider>().isTrigger = true;

        // enable NavMeshAgent
        NavMeshAgent enemyAgent = gameObject.GetComponent<NavMeshAgent>();
        enemyAgent.enabled = true;
        enemyAgent.speed = _movementSpeed;
        enemyAgent.angularSpeed = _angularSpeed;
        enemyAgent.acceleration = _acceleration;
    }

    public void Destroy()
    {
        EnemyFactory.DestroyEnemy(_enemyType, this.gameObject);
    }
}
