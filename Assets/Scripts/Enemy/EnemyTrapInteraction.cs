using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrapInteraction : MonoBehaviour
{
    private struct BoobyTrapStat
    {
        public float hitDamage;
        public float timeInterval;
    }
    private EnemyProperties _enemyProperties;
    private IEnemyMovement _enemyMovement;
    private EnemyPhysics _enemyPhysic;
    private IEnumerator _boobyTrapCoroutine;
    private int _isOnBoobyTrap;
    private Dictionary<GameObject, float> _freezeTrapList;
    private Dictionary<GameObject, BoobyTrapStat> _boobyTrapList;

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
    public void StepOnBoobyTrap(GameObject boobyTrap, float hitDamage, float timeInterval)
    {
        BoobyTrapStat trapStat = new BoobyTrapStat();
        if (!_boobyTrapList.ContainsKey(boobyTrap))
        {
            if (hitDamage > _boobyTrapList[boobyTrap].hitDamage)
            {
                trapStat.hitDamage = hitDamage;
                trapStat.timeInterval = timeInterval;
                _boobyTrapList[boobyTrap] = trapStat;
            }
        }
        else
        {
            trapStat.hitDamage = hitDamage;
            trapStat.timeInterval = timeInterval;
            _boobyTrapList.Add(boobyTrap, trapStat);
        }

        float maxHitDamage = 0;
        float maxtimeInterval = 0;
        foreach (GameObject trap in _boobyTrapList.Keys)
        {
            if (_boobyTrapList[trap].hitDamage > maxHitDamage)
            {
                maxHitDamage = _boobyTrapList[trap].hitDamage;
                maxtimeInterval = _boobyTrapList[trap].timeInterval;
            }
        }
        StopCoroutine(_boobyTrapCoroutine);
        _boobyTrapCoroutine = BoobyTrapHitCoroutine(maxHitDamage, maxtimeInterval);
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
    public void StepOutBoobyTrap(GameObject boobyTrap)
    {
        StopCoroutine(_boobyTrapCoroutine);
        _boobyTrapList.Remove(boobyTrap);
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
