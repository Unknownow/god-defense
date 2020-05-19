using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TowerService : MonoBehaviour
{
    private TowerProperties _towerProperties;

    private void Start()
    {
        _towerProperties = gameObject.GetComponent<TowerProperties>();
    }

    public float Hit(float damage)
    {
        _towerProperties.Hit = damage;
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
}
