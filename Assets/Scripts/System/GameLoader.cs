using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour {
    public GameObject loadingCanvas;
    public GameObject loadingBar;
    public Text bottomText;
    private Slider slider;

    private float currentProgress = 0f;

    private GameStateManager gameStateManager;

    private bool isLoaded = false;

    private bool isConcurrentDone = false;

    void Start() {
        gameStateManager = GameObject.FindWithTag("Manager").GetComponent<GameStateManager>();
        loadingCanvas.SetActive(true);
        slider = loadingBar.GetComponent<Slider>();
        LoadGame();
    }

    public void LoadGame() {
        isLoaded = false;
        gameStateManager.SubscribeOnStageLoaded(OnStageLoaded);
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously() {
        gameStateManager.PrepageStage(0);
        //Fake progress
        bottomText.text = "Loading map...";
        yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        currentProgress = Random.Range(0.3f, 0.5f);
        slider.value = currentProgress;

        bottomText.text = "Loading assets...";
        yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        currentProgress = Random.Range(currentProgress, 0.9f);
        slider.value = currentProgress;

        bottomText.text = "Loading...";

        isConcurrentDone = true;

        if (isLoaded) {
            loadingCanvas.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    private void OnStageLoaded() {
        isLoaded = true;
        
        if (isConcurrentDone) {
            loadingCanvas.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
