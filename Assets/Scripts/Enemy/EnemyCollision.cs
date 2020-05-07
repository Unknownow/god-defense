using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private EnemyProperties _enemyProperties;
    private EnemyDebuff _enemyDebuff;
    private List<FreezingTrapProperties> _freezeTrapList;

    private void Awake()
    {
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
        _enemyDebuff = gameObject.GetComponent<EnemyDebuff>();
        _freezeTrapList = new List<FreezingTrapProperties>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Booby Trap"))
        {
            ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);
            BoobyTrapProperties trap = other.transform.GetComponent<BoobyTrapProperties>();
            _enemyDebuff.StepOnBoobyTrap(trap.HitDamage, trap.TimeInterval);
        }
        if (other.transform.CompareTag("Freezing Trap"))
        {
            ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);
            FreezingTrapProperties freezeTrap = other.transform.GetComponent<FreezingTrapProperties>();
            _freezeTrapList.Add(freezeTrap);
            float slowPercentage = 0;
            foreach (FreezingTrapProperties trap in _freezeTrapList)
            {
                if (trap.SlowPercentage > slowPercentage)
                    slowPercentage = trap.SlowPercentage;
            }
            _enemyDebuff.StepOnFreezeTrap(slowPercentage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Booby Trap"))
        {
            ReliableOnTriggerExit.NotifyTriggerExit(other, gameObject);
            _enemyDebuff.StepOutBoobyTrap();
        }
        if (other.transform.CompareTag("Freezing Trap"))
        {
            ReliableOnTriggerExit.NotifyTriggerExit(other, gameObject);
            _freezeTrapList.Remove(other.transform.GetComponent<FreezingTrapProperties>());
            if (_freezeTrapList.Count <= 0)
                _enemyDebuff.StepOutFreezeTrap();
        }
    }
}
