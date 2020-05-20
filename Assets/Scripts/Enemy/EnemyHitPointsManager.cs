using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitPointsManager : MonoBehaviour
{
    private EnemyProperties _enemyProperties;
    private EnemyStatesController _enemyStates;

    private void Awake()
    {
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
        _enemyStates = gameObject.GetComponent<EnemyStatesController>();
    }

    public void Hit(float damageDeal)
    {
        _enemyProperties.Hit = damageDeal;
        if (!_enemyProperties.IsAlive)
        {
            _enemyStates.Die();
        }
    }

    public void Heal(float healingAmount)
    {
        _enemyProperties.Heal = healingAmount;
    }
}
