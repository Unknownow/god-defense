using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatesController : MonoBehaviour
{
    private EnemyProperties _enemyProperties;
    private IEnemyMovement _enemyMovement;
    private IEnemyAttack _enemyAttack;


    private void Start()
    {
        _enemyMovement = gameObject.GetComponent<IEnemyMovement>();
        _enemyAttack = gameObject.GetComponent<IEnemyAttack>();
        _enemyProperties = gameObject.GetComponent<EnemyProperties>();
    }

    private void Update()
    {
        if (_enemyMovement.IsAtFinishLine)
            _enemyAttack.StartAttack();
        else
            _enemyAttack.StopAttack();
    }

    public void OnEnemyDie()
    {

        _enemyProperties.Die();
        // start countdown destroy enemy
        StartCoroutine(DestroyEnemy(_enemyProperties.TimeBeforeDestroy));
    }

    IEnumerator DestroyEnemy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _enemyProperties.Destroy();
    }
}
