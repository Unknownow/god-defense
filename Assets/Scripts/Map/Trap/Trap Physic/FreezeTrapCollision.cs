﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTrapCollision : TrapCollision
{
    private FreezeTrapProperties TrapProperties
    {
        get
        {
            return (FreezeTrapProperties)_trapProperties;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDisable()
    {
        foreach (EnemyTrapInteraction enemy in _affectedEnemies)
            enemy.StepOutFreezeTrap(gameObject);
        _affectedEnemies.RemoveAll((enemy) =>
        {
            return true;
        });
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.transform.CompareTag("Enemy"))
        {
            EnemyTrapInteraction enemy = other.gameObject.GetComponent<EnemyTrapInteraction>();
            _affectedEnemies.Add(enemy);
            enemy.StepOnFreezeTrap(gameObject, TrapProperties.SlowPercentage);
        }

    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            EnemyTrapInteraction enemy = other.gameObject.GetComponent<EnemyTrapInteraction>();
            enemy.StepOutFreezeTrap(gameObject);
            _affectedEnemies.Remove(enemy);
        }
    }
}