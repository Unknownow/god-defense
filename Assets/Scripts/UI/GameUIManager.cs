using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

public class GameUIManager : MonoBehaviour
{

    // Canvas
    public GameObject ingameCanvas;
    public GameObject pasueMenuCanvas;
    public GameObject optionsCanvas;
    public GameObject dialogCanvas;
    public GameObject defeatedCanvas;
    public GameObject victoryCanvas;

    public GameObject loader;

    // Managers
    private GameStateManager gsm;

    private StageTimerManager stageTimerManager;

    // UI Objects
    public Image healthBar;

    public TextMeshProUGUI notiText;

    public TextMeshProUGUI timeText;


    void Awake()
    {
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

    public void pauseClick()
    {
        // Pause game
        pasueMenuCanvas.SetActive(true);
        gsm.Pause();
    }

    public void onResumeClick()
    {
        pasueMenuCanvas.SetActive(false);
        gsm.Resume();
    }

    public void onOptionsClick()
    {
        pasueMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }

    public void onBackToMenuClick()
    {
        dialogCanvas.SetActive(true);
    }

    public void onCancelClick()
    {
        dialogCanvas.SetActive(false);
    }

    public void onYesClick()
    {
        Debug.Log("Returned to Menu");
        // Application.LoadLevel(0);
        SceneManager.LoadScene(0);
    }

    public void onReplayClick()
    {
        defeatedCanvas.SetActive(false);
        loader.SetActive(true);
        // gsm.PrepareStage(0);
        //TODO: làm replay.
    }

    public void onNextStageClick()
    {
        victoryCanvas.SetActive(false);
        // TODO: next stage
        loader.SetActive(true);
    }

    public void onOptionBackClick()
    {
        pasueMenuCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
    }

    private void OnTowerHit(float heal)
    {
        healthBar.fillAmount = heal;
    }

    private void OnTimeUpdate(int currentTime)
    {
        TimeSpan t = TimeSpan.FromSeconds(currentTime);

        string timeStr = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);

        timeText.SetText(timeStr);
    }

    private void OnStartStage()
    {
        notiText.SetText("Stage started");
        notiText.alpha = 1.0f;

        notiText.CrossFadeAlpha(0, 1, true);
        GameObject.FindObjectOfType<EventSystem>().enabled = true;
    }

    private void OnWin()
    {
        victoryCanvas.SetActive(true);

        // TODO: Save stage
        Save save = new Save(gsm.GetCurrentStage() + 1);
        SaveLoadSystem.Save(save);
    }

    private void OnDefeated()
    {
        // notiText.SetText("Defeated");
        // notiText.alpha = 1.0f;

        // notiText.CrossFadeAlpha(1, 0.1f, true);

        // notiText.CrossFadeAlpha(0, 1, true);

        defeatedCanvas.SetActive(true);

    }

    private void OnWavePreparing(float time)
    {
        StartCoroutine(OnWaveDelayPreparing(time));

    }

    private IEnumerator OnWaveDelayPreparing(float time)
    {
        float waitTime = Mathf.Max(0, time - 3);
        yield return new WaitForSeconds(waitTime);

        // yield return StartCoroutine(HandleTimeDelay("3"));
        // yield return StartCoroutine(HandleTimeDelay("2"));
        // yield return StartCoroutine(HandleTimeDelay("1"));

    }

    public void OnStageLoaded()
    {
        CountdownBeforeStart();
    }
    private async void CountdownBeforeStart()
    {
        GameObject.FindObjectOfType<EventSystem>().enabled = false;
        GameManager.Instance().UpdateGround();
        notiText.gameObject.SetActive(false);
        notiText.gameObject.SetActive(true);
        timeText.SetText("00:00");
        healthBar.fillAmount = 1.0f;
        int i = 3;
        while (i > 0)
        {
            notiText.SetText(i.ToString());
            // notiText.CrossFadeAlpha(255, 0.5f, true);
            notiText.alpha = 1;
            await Task.Delay(System.TimeSpan.FromSeconds(0.5f));
            // notiText.CrossFadeAlpha(0, 0.5f, true);
            notiText.alpha = 0;
            await Task.Delay(System.TimeSpan.FromSeconds(0.5f));
            i--;
        }
        gsm.StartStage();
    }

    private void HandleTimeDelay(string time)
    {
        notiText.SetText(time);

        Debug.Log("X");

    }


    private void OnWaveStarts()
    {
        // StartCoroutine(HandleTimeDelay("Wave Started"));
    }
}
