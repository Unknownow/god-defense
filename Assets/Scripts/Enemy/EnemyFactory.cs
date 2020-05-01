using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory instance;
    [SerializeField]
    private GameObject _runnerPerfab;
    [SerializeField]
    private GameObject _heavierPerfab;

    private void Awake()
    {
        instance = this;
    }

    public static GameObject SpawnRunner(Vector3 position, Vector3 direction, Transform parent, int laneIndex)
    {
        GameObject runner = Instantiate(instance._runnerPerfab, position, Quaternion.identity, parent);
        runner.transform.forward = direction;
        runner.GetComponent<EnemyProperties>().Initialize(laneIndex);
        return runner;
    }

    public static GameObject SpawnHeavier(Vector3 position, Vector3 direction, Transform parent, int laneIndex)
    {
        GameObject heavier = Instantiate(instance._heavierPerfab, position, Quaternion.identity, parent);
        heavier.transform.forward = direction;
        heavier.GetComponent<EnemyProperties>().Initialize(laneIndex);
        return heavier;
    }
}
