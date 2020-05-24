﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFactory : MonoBehaviour
{
    private static TrapFactory _instance;
    public static TrapFactory Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField]
    private GameObject _boobyTrapPrefab;
    [SerializeField]
    private GameObject _bombTrapPrefab;
    [SerializeField]
    private GameObject _freezeTrapPrefab;

    [SerializeField]
    private GameObject _freezeDummyTrapPrefab;

    [SerializeField]
    private GameObject _boobyDummyTrapPrefab;

    private Queue<GameObject> _boobyTrapPool;
    private Queue<GameObject> _bombTrapPool;
    private Queue<GameObject> _freezeTrapPool;

    private int count = 0;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        _instance._boobyTrapPool = new Queue<GameObject>();
        _instance._bombTrapPool = new Queue<GameObject>();
        _instance._freezeTrapPool = new Queue<GameObject>();
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.B))
        // {
        //     SpawnBoobyTrap(new Vector3(count, 0, 0), transform);
        //     count++;
        // }
    }


    public static GameObject SpawnTrap(TrapType trapType, Vector3 position, Transform parent = null)
    {
        switch (trapType)
        {
            case TrapType.Booby:
                return _instance.SpawnBoobyTrap(position, parent);
            case TrapType.Bomb:
                return _instance.SpawnBombTrap(position, parent);
            case TrapType.Freeze:
                return _instance.SpawnFreezeTrap(position, parent);
            default:
                return null;
        }
    }

    public static GameObject SpawnDummyTrap(TrapType trapType, Vector3 position, Transform parent = null)
    {
        switch (trapType)
        {
            case TrapType.Booby:
                return Instantiate(_instance._boobyDummyTrapPrefab, position, Quaternion.identity, _instance.transform);
            case TrapType.Bomb:
                return null;
            case TrapType.Freeze:
                return Instantiate(_instance._freezeDummyTrapPrefab, position, Quaternion.identity, _instance.transform);
            default:
                return null;
        }
    }

    public static void DestroyTrap(TrapType trapType, GameObject trap)
    {
        switch (trapType)
        {
            case TrapType.Booby:
                trap.SetActive(false);
                _instance._boobyTrapPool.Enqueue(trap);
                break;
            case TrapType.Bomb:
                trap.SetActive(false);
                _instance._bombTrapPool.Enqueue(trap);
                break;
            case TrapType.Freeze:
                trap.SetActive(false);
                _instance._freezeTrapPool.Enqueue(trap);
                break;
            default:
                trap.SetActive(false);
                break;
        }
    }

    private GameObject SpawnBoobyTrap(Vector3 position, Transform parent = null)
    {
        GameObject boobyTrap;
        if (_instance._boobyTrapPool.Count <= 0)
        {
            boobyTrap = Instantiate(_instance._boobyTrapPrefab, position, Quaternion.identity, _instance.transform);
            boobyTrap.SetActive(false);
        }
        else
            boobyTrap = _instance._boobyTrapPool.Dequeue();

        boobyTrap.transform.parent = (parent == null) ? _instance.transform : parent;
        boobyTrap.SetActive(true);
        boobyTrap.GetComponent<TrapStatesController>().Initialize(position);
        return boobyTrap;
    }

    private GameObject SpawnBombTrap(Vector3 position, Transform parent = null)
    {
        GameObject bombTrap;
        if (_instance._bombTrapPool.Count <= 0)
        {
            bombTrap = Instantiate(_instance._bombTrapPrefab, position, Quaternion.identity, _instance.transform);
            bombTrap.SetActive(false);
        }
        else

            bombTrap = _instance._bombTrapPool.Dequeue();

        bombTrap.transform.parent = (parent == null) ? _instance.transform : parent;
        bombTrap.SetActive(true);
        bombTrap.GetComponent<TrapStatesController>().Initialize(position);
        return bombTrap;
    }

    private GameObject SpawnFreezeTrap(Vector3 position, Transform parent = null)
    {
        GameObject freezeTrap;
        if (_instance._freezeTrapPool.Count <= 0)
        {
            freezeTrap = Instantiate(_instance._freezeTrapPrefab, position, Quaternion.identity, _instance.transform);
            freezeTrap.SetActive(false);
        }
        else
            freezeTrap = _instance._freezeTrapPool.Dequeue();

        freezeTrap.transform.parent = (parent == null) ? _instance.transform : parent;
        freezeTrap.SetActive(true);
        freezeTrap.GetComponent<TrapStatesController>().Initialize(position);
        return freezeTrap;
    }

}
