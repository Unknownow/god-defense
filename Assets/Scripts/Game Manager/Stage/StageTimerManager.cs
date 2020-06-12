using System.Collections;
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

    /// <summary>
    /// Phương thức dùng để subscribe sự kiện thời gian tăng lên 1 giây của đồng hồ stage.
    /// </summary>
    /// <param name="subscriber">Phương thức muốn gọi lúc đồng hồ stage tăng lên 1 giây</param>
    public void SubscribeOnStageTimerIncrease(OnStageTimerIncrease subscriber)
    {
        _stageTimerSubscriberList += subscriber;
    }

    /// <summary>
    /// Phương thức dùng để subscribe sự kiện thời gian tăng lên 1 giây của đồng hồ wave.
    /// </summary>
    /// <param name="subscriber">Phương thức muốn gọi lúc đồng hồ wave tăng lên 1 giây</param>
    public void SubscribeOnWaveTimerIncrease(OnWaveTimerIncrease subscriber)
    {
        _waveTimerSubscriberList += subscriber;
    }

    /// <summary>
    /// Phương thức dùng để unsubscribe sự kiện thời gian tăng lên 1 giây của đồng hồ stage.
    /// </summary>
    /// <param name="subscriber">Phương thức muốn unsubscribe</param>
    public void UnsubscribeOnStageTimerIncrease(OnStageTimerIncrease subscriber)
    {
        _stageTimerSubscriberList -= subscriber;
    }

    /// <summary>
    /// Phương thức dùng để unsubscribe sự kiện thời gian tăng lên 1 giây của đồng hồ wave.
    /// </summary>
    /// <param name="subscriber">Phương thức muốn unsubscribe</param>
    public void UnsubscribeOnWaveTimerIncrease(OnWaveTimerIncrease subscriber)
    {
        _waveTimerSubscriberList -= subscriber;
    }

    /// <summary>
    /// Bắt đầu cho đồng hồ stage chạy
    /// </summary>
    public void StartStageTimer()
    {
        _stageTimer = 0;
        if (_stageTimerCoroutine != null)
            StopCoroutine(_stageTimerCoroutine);
        _stageTimerCoroutine = StartCoroutine(StageTimerCoroutine());
    }

    /// <summary>
    /// Cho đồng hồ stage chạy tiếp sau khi pause
    /// </summary>
    public void ResumeStageTimer()
    {
        _stageTimerCoroutine = StartCoroutine(StageTimerCoroutine());
    }

    /// <summary>
    /// Dừng/Tạm dừng đồng hồ stage
    /// </summary>
    public void StopStageTimer()
    {
        StopCoroutine(_stageTimerCoroutine);
    }

    /// <summary>
    /// Bắt đầu cho đồng hồ wave chạy
    /// </summary>
    public void StartWaveTimer()
    {
        _waveTimer = 0;
        if (_waveTimerCoroutine != null)
            StopCoroutine(_waveTimerCoroutine);
        _waveTimerCoroutine = StartCoroutine(WaveTimerCoroutine());
    }

    /// <summary>
    /// Dừng/Tạm dừng đồng hồ stage
    /// </summary>
    public void StopWaveTimer()
    {
        StopCoroutine(_waveTimerCoroutine);
    }

    /// <summary>
    /// Cho đồng hồ wave chạy tiếp sau khi pause
    /// </summary>
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
            foreach(OnStageTimerIncrease func in _stageTimerSubscriberList.GetInvocationList()){
                func.Invoke(_stageTimer);
            }
            // _stageTimerSubscriberList?.Invoke(_stageTimer);
        }
    }
}
