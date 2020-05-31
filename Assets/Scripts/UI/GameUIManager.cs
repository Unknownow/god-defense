using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameUIManager : MonoBehaviour
{

    // Canvas
    public GameObject ingameCanvas;
    public GameObject pasueMenuCanvas;
    public GameObject optionsCanvas;
    public GameObject dialogCanvas;

    // Managers
    private GameStateManager gsm;

    private StageTimerManager stageTimerManager;

    // UI Objects
    public Image healthBar;

    public TextMeshProUGUI notiText;

    public TextMeshProUGUI timeText;


    void Start() {
        gsm = GameObject.Find("GameManager").GetComponent<GameStateManager>();
        stageTimerManager = GameObject.Find("GameManager").GetComponent<StageTimerManager>();

        notiText.alpha = 0;

        gsm.SubscribeOnUpdateTowerHealth(OnTowerHit);
        gsm.SubscribeOnStageStart(OnStartStage);
        gsm.SubscribeOnWinStage(OnWin);
        gsm.SubscribeOnLostStage(OnDefeated);
        gsm.SubscribeOnWavePreparing(OnWavePreparing);
        gsm.SubscribeOnWaveStarts(OnWaveStarts);

        stageTimerManager.SubscribeOnStageTimerIncrease(OnTimeUpdate);
    }

    public void pauseClick() {
        // Pause game
        pasueMenuCanvas.SetActive(true);
        stageTimerManager.StopStageTimer();
    }

    public void onResumeClick() {
        pasueMenuCanvas.SetActive(false);
        stageTimerManager.ResumeStageTimer();
    }

    public void onOptionsClick() {
        pasueMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }

    public void onBackToMenuClick() {
        dialogCanvas.SetActive(true);
    }

    public void onCancelClick() {
        dialogCanvas.SetActive(false);
    }

    public void onYesClick() {
        Debug.Log("Returned to Menu");
        Application.LoadLevel(0);
    }

    public void onOptionBackClick() {
        pasueMenuCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
    }

    private void OnTowerHit(float heal) {
        healthBar.fillAmount = heal;
    }

    private void OnTimeUpdate(int currentTime) {
        TimeSpan t = TimeSpan.FromSeconds( currentTime );

        string timeStr = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);

        timeText.SetText(timeStr);
    }

    private void OnStartStage() {
        notiText.SetText("Stage started");
        notiText.alpha = 1.0f;

        notiText.CrossFadeAlpha(0, 1, true);
    }

    private void OnWin() {

    }

    private void OnDefeated() {
        notiText.SetText("Defeated");
        notiText.alpha = 1.0f;

        notiText.CrossFadeAlpha(1,0.1f, true);

        // notiText.CrossFadeAlpha(0, 1, true);
    }

    private void OnWavePreparing(float time) {
        StartCoroutine(OnWaveDelayPreparing(time));

    }

    private IEnumerator OnWaveDelayPreparing(float time) {
        float waitTime = Mathf.Max(0, time - 3);
        yield return new WaitForSeconds(waitTime);
        
        yield return StartCoroutine(HandleTimeDelay("3"));
        yield return StartCoroutine(HandleTimeDelay("2"));
        yield return StartCoroutine(HandleTimeDelay("1"));

    }

    private IEnumerator HandleTimeDelay(string time) {
        notiText.SetText(time);
        notiText.CrossFadeAlpha(1, 0.1f, true);
        yield return new WaitForSeconds(0.1f);
        notiText.CrossFadeAlpha(0, 0.9f, true);
        yield return new WaitForSeconds(0.9f);

    }


    private void OnWaveStarts() {
        StartCoroutine(HandleTimeDelay("Wave Started"));
    }
}
