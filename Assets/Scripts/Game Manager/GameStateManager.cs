﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        _stageSpawnManager = gameObject.GetComponent<StageSpawnManager>();
        _stageSpawnManager.SubscribeOnStageEnd(OnStageEnd);
        DontDestroyOnLoad(gameObject);
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
    public void PrepageStage(int stageIndex)
    {
        _isGameOver = false;
        _stageSpawnManager.LoadStageDetail(stageIndex);
        _towerHitPoint = GameObject.FindWithTag("Tower").GetComponent<TowerHitPointManager>();
        if (_towerHitPoint != null)
        {
            _towerHitPoint.SubscribeOnTowerDestroy(OnTowerDestroyed);
            _towerHitPoint.SubscribeOnTowerHit(OnUpdateTowerHealth);
        }
        // _towerHitPoint.SubscribeOnTowerDestroy(OnTowerDestroyed);
        // _stageSpawnManager.SubscribeOnStageEnd(OnStageEnd);
        //TODO: add load map prefab

        // After loaded
        _stageLoadedSubscribers?.Invoke();
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

    public void SubscribeOnStageLoaded(OnStageLoaded subscriber) {
        _stageLoadedSubscribers += subscriber;
    }

    public void UnsubscribeOnStageLoaded(OnStageLoaded subscriber) {
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
