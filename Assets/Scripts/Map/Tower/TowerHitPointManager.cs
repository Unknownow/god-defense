using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TowerHitPointManager : MonoBehaviour
{
    private TowerProperties _towerProperties;
    public delegate void OnTowerDestroy();
    private event OnTowerDestroy _subscribersList;

    private void Start()
    {
        _towerProperties = gameObject.GetComponent<TowerProperties>();
    }

    public float Hit(float damage)
    {
        if (_towerProperties.IsDestroyed)
            return 0;
        _towerProperties.Hit = damage;
        if (_towerProperties.IsDestroyed)
            _subscribersList?.Invoke();
        return _towerProperties.CurrentHitPoints;
    }

    public float Heal(float healingAmount)
    {
        _towerProperties.Heal = healingAmount;
        return _towerProperties.CurrentHitPoints;
    }

    public float GetHitPoint()
    {
        return _towerProperties.CurrentHitPoints;
    }

    public void SubscribeOnTowerDestroy(OnTowerDestroy onTowerDestroy)
    {
        _subscribersList += onTowerDestroy;
    }

    public void UnsubscribeOnTowerDestroy(OnTowerDestroy onTowerDestroy)
    {
        _subscribersList -= onTowerDestroy;
    }
}
