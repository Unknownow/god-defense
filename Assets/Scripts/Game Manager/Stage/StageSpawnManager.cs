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
    private WaveSpawnManager _waveManager;

    private void Awake()
    {
        _timer = gameObject.GetComponent<StageTimerManager>();
        _waveManager = gameObject.GetComponent<WaveSpawnManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            LoadStageDetail(0);
            StartStage();
        }
    }

    public void LoadStageDetail(int stageIndex)
    {
        GetStagePattern(stageIndex);
    }

    public void StartStage()
    {
        _currentWaveCount = 0;
        _waveManager.SubscribeOnWaveEnd(OnWaveEnd);
        _timer.StartStageTimer();
        _waveManager.StartWave(_currentStage.waves[_currentWaveCount]);
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
        _currentStage = Utils.ReadJsonFile<Stage>("JSON/Stages/Stage" + stageIndex);
        foreach (Wave wave in _currentStage.waves)
        {
            Array.Sort(wave.enemies);
        }
    }

    private void EndStage()
    {
        Debug.Log("stage " + _currentStage.stageIndex + " ended");
        _timer.StopStageTimer();
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
            _waveManager.StartWave(_currentStage.waves[_currentWaveCount]);
    }
}
