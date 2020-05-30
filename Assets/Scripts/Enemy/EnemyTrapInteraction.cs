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
    private EnemyHitPointsManager _enemyHitPoints;
    private IEnemyMovement _enemyMovement;
    private Dictionary<GameObject, float> _freezeTrapList;
    private float _currentSlowPercentage;
    private Dictionary<GameObject, BoobyTrapStat> _boobyTrapList;
    private IEnumerator _boobyTrapCoroutine;
    private BoobyTrapStat _currentBoobyTrapStatUse;
    private IEnemyVisualEffect _visual;

    private void Awake()
    {
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
        _enemyMovement = gameObject.GetComponent<IEnemyMovement>();
        _enemyHitPoints = gameObject.GetComponent<EnemyHitPointsManager>();
        _freezeTrapList = new Dictionary<GameObject, float>();
        _boobyTrapList = new Dictionary<GameObject, BoobyTrapStat>();
        _visual = gameObject.GetComponent<IEnemyVisualEffect>();
    }

    /// <summary>
    /// Call when step on booby trap
    /// </summary>
    /// <param name="damage">Damage of the trap</param>
    /// <param name="timeInterval">Time between each hit</param>
    public void StepOnBoobyTrap(GameObject boobyTrap, float hitDamage, float timeInterval)
    {
        BoobyTrapStat trapStat = new BoobyTrapStat();
        if (_boobyTrapList.ContainsKey(boobyTrap))
        {
            if (hitDamage != _boobyTrapList[boobyTrap].hitDamage)
            {
                trapStat.hitDamage = hitDamage;
                trapStat.timeInterval = timeInterval;
                _boobyTrapList[boobyTrap] = trapStat;
            }
            else
                return;
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
        _currentBoobyTrapStatUse.hitDamage = maxHitDamage;
        _currentBoobyTrapStatUse.timeInterval = maxtimeInterval;

        if (_boobyTrapList.Count == 1)
        {
            _boobyTrapCoroutine = BoobyTrapHitCoroutine();
            StartCoroutine(_boobyTrapCoroutine);
        }
    }

    /// <summary>
    /// Call when step on bomb trap
    /// </summary>
    /// <param name="damage">Amount of damages taken when enemy is hit by bomb trap</param>
    public bool StepOnBombTrap(float damage)
    {
        _enemyHitPoints.Hit(damage);
        return !_enemyProperties.IsAlive;
    }

    /// <summary>
    /// Call when step on freezing trap
    /// </summary>
    /// <param name="slowPercentage">percentage of slow</param>
    public void StepOnFreezeTrap(GameObject freezeTrap, float slowPercentage)
    {
        if (_freezeTrapList.ContainsKey(freezeTrap))
        {
            if (_freezeTrapList[freezeTrap] != slowPercentage)
                _freezeTrapList[freezeTrap] = slowPercentage;
            else
                return;
        }
        else
        {
            _freezeTrapList.Add(freezeTrap, slowPercentage);
        }
        _currentSlowPercentage = 0;
        foreach (GameObject trap in _freezeTrapList.Keys)
            _currentSlowPercentage = (_freezeTrapList[trap] > _currentSlowPercentage) ? _freezeTrapList[trap] : _currentSlowPercentage;
        _enemyMovement.SlowDown(_currentSlowPercentage);
        _visual.Freeze(_currentSlowPercentage);
    }

    /// <summary>
    /// Call when step out of booby trap
    /// </summary>
    public void StepOutBoobyTrap(GameObject boobyTrap)
    {
        if (!_boobyTrapList.ContainsKey(boobyTrap))
            return;

        //remove booby trap from the list
        _boobyTrapList.Remove(boobyTrap);
        if (_boobyTrapList.Count <= 0)
        {
            StopCoroutine(_boobyTrapCoroutine);
            return;
        }

        // re-update current trap stat used
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
        _currentBoobyTrapStatUse.hitDamage = maxHitDamage;
        _currentBoobyTrapStatUse.timeInterval = maxtimeInterval;
    }

    /// <summary>
    /// Call when step out of freezing trap
    /// </summary>
    public void StepOutFreezeTrap(GameObject freezeTrap)
    {
        if (!_freezeTrapList.ContainsKey(freezeTrap))
            return;

        // remove freeze trap off the list.
        _freezeTrapList.Remove(freezeTrap);
        if (_freezeTrapList.Count <= 0)
        {
            _enemyMovement.BackToNormalSpeed();
            _visual.Unfreeze();
            return;
        }

        // re-update slow of player.
        _currentSlowPercentage = 0;
        foreach (GameObject trap in _freezeTrapList.Keys)
            _currentSlowPercentage = (_freezeTrapList[trap] > _currentSlowPercentage) ? _freezeTrapList[trap] : _currentSlowPercentage;
        _enemyMovement.SlowDown(_currentSlowPercentage);
        _visual.Freeze(_currentSlowPercentage);
    }

    private IEnumerator BoobyTrapHitCoroutine()
    {
        while (true)
        {
            _enemyHitPoints.Hit(_currentBoobyTrapStatUse.hitDamage);
            if (!_enemyProperties.IsAlive)
                break;
            yield return new WaitForSeconds(_currentBoobyTrapStatUse.timeInterval);
        }
        StopCoroutine(_boobyTrapCoroutine);
    }
}
