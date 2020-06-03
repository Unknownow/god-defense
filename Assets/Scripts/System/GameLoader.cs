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

    private bool isLoaded = true;

    private bool isConcurrentDone = false;

    void Awake() {
        gameStateManager = GameObject.FindWithTag("Manager").GetComponent<GameStateManager>();
        loadingCanvas.SetActive(true);
        slider = loadingBar.GetComponent<Slider>();
        gameStateManager.SubscribeOnStageLoaded(OnStageLoaded);
        LoadGame();
    }

    private void OnEnable() {
        loadingCanvas.SetActive(true);
        LoadGame();
    }

    public void LoadGame() {
        if (isLoaded) {
            isLoaded = false;
            currentProgress = 0f;
            slider.value = currentProgress;
            
            StartCoroutine(LoadAsynchronously());
            Debug.Log("Started Load");
        }
    }

    IEnumerator LoadAsynchronously() {
        isLoaded = false;
        isConcurrentDone = false;
        Save save = SaveLoadSystem.Load();
        if (save == null) {
            save = new Save(0);
        }
        
        //Fake progress
        bottomText.text = "Loading map...";
        yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        currentProgress = Random.Range(0.3f, 0.5f);
        slider.value = currentProgress;
        Debug.Log("Still Loading");

        bottomText.text = "Loading assets...";
        yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        currentProgress = Random.Range(currentProgress, 0.9f);
        slider.value = currentProgress;

        bottomText.text = "Loading...";

        gameStateManager.PrepareStage(save.StageIndex);

        isConcurrentDone = true;

        if (isLoaded) {
            OnStageLoaded();
        }
    }

    private void OnStageLoaded() {
        isLoaded = true;
        
        if (isConcurrentDone) {
            loadingCanvas.SetActive(false);
            this.gameObject.SetActive(false);

            GameUIManager gameUIManager = GameObject.FindObjectOfType<GameUIManager>();
            gameUIManager.OnStageLoaded();
        }
    }

    private void OnDisable() {
        
    }
}
