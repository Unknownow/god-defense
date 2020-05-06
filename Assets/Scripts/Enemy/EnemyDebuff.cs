using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDebuff : MonoBehaviour
{
    private EnemyProperties _enemyProperties;
    private IEnemyMovement _enemyMovement;
    private IEnumerator _boobyTrap;

    private void Start()
    {
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
        _enemyMovement = gameObject.GetComponent<IEnemyMovement>();
    }

    /// <summary>
    /// Call when step on booby trap
    /// </summary>
    /// <param name="damage">Damage of the trap</param>
    /// <param name="timeInterval">Time between each hit</param>
    public void StepOnBoobyTrap(float damage, float timeInterval)
    {
        _boobyTrap = HitCoroutine(damage, timeInterval);
        StartCoroutine(_boobyTrap);
    }

    public void StepOnBombTrap(float damage)
    {

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
        StopCoroutine(_boobyTrap);
    }

    public void StepOutBombTrap()
    {

    }

    /// <summary>
    /// Call when step out of freezing trap
    /// </summary>
    public void StepOutFreezeTrap()
    {
        _enemyMovement.BackToNormalSpeed();
    }

    private void Hit(float damage)
    {
        _enemyProperties.Hit = damage;
    }

    private IEnumerator HitCoroutine(float damage, float timeInterval)
    {
        while (true)
        {
            Hit(damage);
            yield return new WaitForSeconds(timeInterval);
        }
    }
}
