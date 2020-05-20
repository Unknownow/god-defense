using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyProperties : MonoBehaviour
{
    [Header("Stats")]

    // [SerializeField]
    // [Tooltip("Size of this enemy")]
    // private int _size;
    // public int Size
    // {
    //     get
    //     {
    //         return this._size;
    //     }
    // }
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
            if (_currentHitPoints <= 0)
                Die();
        }
    }

    [SerializeField]
    private EnemyType _enemyType;
    public EnemyType Type
    {
        get
        {
            return this._enemyType;
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

    [Header("Other")]
    [SerializeField]
    [Tooltip("How many seconds before destroy this enemy")]
    private float _timeBeforeDestroy;
    public float TimeBeforeDestroy
    {
        get
        {
            return this._timeBeforeDestroy;
        }
    }

    private bool _isAlive;
    public bool IsAlive
    {
        get
        {
            return this._isAlive;
        }
    }

    // public void Initialize(int laneIndex, EnemyType type, Vector3 position, float hitPointMul = 1, float movementSpeedMul = 1, float accelerationMul = 1, float angularSpeedMul = 1)
    // {
    //     // Reset properties:
    //     this._enemyType = type;
    //     this._laneIndex = laneIndex;
    //     transform.position = position;
    //     _currentHitPoints = _hitPoints * hitPointMul;
    //     _movementSpeed *= movementSpeedMul;
    //     _acceleration *= accelerationMul;
    //     _angularSpeed *= angularSpeedMul;
    //     StopAllCoroutines();


    //     // Reset physical components:
    //     transform.rotation = Quaternion.identity;
    //     // freeze gravity, rotation and position
    //     Rigidbody enemyBody = gameObject.GetComponent<Rigidbody>();
    //     enemyBody.velocity = Vector3.zero;
    //     enemyBody.angularVelocity = Vector3.zero;
    //     enemyBody.useGravity = false;
    //     enemyBody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY;

    //     // check isTrigger on collider.
    //     gameObject.GetComponent<Collider>().isTrigger = true;

    //     // enable NavMeshAgent
    //     NavMeshAgent enemyAgent = gameObject.GetComponent<NavMeshAgent>();
    //     enemyAgent.enabled = true;
    //     // enemyAgent.ResetPath();
    //     enemyAgent.speed = _movementSpeed;
    //     enemyAgent.angularSpeed = _angularSpeed;
    //     enemyAgent.acceleration = _acceleration;

    //     // Set is alive is true
    //     _isAlive = true;
    // }

    public void Initialize(Vector3 position, int laneIndex)
    {
        // Reset properties:
        LaneIndex = laneIndex;
        transform.position = position;
        _currentHitPoints = _hitPoints;

        // Set is alive is true
        _isAlive = true;
    }

    public void Die()
    {
        _isAlive = false;
    }

    // private void Die()
    // {
    //     // set is alive is false
    //     _isAlive = false;

    //     // disable NavMeshAgent
    //     gameObject.GetComponent<NavMeshAgent>().enabled = false;

    //     // using gravity and unfreeze rotation, position.
    //     Rigidbody enemyBody = gameObject.GetComponent<Rigidbody>();
    //     enemyBody.useGravity = true;
    //     enemyBody.constraints = RigidbodyConstraints.None;

    //     // uncheck isTrigger on collider.
    //     gameObject.GetComponent<Collider>().isTrigger = false;

    //     StartCoroutine(DestroyEnemy(_timeBeforeDestroy));
    // }



    // private IEnumerator DestroyEnemy(float seconds)
    // {
    //     yield return new WaitForSeconds(seconds);
    //     transform.position = Vector3.zero;
    //     transform.rotation = Quaternion.identity;
    //     EnemyFactory.DestroyEnemy(_enemyType, this.gameObject);
    // }
}
