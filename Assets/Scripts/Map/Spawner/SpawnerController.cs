using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField]
    private Transform _enemyParent;
    private SpawnerProperties _spawnerProperties;

    private void Start()
    {
        _spawnerProperties = gameObject.GetComponent<SpawnerProperties>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemy(EnemyType.Runner);
        }
    }

    public GameObject SpawnEnemy(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Runner:
                GameObject enemy = EnemyFactory.SpawnRunner(transform.position, _spawnerProperties.SpawnerDirection, _enemyParent, _spawnerProperties.LaneIndex);
                enemy.GetComponent<EnemyMovement>().SetDestination(_spawnerProperties.Target);
                return enemy;
            default:
                return null;
        }

    }
}
