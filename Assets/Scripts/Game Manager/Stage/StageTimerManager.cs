using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTimerManager : MonoBehaviour
{
    public delegate void OnWaveTimerIncrease(int currentTime, out bool isDone);
    private event OnWaveTimerIncrease _waveTimerSubscriberList;

    public delegate void OnStageTimerIncrease(int currentTime, out bool isDone);
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
        _stageTimerCoroutine = StartCoroutine(WaveTimerCoroutine());
    }

    public void PauseStageTimer()
    {
        StopCoroutine(_stageTimerCoroutine);
        PauseWaveTimer();
    }

    public void ResumeStageTimer()
    {
        _stageTimerCoroutine = StartCoroutine(StageTimerCoroutine());
        ResumeWaveTimer();
    }

    public void StartWaveTimer()
    {
        _waveTimer = 0;
        _waveTimerCoroutine = StartCoroutine(WaveTimerCoroutine());
    }

    private void PauseWaveTimer()
    {
        StopCoroutine(_waveTimerCoroutine);
    }

    private void ResumeWaveTimer()
    {
        _waveTimerCoroutine = StartCoroutine(WaveTimerCoroutine());
    }

    private IEnumerator WaveTimerCoroutine()
    {
        bool isDone = true;
        while (isDone)
        {
            yield return new WaitForSeconds(1);
            _waveTimer += 1;
            _waveTimerSubscriberList?.Invoke(_waveTimer, out isDone);
        }
        StopCoroutine(_waveTimerCoroutine);
    }

    private IEnumerator StageTimerCoroutine()
    {
        bool isDone = true;
        while (isDone)
        {
            yield return new WaitForSeconds(1);
            _stageTimer += 1;
            _stageTimerSubscriberList?.Invoke(_stageTimer, out isDone);
        }
        StopCoroutine(_stageTimerCoroutine);
    }
}
