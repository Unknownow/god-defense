using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageSpawnManager : MonoBehaviour
{
    [SerializeField]
    private float _delayBetweenWave;
    public delegate void OnStageEnds();
    private event OnStageEnds _onStageEndsSubscribers;
    private Stage _currentStage;
    private int _currentWaveCount;

    private StageTimerManager _timer;
    private WaveSpawnManager _waveSpawnManager;

    private void Awake()
    {
        _timer = gameObject.GetComponent<StageTimerManager>();
        _waveSpawnManager = gameObject.GetComponent<WaveSpawnManager>();
        _waveSpawnManager.SubscribeOnWaveEnd(OnWaveEnd);
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.O))
        // {
        //     LoadStageDetail(0);
        //     StartStage();
        // }
    }

    public void LoadStageDetail(int stageIndex)
    {
        GetStagePattern(stageIndex);
    }

    public void StartStage()
    {
        _currentWaveCount = 0;
        // _waveSpawnManager.SubscribeOnWaveEnd(OnWaveEnd);
        _timer.StartStageTimer();
        _waveSpawnManager.StartWave(_currentStage.waves[_currentWaveCount]);
    }

    public void StopStageSpawn()
    {
        _waveSpawnManager.StopWaveSpawn();
        // _waveSpawnManager.UnsubscribeOnWaveEnd(OnWaveEnd);
        _timer.StopStageTimer();
    }

    public void SubscribeOnStageEnd(OnStageEnds subscriber)
    {
        _onStageEndsSubscribers += subscriber;
    }

    public void UnsubscribeOnStageEnd(OnStageEnds subscriber)
    {
        _onStageEndsSubscribers -= subscriber;
    }

    private void GetStagePattern(int stageIndex)
    {
        _currentStage = Utils.ReadResourcesToJson<Stage>("JSON/Stages/Stage" + stageIndex);
        foreach (Wave wave in _currentStage.waves)
        {
            Array.Sort(wave.enemies);
        }
    }

    private void EndStage()
    {
        Debug.Log("stage " + _currentStage.stageIndex + " ended");
        _timer.StopStageTimer();
        // _waveSpawnManager.UnsubscribeOnWaveEnd(OnWaveEnd);
        _onStageEndsSubscribers?.Invoke();
    }

    private void OnWaveEnd()
    {
        StartCoroutine(OnWaveEndDelay());
    }

    private IEnumerator OnWaveEndDelay()
    {
        yield return new WaitForSeconds(_delayBetweenWave);
        if (++_currentWaveCount >= _currentStage.waves.Length)
            EndStage();
        else
            _waveSpawnManager.StartWave(_currentStage.waves[_currentWaveCount]);
    }
}
