using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoobyTrapEffect : TrapEffect
{
    private BoobyTrapProperties TrapProperties
    {
        get
        {
            return (BoobyTrapProperties)_trapProperties;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDisable()
    {
        foreach (EnemyTrapInteraction enemy in _affectedEnemies)
            enemy.StepOutBoobyTrap(gameObject);
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
            enemy.StepOnBoobyTrap(gameObject, TrapProperties.HitDamage, TrapProperties.TimeInterval);
            _affectedEnemies.Add(enemy);
        }

    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            EnemyTrapInteraction enemy = other.gameObject.GetComponent<EnemyTrapInteraction>();
            enemy.StepOutBoobyTrap(gameObject);
            _affectedEnemies.Remove(enemy);
        }

    }

    public override void ReapplyWhenBuffed()
    {
        foreach (EnemyTrapInteraction enemy in _affectedEnemies)
            enemy.StepOnBoobyTrap(gameObject, TrapProperties.HitDamage, TrapProperties.TimeInterval);
    }
}
