using System.Collections;
using System.Collections.Generic;
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
        if (damage < 0)
            return -1;
        _towerProperties.Hit = damage;
        return _towerProperties.HitPoints;
    }

    public float Heal(float healingAmount)
    {
        if (healingAmount < 0)
            return -1;
        _towerProperties.Heal = healingAmount;
        return _towerProperties.HitPoints;
    }

    public float GetHitPoint()
    {
        return _towerProperties.HitPoints;
    }
}
