﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Threading.Tasks;

public class GameStateManager : MonoBehaviour
{
    public delegate void OnWinStage();
    private event OnWinStage _winStageSubscribersList;
    public delegate void OnLostStage();
    private event OnLostStage _lostStageSubscribersList;

    public delegate void OnStartStage();
    private event OnStartStage _startStageSubscribersList;

    public delegate void OnTowerUpdateHeal(float health);
    private event OnTowerUpdateHeal _towerHealthSubscribersList;

    public delegate void OnStageLoaded();
    private event OnStageLoaded _stageLoadedSubscribers;

    private GameObject _currentMap;
    [SerializeField]
    private TowerHitPointManager _towerHitPoint;
    private StageSpawnManager _stageSpawnManager;
    private bool _isGameOver;
    public bool IsGameOver
    {
        get
        {
            return this._isGameOver;
        }
    }
    private bool _firstTimeLoad;

    private int _currentStage;

    private void Awake()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Manager");
        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }
        // DontDestroyOnLoad(this.gameObject);

        _stageSpawnManager = gameObject.GetComponent<StageSpawnManager>();
        _stageSpawnManager.SubscribeOnStageEnd(OnStageEnd);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            _stageSpawnManager.LoadStageDetail(0);
            StartStage();
        }
    }

    /// <summary>
    /// Phương thức được sử dụng để load các sự kiện trước khi bắt đầu game, tuy nhiên còn thiếu load prefab của map.
    /// </summary>
    /// <param name="stageIndex"></param>
    public void PrepareStage(int stageIndex)
    {
        if (_currentMap != null)
            Destroy(_currentMap);
        _isGameOver = false;
        _stageSpawnManager.LoadStageDetail(stageIndex);

        // TODO: add load map prefab
        GameObject mapPrefab = Resources.Load<GameObject>("Prefabs/Maps/Stage/Stage " + stageIndex);
        _currentMap = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity);

        // _towerHitPoint = GameObject.FindGameObjectWithTag("Tower").GetComponent<TowerHitPointManager>();
        _towerHitPoint = GameObject.FindObjectOfType<TowerHitPointManager>();

        // Subscribe
        _towerHitPoint.SubscribeOnTowerDestroy(OnTowerDestroyed);
        _towerHitPoint.SubscribeOnTowerHit(OnUpdateTowerHealth);

        // After loaded
        _currentStage = stageIndex;
        if (_stageLoadedSubscribers?.GetInvocationList() != null)
        {
            foreach (OnStageLoaded func in _stageLoadedSubscribers?.GetInvocationList())
            {
                if (func != null)
                {
                    func();
                }
            }
        }
        _currentMap.SetActive(false);
    }

    // NOTE VĨNH: Gọi hàm này để set vị trí.
    public void setMapPosition(Vector3 pos)
    {
        if (this._currentMap == null)
        {
            GameObject mapPrefab = Resources.Load<GameObject>("Prefabs/Maps/Stage/Stage " + _currentStage);
            _currentMap = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity);
        }
        this._currentMap.transform.position = pos;
        this._currentMap.SetActive(true);
    }

    /// <summary>
    /// Phương thức được sử dụng để bắt đầu vào stage (tức là bắt đầu sinh quái)
    /// </summary>
    public void StartStage()
    {
        _stageSpawnManager.StartStage();
        _startStageSubscribersList?.Invoke();
    }

    /// <summary>
    /// Phương thức sử dụng để pause game
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// Phương thức sử dụng để resume game
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1;
    }

    public int GetCurrentStage()
    {
        return _currentStage;
    }

    /// <summary>
    /// Phương thức sử dụng để các object bên ngoài có thể subscribe event win.
    /// </summary>
    /// <param name="subscriber">phương thức của object bên ngoài muốn gọi lúc win</param>
    public void SubscribeOnWinStage(OnWinStage subscriber)
    {
        _winStageSubscribersList += subscriber;
    }

    /// <summary>
    /// Phương thức sử dụng để các object bên ngoài có thể unsubscribe event win.
    /// </summary>
    /// <param name="subscriber">phương thức của object bên ngoài muốn unsubscribe</param>
    public void UnsubscribeOnWinStage(OnWinStage subscriber)
    {
        _winStageSubscribersList -= subscriber;
    }

    /// <summary>
    /// Phương thức sử dụng để các object bên ngoài có thể subscribe event lost.
    /// </summary>
    /// <param name="subscriber">phương thức của object bên ngoài muốn gọi lúc lost</param>
    public void SubscribeOnLostStage(OnLostStage subscriber)
    {
        _lostStageSubscribersList += subscriber;
    }

    /// <summary>
    /// Phương thức sử dụng để các object bên ngoài có thể unsubscribe event lost.
    /// </summary>
    /// <param name="subscriber">phương thức của object bên ngoài muốn unsubscribe</param>
    public void UnsubscribeOnLostStage(OnLostStage subscriber)
    {
        _lostStageSubscribersList -= subscriber;
    }

    public void SubscribeOnStageStart(OnStartStage subscriber)
    {
        _startStageSubscribersList += subscriber;
    }

    public void UnsubscribeOnStageStart(OnStartStage subscriber)
    {
        _startStageSubscribersList -= subscriber;
    }

    public void SubscribeOnUpdateTowerHealth(OnTowerUpdateHeal subscriber)
    {
        _towerHealthSubscribersList += subscriber;
    }

    public void UnsubscribeOnUpdateTowerHealth(OnTowerUpdateHeal subscriber)
    {
        _towerHealthSubscribersList -= subscriber;
    }

    public void SubscribeOnWavePreparing(StageSpawnManager.OnWavePreparing subscriber)
    {
        _stageSpawnManager.SubscribeOnWavePreparing(subscriber);
    }

    public void UnsubscribeOnWavePreparing(StageSpawnManager.OnWavePreparing subscriber)
    {
        _stageSpawnManager.UnsubscribeOnWavePreparing(subscriber);
    }

    public void SubscribeOnWaveStarts(StageSpawnManager.OnWaveStarts subscriber)
    {
        _stageSpawnManager.SubscribeOnWaveStarts(subscriber);
    }

    public void UnsubscribeOnWaveStarts(StageSpawnManager.OnWaveStarts subscriber)
    {
        _stageSpawnManager.SubscribeOnWaveStarts(subscriber);
    }

    public void SubscribeOnStageLoaded(OnStageLoaded subscriber)
    {
        _stageLoadedSubscribers += subscriber;
    }

    public void UnsubscribeOnStageLoaded(OnStageLoaded subscriber)
    {
        _stageLoadedSubscribers -= subscriber;
    }

    private void OnTowerDestroyed()
    {
        Debug.Log("LOSE!");
        _isGameOver = true;
        _stageSpawnManager.StopStageSpawn();
        // _towerHitPoint.SubscribeOnTowerDestroy(OnTowerDestroyed);
        // _stageSpawnManager.SubscribeOnStageEnd(OnStageEnd);

        _lostStageSubscribersList?.Invoke();
        EnemyFactory.DestroyAllEnemies();
    }

    private void OnStageEnd()
    {
        // _towerHitPoint.SubscribeOnTowerDestroy(OnTowerDestroyed);
        // _stageSpawnManager.SubscribeOnStageEnd(OnStageEnd);
        _winStageSubscribersList?.Invoke();
    }

    private void OnUpdateTowerHealth(float health)
    {
        _towerHealthSubscribersList?.Invoke(health);
    }
}
