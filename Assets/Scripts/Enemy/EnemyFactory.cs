using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory instance;
    [SerializeField]
    private GameObject _runnerPerfab;

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
}
