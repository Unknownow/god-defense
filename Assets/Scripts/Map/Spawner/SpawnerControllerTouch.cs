using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControllerTouch : MonoBehaviour
{
    [SerializeField]
    private Transform _enemyParent;
    private SpawnerProperties _spawnerProperties;
    private float maxTouchTime = 0.5f;
    private float lastTouchTime;
    private int tapCount;

    private void Start()
    {
        _spawnerProperties = gameObject.GetComponent<SpawnerProperties>();
    }
    private void Update()
    {
        if( Input.touchCount == 1  && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (tapCount > 0)
            {
                tapCount++;
            }
            else
            {
                tapCount = 1;
                lastTouchTime = Time.time;
            }
        }
        if (Time.time - lastTouchTime > maxTouchTime)
        {
            if (tapCount == 1)
            {
                SpawnEnemy(EnemyType.Runner);
            }
            if (tapCount > 1)
            {
                SpawnEnemy(EnemyType.Heavier);
            }
            tapCount = 0;
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
            case EnemyType.Heavier:
            case EnemyType.Tanker:
                enemy = EnemyFactory.SpawnEnemy(type, transform.position, _spawnerProperties.SpawnerDirection, _spawnerProperties.LaneIndex);
                enemy.GetComponent<IEnemyMovement>().StartMoving(_spawnerProperties.Target);
                return enemy;
            default:
                return null;
        }
    }
}
