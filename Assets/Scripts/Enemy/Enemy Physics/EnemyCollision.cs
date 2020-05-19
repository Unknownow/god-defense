using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private EnemyProperties _enemyProperties;

    private void Awake()
    {
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Bomb Trap"))
        {
            other.transform.GetComponent<BombTrapExplosion>().DetonateBomb();
        }
        if (other.transform.CompareTag("Bullet"))
        {
            //TODO: add enemy damaged by bullet.
            _enemyProperties.Hit = other.transform.GetComponent<BulletProperties>().BulletDamage;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // if (other.transform.CompareTag("Booby Trap"))
        // {
        //     ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);
        //     BoobyTrapProperties boobyTrap = other.transform.GetComponent<BoobyTrapProperties>();
        //     _boobyTrapList.Add(boobyTrap);
        //     float hitDamage = 0;
        //     float timeInterval = 0;
        //     foreach (BoobyTrapProperties trap in _boobyTrapList)
        //     {
        //         if (trap.HitDamage > hitDamage)
        //         {
        //             hitDamage = trap.HitDamage;
        //             timeInterval = trap.TimeInterval;
        //         }
        //     }
        //     _enemyTrapInteraction.StepOnBoobyTrap(hitDamage, timeInterval);
        // }
        // if (other.transform.CompareTag("Freezing Trap"))
        // {
        //     ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);
        //     FreezingTrapProperties freezeTrap = other.transform.GetComponent<FreezingTrapProperties>();
        //     _freezeTrapList.Add(freezeTrap);
        //     float slowPercentage = 0;
        //     foreach (FreezingTrapProperties trap in _freezeTrapList)
        //     {
        //         if (trap.SlowPercentage > slowPercentage)
        //             slowPercentage = trap.SlowPercentage;
        //     }
        //     _enemyTrapInteraction.StepOnFreezeTrap(slowPercentage);
        // }
    }

    private void OnTriggerExit(Collider other)
    {
        // if (other.transform.CompareTag("Booby Trap"))
        // {
        //     ReliableOnTriggerExit.NotifyTriggerExit(other, gameObject);
        //     _boobyTrapList.Remove(other.transform.GetComponent<BoobyTrapProperties>());
        //     if (_boobyTrapList.Count <= 0)
        //         _enemyTrapInteraction.StepOutBoobyTrap();
        // }
        // if (other.transform.CompareTag("Freezing Trap"))
        // {
        //     ReliableOnTriggerExit.NotifyTriggerExit(other, gameObject);
        //     _freezeTrapList.Remove(other.transform.GetComponent<FreezingTrapProperties>());
        //     if (_freezeTrapList.Count <= 0)
        //         _enemyTrapInteraction.StepOutFreezeTrap();
        // }
    }
}
