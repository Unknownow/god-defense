using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TowerHitPointManager : MonoBehaviour
{
    private TowerProperties _towerProperties;
    public delegate void OnTowerDestroy();

    public delegate void OnTowerHit(float Heal);
    private event OnTowerDestroy _subscribersList;

    private event OnTowerHit _subscribersHitList;

    private void Start()
    {
        _towerProperties = gameObject.GetComponent<TowerProperties>();
    }

    // public void Init(){

    // }

    public float Hit(float damage)
    {
        Debug.Log("Got damage " + damage);
        if (_towerProperties.IsDestroyed)
            return 0;
        _towerProperties.Hit = damage;

        foreach (OnTowerHit func in _subscribersHitList.GetInvocationList())
        {
            func.Invoke(_towerProperties.Health);
        }
        
        // _subscribersHitList?.Invoke(_towerProperties.Health);
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

    public void SubscribeOnTowerHit(OnTowerHit onTowerHit)
    {
        _subscribersHitList += onTowerHit;
    }

    public void UnsubscribeOnTowerHit(OnTowerHit onTowerHit)
    {
        _subscribersHitList -= onTowerHit;
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
