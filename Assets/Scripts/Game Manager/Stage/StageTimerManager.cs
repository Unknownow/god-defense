﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageTimerManager : MonoBehaviour
{
    public delegate void OnWaveTimerIncrease(int currentTime);
    private event OnWaveTimerIncrease _waveTimerSubscriberList;

    public delegate void OnStageTimerIncrease(int currentTime);
    private event OnStageTimerIncrease _stageTimerSubscriberList;

    private int _waveTimer;
    public int WaveTimer
    {
        get
        {
            return this._waveTimer;
        }
    }
    private int _stageTimer;
    public int StageTimer
    {
        get
        {
            return this._stageTimer;
        }
    }
    private Coroutine _waveTimerCoroutine;
    private Coroutine _stageTimerCoroutine;

    public void SubscribeOnStageTimerIncrease(OnStageTimerIncrease subscriber)
    {
        _stageTimerSubscriberList += subscriber;
    }

    public void SubscribeOnWaveTimerIncrease(OnWaveTimerIncrease subscriber)
    {
        _waveTimerSubscriberList += subscriber;
    }

    public void UnsubscribeOnStageTimerIncrease(OnStageTimerIncrease subscriber)
    {
        _stageTimerSubscriberList -= subscriber;
    }

    public void UnsubscribeOnWaveTimerIncrease(OnWaveTimerIncrease subscriber)
    {
        _waveTimerSubscriberList -= subscriber;
    }

    public void StartStageTimer()
    {
        _stageTimer = 0;
        if (_stageTimerCoroutine != null)
            StopCoroutine(_stageTimerCoroutine);
        _stageTimerCoroutine = StartCoroutine(StageTimerCoroutine());
    }

    public void ResumeStageTimer()
    {
        _stageTimerCoroutine = StartCoroutine(StageTimerCoroutine());
    }
    public void StopStageTimer()
    {
        StopCoroutine(_stageTimerCoroutine);
    }

    public void StartWaveTimer()
    {
        _waveTimer = 0;
        if (_waveTimerCoroutine != null)
            StopCoroutine(_waveTimerCoroutine);
        _waveTimerCoroutine = StartCoroutine(WaveTimerCoroutine());
    }

    public void StopWaveTimer()
    {
        StopCoroutine(_waveTimerCoroutine);
    }

    public void ResumeWaveTimer()
    {
        _waveTimerCoroutine = StartCoroutine(WaveTimerCoroutine());
    }

    private IEnumerator WaveTimerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            _waveTimer += 1;
            _waveTimerSubscriberList?.Invoke(_waveTimer);
        }
    }

    private IEnumerator StageTimerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            _stageTimer += 1;
            _stageTimerSubscriberList?.Invoke(_stageTimer);
        }
    }
}