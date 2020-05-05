using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControllerTouch : MonoBehaviour
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
        if (tapCount() == 1)
        {
            SpawnEnemy(EnemyType.Runner);
        }
        if (tapCount() == 2)
        {
            SpawnEnemy(EnemyType.Heavier);
        }
    }

public static int tapCount(){
    int result = 0;
    float MaxTimeWait = 1;
    float VariancePosition = 1;

    if( Input.touchCount == 1  && Input.GetTouch(0).phase == TouchPhase.Began)
    {
        float DeltaTime = Input.GetTouch (0).deltaTime;
        float DeltaPositionLenght = Input.GetTouch (0).deltaPosition.magnitude;

        if ( DeltaTime > 0 && DeltaTime < MaxTimeWait && DeltaPositionLenght < VariancePosition)
            result = 2;
        else
            result = 1;                
    }
    return result;
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
                enemy.GetComponent<EnemyMovement>().SetDestination(_spawnerProperties.Target);
                return enemy;
            case EnemyType.Heavier:
                enemy = EnemyFactory.SpawnHeavier(transform.position, _spawnerProperties.SpawnerDirection, _enemyParent, _spawnerProperties.LaneIndex);
                enemy.GetComponent<EnemyMovement>().SetDestination(_spawnerProperties.Target);
                return enemy;
            default:
                return null;
        }
    }
}
