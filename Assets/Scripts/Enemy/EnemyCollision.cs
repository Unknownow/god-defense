using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private EnemyProperties _enemyProperties;
    private EnemyDebuff _enemyDebuff;

    private void Start()
    {
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
        _enemyDebuff = gameObject.GetComponent<EnemyDebuff>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Booby Trap"))
        {
            BoobyTrapProperties trap = other.transform.GetComponent<BoobyTrapProperties>();
            _enemyDebuff.StepOnBoobyTrap(trap.HitDamage, trap.TimeInterval);
        }
        if (other.transform.CompareTag("Freezing Trap"))
        {
            FreezingTrapProperties trap = other.transform.GetComponent<FreezingTrapProperties>();
            _enemyDebuff.StepOnFreezeTrap(trap.SlowPercentage);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform.CompareTag("Booby Trap"))
        {
            _enemyDebuff.StepOutBoobyTrap();
        }
        if (other.transform.CompareTag("Freezing Trap"))
        {
            _enemyDebuff.StepOutFreezeTrap();
        }
    }
}
