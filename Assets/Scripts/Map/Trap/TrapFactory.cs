using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFactory : MonoBehaviour
{
    private static TrapFactory instance;

    [SerializeField]
    private GameObject _boobyTrapPrefab;
    [SerializeField]
    private GameObject _bombTrapPrefab;
    [SerializeField]
    private GameObject _freezeTrapPrefab;

    private Queue<GameObject> _boobyTrapPool;
    private Queue<GameObject> _bombTrapPool;
    private Queue<GameObject> _freezeTrapPool;
    private int count = 0;
    private void Awake()
    {
        instance = this;
        instance._boobyTrapPool = new Queue<GameObject>();
        instance._bombTrapPool = new Queue<GameObject>();
        instance._freezeTrapPool = new Queue<GameObject>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnFreezeTrap(new Vector3(count, 0, 0), transform);
            count++;
        }
    }

    public static GameObject SpawnTrap(TrapType trapType, Vector3 position, Transform parent)
    {
        GameObject trap;
        switch (trapType)
        {
            case TrapType.Booby:
                trap = SpawnBoobyTrap(position, parent);
                break;
            case TrapType.Bomb:
                trap = SpawnBombTrap(position, parent);
                break;
            case TrapType.Freeze:
                trap = SpawnFreezeTrap(position, parent);
                break;
            default:
                trap = null;
                break;
        }
        return trap;
    }

    public static void DestroyTrap(TrapType trapType, GameObject trap)
    {
        switch (trapType)
        {
            case TrapType.Booby:
                trap.SetActive(false);
                instance._boobyTrapPool.Enqueue(trap);
                break;
            case TrapType.Bomb:
                trap.SetActive(false);
                instance._bombTrapPool.Enqueue(trap);
                break;
            case TrapType.Freeze:
                trap.SetActive(false);
                instance._freezeTrapPool.Enqueue(trap);
                break;
            default:
                trap.SetActive(false);
                break;
        }
    }

    private static GameObject SpawnBoobyTrap(Vector3 position, Transform parent)
    {
        GameObject boobyTrap;
        if (instance._boobyTrapPool.Count <= 0)
        {
            boobyTrap = Instantiate(instance._boobyTrapPrefab, position, Quaternion.identity, parent);
        }
        else
        {
            boobyTrap = instance._boobyTrapPool.Dequeue();
            boobyTrap.SetActive(true);
        }
        boobyTrap.GetComponent<TrapProperties>().Initialize(position, TrapType.Booby);
        return boobyTrap;
    }

    private static GameObject SpawnBombTrap(Vector3 position, Transform parent)
    {
        GameObject bombTrap;
        if (instance._bombTrapPool.Count <= 0)
        {
            bombTrap = Instantiate(instance._bombTrapPrefab, position, Quaternion.identity, parent);
        }
        else
        {
            bombTrap = instance._bombTrapPool.Dequeue();
            bombTrap.SetActive(true);
        }
        bombTrap.GetComponent<TrapProperties>().Initialize(position, TrapType.Bomb);
        return bombTrap;
    }

    private static GameObject SpawnFreezeTrap(Vector3 position, Transform parent)
    {
        GameObject freezeTrap;
        if (instance._freezeTrapPool.Count <= 0)
        {
            freezeTrap = Instantiate(instance._freezeTrapPrefab, position, Quaternion.identity, parent);
        }
        else
        {
            freezeTrap = instance._freezeTrapPool.Dequeue();
            freezeTrap.SetActive(true);
        }
        freezeTrap.GetComponent<TrapProperties>().Initialize(position, TrapType.Freeze);
        return freezeTrap;
    }
}
