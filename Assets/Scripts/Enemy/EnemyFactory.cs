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
    private GameObject _runnerPrefab;
    [SerializeField]
    private GameObject _heavierPrefab;
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

    public static GameObject SpawnEnemy(EnemyType enemyType, Vector3 position, Vector3 direction, int laneIndex, Transform parent = null)
    {
        switch (enemyType)
        {
            case EnemyType.Runner:
                return SpawnRunner(position, direction, laneIndex, parent);
            case EnemyType.Heavier:
                return SpawnHeavier(position, direction, laneIndex, parent);
            case EnemyType.Tanker:
                return SpawnTanker(position, direction, laneIndex, parent);
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

    private static GameObject SpawnRunner(Vector3 position, Vector3 direction, int laneIndex, Transform parent = null)
    {
        GameObject runner;
        if (_instance._runnerPool.Count <= 0)
        {
            runner = Instantiate(_instance._runnerPrefab, position, Quaternion.identity, _instance.transform);
            runner.SetActive(false);
        }
        else
            runner = _instance._runnerPool.Dequeue();
        runner.GetComponent<EnemyStatesController>().Initialize(position, laneIndex);
        runner.transform.forward = direction;
        runner.transform.parent = (parent == null) ? _instance.transform : parent;
        runner.SetActive(true);
        return runner;
    }

    private static GameObject SpawnHeavier(Vector3 position, Vector3 direction, int laneIndex, Transform parent = null)
    {
        GameObject heavier;
        if (_instance._heavierPool.Count <= 0)
        {
            heavier = Instantiate(_instance._heavierPrefab, position, Quaternion.identity, _instance.transform);
            heavier.SetActive(false);
        }
        else
            heavier = _instance._heavierPool.Dequeue();

        heavier.GetComponent<EnemyStatesController>().Initialize(position, laneIndex);
        heavier.transform.forward = direction;
        heavier.transform.parent = (parent == null) ? _instance.transform : parent;
        heavier.SetActive(true);
        return heavier;
    }

    private static GameObject SpawnTanker(Vector3 position, Vector3 direction, int laneIndex, Transform parent = null)
    {
        GameObject tanker;
        if (_instance._tankerPool.Count <= 0)
        {
            tanker = Instantiate(_instance._tankerPrefab, position, Quaternion.identity, _instance.transform);
            tanker.SetActive(false);
        }
        else
            tanker = _instance._tankerPool.Dequeue();

        tanker.GetComponent<EnemyStatesController>().Initialize(position, laneIndex);
        tanker.transform.forward = direction;
        tanker.transform.parent = (parent == null) ? _instance.transform : parent;
        tanker.SetActive(true);
        return tanker;
    }
}
