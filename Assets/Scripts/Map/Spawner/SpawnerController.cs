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
        if (Input.GetKeyDown(KeyCode.X))
        {
            SpawnEnemy(EnemyType.Heavier);
        }
    }

    public GameObject SpawnEnemy(EnemyType type)
    {
        if (_spawnerProperties.SpawnableTypes.IndexOf(type) == -1)
            return null;
        GameObject enemy;
        switch (type)
        {
            case EnemyType.Runner:
                enemy = EnemyFactory.SpawnRunner(transform.position, _spawnerProperties.SpawnerDirection, _enemyParent, _spawnerProperties.LaneIndex);
                enemy.GetComponent<IEnemyMovement>().StartMoving(_spawnerProperties.Target);
                return enemy;
            case EnemyType.Heavier:
                enemy = EnemyFactory.SpawnHeavier(transform.position, _spawnerProperties.SpawnerDirection, _enemyParent, _spawnerProperties.LaneIndex);
                enemy.GetComponent<IEnemyMovement>().StartMoving(_spawnerProperties.Target);
                return enemy;
            default:
                return null;
        }
    }
}
