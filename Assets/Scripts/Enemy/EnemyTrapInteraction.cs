using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrapInteraction : MonoBehaviour
{
    private EnemyProperties _enemyProperties;
    private IEnemyMovement _enemyMovement;
    private EnemyPhysics _enemyPhysic;
    private IEnumerator _boobyTrapCoroutine;
    private int _isOnBoobyTrap;

    private void Start()
    {
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
        _enemyMovement = gameObject.GetComponent<IEnemyMovement>();
        _enemyPhysic = gameObject.GetComponent<EnemyPhysics>();
        _isOnBoobyTrap = 0;
    }

    /// <summary>
    /// Call when step on booby trap
    /// </summary>
    /// <param name="damage">Damage of the trap</param>
    /// <param name="timeInterval">Time between each hit</param>
    public void StepOnBoobyTrap(float damage, float timeInterval)
    {
        StopCoroutine(_boobyTrapCoroutine);
        _boobyTrapCoroutine = BoobyTrapHitCoroutine(damage, timeInterval);
        StartCoroutine(_boobyTrapCoroutine);
    }

    /// <summary>
    /// Call when step on bomb trap
    /// </summary>
    /// <param name="damage">Amount of damages taken when enemy is hit by bomb trap</param>
    public bool StepOnBombTrap(float damage)
    {
        return HitAndCheckIsDead(damage);
    }

    /// <summary>
    /// Call when step on freezing trap
    /// </summary>
    /// <param name="slowPercentage">percentage of slow</param>
    public void StepOnFreezeTrap(float slowPercentage)
    {
        _enemyMovement.SlowDown(slowPercentage);
    }

    /// <summary>
    /// Call when step out of booby trap
    /// </summary>
    public void StepOutBoobyTrap()
    {
        StopCoroutine(_boobyTrapCoroutine);
    }

    /// <summary>
    /// Call when step out of freezing trap
    /// </summary>
    public void StepOutFreezeTrap()
    {
        _enemyMovement.BackToNormalSpeed();
    }

    private bool HitAndCheckIsDead(float damage)
    {
        _enemyProperties.Hit = damage;
        if (_enemyProperties.CurrentHitPoints <= 0)
        {
            gameObject.GetComponent<EnemyStates>().OnEnemyDie();
            return true;
        }
        return false;
    }

    private IEnumerator BoobyTrapHitCoroutine(float damage, float timeInterval)
    {
        while (true)
        {
            if (HitAndCheckIsDead(damage))
                break;
            yield return new WaitForSeconds(timeInterval);
        }
        StopCoroutine(_boobyTrapCoroutine);
    }
}
