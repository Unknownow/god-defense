﻿using System.Collections;
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
    [SerializeField]
    private GameObject _tankerPrefab;


    private Queue<GameObject> _runnerPool;
    private Queue<GameObject> _heavierPool;
    private Queue<GameObject> _tankerPool;

    private void Awake()
    {
        instance = this;
        instance._runnerPool = new Queue<GameObject>();
        instance._heavierPool = new Queue<GameObject>();
        instance._tankerPool = new Queue<GameObject>();
    }

    public static GameObject SpawnRunner(Vector3 position, Vector3 direction, Transform parent, int laneIndex)
    {
        GameObject runner;
        if (instance._runnerPool.Count <= 0)
        {
            runner = Instantiate(instance._runnerPerfab, position, Quaternion.identity, parent);
        }
        else
        {
            runner = instance._runnerPool.Dequeue();
            runner.SetActive(true);
        }
        runner.transform.forward = direction;
        runner.GetComponent<EnemyProperties>().Initialize(laneIndex);
        return runner;
    }

    public static GameObject SpawnHeavier(Vector3 position, Vector3 direction, Transform parent, int laneIndex)
    {
        GameObject heavier;
        if (instance._runnerPool.Count <= 0)
        {
            heavier = Instantiate(instance._heavierPerfab, position, Quaternion.identity, parent);
        }
        else
        {
            heavier = instance._heavierPool.Dequeue();
            heavier.SetActive(true);
        }
        heavier.transform.forward = direction;
        heavier.GetComponent<EnemyProperties>().Initialize(laneIndex);
        return heavier;
    }

    public static GameObject SpawnTanker(Vector3 position, Vector3 direction, Transform parent, int laneIndex)
    {
        GameObject tanker;
        if (instance._runnerPool.Count <= 0)
        {
            tanker = Instantiate(instance._tankerPrefab, position, Quaternion.identity, parent);
        }
        else
        {
            tanker = instance._tankerPool.Dequeue();
            tanker.SetActive(true);
        }
        tanker.transform.forward = direction;
        tanker.GetComponent<EnemyProperties>().Initialize(laneIndex);
        return tanker;
    }

    public static void DestroyEnemy(EnemyType type, GameObject enemy)
    {
        switch (type)
        {
            case EnemyType.Runner:
                enemy.SetActive(false);
                instance._runnerPool.Enqueue(enemy);
                break;
            case EnemyType.Heavier:
                enemy.SetActive(false);
                instance._heavierPool.Enqueue(enemy);
                break;
            case EnemyType.Tanker:
                enemy.SetActive(false);
                instance._tankerPool.Enqueue(enemy);
                break;
        }
    }
}