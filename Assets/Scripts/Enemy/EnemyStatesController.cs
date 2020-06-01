using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatesController : MonoBehaviour
{
    private EnemyProperties _enemyProperties;
    private IEnemyMovement _enemyMovement;
    private IEnemyAttack _enemyAttack;
    private Animator _enemyAnimator;
    private IEnemyVisualEffect _visual;


    private void Awake()
    {
        _enemyMovement = gameObject.GetComponent<IEnemyMovement>();
        _enemyAttack = gameObject.GetComponent<IEnemyAttack>();
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
        _enemyAnimator = gameObject.GetComponent<Animator>();
        _visual = gameObject.GetComponent<IEnemyVisualEffect>();
    }

    private void Update()
    {
        if (!_enemyProperties.IsAlive)
            return;

        if (_enemyMovement.IsAtFinishLine)
            Attack();
        else
            Moving();
    }

    public void Initialize(Vector3 position, int laneIndex)
    {
        _enemyProperties.Initialize(laneIndex);
        _visual.Init();
        StopAllCoroutines();
        // Reset physical components:
        transform.rotation = Quaternion.identity;
        // freeze gravity, rotation and position
        Rigidbody enemyBody = gameObject.GetComponent<Rigidbody>();
        enemyBody.velocity = Vector3.zero;
        enemyBody.angularVelocity = Vector3.zero;
        enemyBody.useGravity = false;
        enemyBody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY;

        // check isTrigger on collider.
        gameObject.GetComponent<Collider>().isTrigger = true;

        // enable NavMeshAgent
        NavMeshAgent enemyAgent = gameObject.GetComponent<NavMeshAgent>();
        if (enemyAgent.hasPath)
            enemyAgent.ResetPath();
        enemyAgent.enabled = true;
        enemyAgent.Warp(position);

        enemyAgent.speed = _enemyProperties.MovementSpeed;
        enemyAgent.angularSpeed = _enemyProperties.AngularSpeed;
        enemyAgent.acceleration = _enemyProperties.Acceleration;

        // animator
        _enemyAnimator.SetBool("dieFlag", false);
        _enemyAnimator.SetBool("runFlag", true);
        _enemyAnimator.SetBool("attackFlag", false);

    }

    public void Die()
    {
        _enemyProperties.Die();

        // die animation
        _enemyAnimator.SetBool("dieFlag", true);
        _enemyAnimator.SetBool("runFlag", false);
        _enemyAnimator.SetBool("attackFlag", false);

        // disable NavMeshAgent
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        // Destroy(gameObject.GetComponent<NavMeshAgent>());

        // using gravity and unfreeze rotation, position.
        Rigidbody enemyBody = gameObject.GetComponent<Rigidbody>();
        enemyBody.useGravity = true;
        enemyBody.constraints = RigidbodyConstraints.None;

        // uncheck isTrigger on collider.
        gameObject.GetComponent<Collider>().isTrigger = false;

        StartCoroutine(DestroyEnemy(_enemyProperties.TimeBeforeDestroy));
    }

    public void Destroy()
    {
        _enemyProperties.Die();

        // die animation
        _enemyAnimator.SetBool("dieFlag", true);
        _enemyAnimator.SetBool("runFlag", false);
        _enemyAnimator.SetBool("attackFlag", false);

        // disable NavMeshAgent
        gameObject.GetComponent<NavMeshAgent>().enabled = false;

        // using gravity and unfreeze rotation, position.
        Rigidbody enemyBody = gameObject.GetComponent<Rigidbody>();
        enemyBody.useGravity = true;
        enemyBody.constraints = RigidbodyConstraints.None;

        // uncheck isTrigger on collider.
        gameObject.GetComponent<Collider>().isTrigger = false;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        EnemyFactory.DestroyEnemy(_enemyProperties.Type, this.gameObject);
    }

    public void Attack()
    {
        _enemyAttack.StartAttack();

        _enemyAnimator.SetBool("attackFlag", true);
        _enemyAnimator.SetBool("runFlag", false);
        _enemyAnimator.SetBool("dieFlag", false);
    }

    public void Moving()
    {
        _enemyAttack.StopAttack();

        _enemyAnimator.SetBool("runFlag", true);
        _enemyAnimator.SetBool("attackFlag", false);
        _enemyAnimator.SetBool("dieFlag", false);
    }

    private IEnumerator DestroyEnemy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        EnemyFactory.DestroyEnemy(_enemyProperties.Type, this.gameObject);
    }
}
