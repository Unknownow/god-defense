using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFactory : MonoBehaviour
{
    private static EnemyFactory _instance;
    public static EnemyFactory Instance
    {
        get
        {
            return _instance;
        }
    }
    [SerializeField]
    private GameObject _runnerPerfab;
    [SerializeField]
    private GameObject _heavierPerfab;
    [SerializeField]
    private GameObject _tankerPrefab;


    private Queue<GameObject> _runnerPool;
    private Queue<GameObject> _heavierPool;
    private Queue<GameObject> _tankerPool;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        _instance._runnerPool = new Queue<GameObject>();
        _instance._heavierPool = new Queue<GameObject>();
        _instance._tankerPool = new Queue<GameObject>();
    }

    public static GameObject SpawnEnemy(EnemyType enemyType, Vector3 position, Vector3 direction, int laneIndex)
    {
        switch (enemyType)
        {
            case EnemyType.Runner:
                return SpawnRunner(position, direction, laneIndex);
            case EnemyType.Heavier:
                return SpawnHeavier(position, direction, laneIndex);
            case EnemyType.Tanker:
                return SpawnTanker(position, direction, laneIndex);
            default:
                return null;
        }
    }

    public static void DestroyEnemy(EnemyType enemyType, GameObject enemy)
    {
        switch (enemyType)
        {
            case EnemyType.Runner:
                enemy.SetActive(false);
                _instance._runnerPool.Enqueue(enemy);
                break;
            case EnemyType.Heavier:
                enemy.SetActive(false);
                _instance._heavierPool.Enqueue(enemy);
                break;
            case EnemyType.Tanker:
                enemy.SetActive(false);
                _instance._tankerPool.Enqueue(enemy);
                break;
            default:
                Destroy(enemy);
                break;
        }
    }

    private static GameObject SpawnRunner(Vector3 position, Vector3 direction, int laneIndex)
    {
        GameObject runner;
        if (_instance._runnerPool.Count <= 0)
        {
            runner = Instantiate(_instance._runnerPerfab, position, Quaternion.identity, _instance.transform);
            runner.SetActive(false);
        }
        else
            runner = _instance._runnerPool.Dequeue();

        runner.transform.forward = direction;
        runner.GetComponent<EnemyProperties>().Initialize(laneIndex, EnemyType.Runner, position);
        runner.SetActive(true);
        return runner;
    }

    private static GameObject SpawnHeavier(Vector3 position, Vector3 direction, int laneIndex)
    {
        GameObject heavier;
        if (_instance._runnerPool.Count <= 0)
        {
            heavier = Instantiate(_instance._heavierPerfab, position, Quaternion.identity, _instance.transform);
            heavier.SetActive(false);
        }
        else
            heavier = _instance._heavierPool.Dequeue();

        heavier.transform.forward = direction;
        heavier.GetComponent<EnemyProperties>().Initialize(laneIndex, EnemyType.Heavier, position);
        heavier.SetActive(true);
        return heavier;
    }

    private static GameObject SpawnTanker(Vector3 position, Vector3 direction, int laneIndex)
    {
        GameObject tanker;
        if (_instance._runnerPool.Count <= 0)
        {
            tanker = Instantiate(_instance._tankerPrefab, position, Quaternion.identity, _instance.transform);
            tanker.SetActive(false);
        }
        else
            tanker = _instance._tankerPool.Dequeue();

        tanker.transform.forward = direction;
        tanker.GetComponent<EnemyProperties>().Initialize(laneIndex, EnemyType.Tanker, position);
        tanker.SetActive(true);
        return tanker;
    }
}
