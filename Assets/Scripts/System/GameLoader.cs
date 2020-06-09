using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class GameLoader : MonoBehaviour {
    public GameObject loadingCanvas;
    public GameObject loadingBar;
    public Text bottomText;
    private Slider slider;

    private float currentProgress = 0f;

    [SerializeField]
    private GameStateManager gameStateManager;

    private bool isLoaded = true;

    private bool isConcurrentDone = false;

    void Awake() {
        gameStateManager = GameObject.FindWithTag("Manager").GetComponent<GameStateManager>();
        // loadingCanvas.SetActive(true);
        slider = loadingBar.GetComponent<Slider>();
        // gameStateManager.SubscribeOnStageLoaded(OnStageLoaded);
        // LoadGame();
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
            
            LoadAsynchronously();
        }
    }

    async void LoadAsynchronously() {
        isLoaded = false;
        isConcurrentDone = false;
        Save save = SaveLoadSystem.Load();
        if (save == null) {
            save = new Save(0);
        }
        
        //Fake progress
        bottomText.text = "Loading map...";
        await Task.Delay(System.TimeSpan.FromSeconds(Random.Range(1.0f, 2.0f)));
        currentProgress = Random.Range(0.3f, 0.5f);
        slider.value = currentProgress;
        Debug.Log("Still Loading");

        bottomText.text = "Loading assets...";
        await Task.Delay(System.TimeSpan.FromSeconds(Random.Range(1.0f, 2.0f)));
        currentProgress = Random.Range(currentProgress, 0.9f);
        slider.value = currentProgress;

        bottomText.text = "Loading...";
        await Task.Delay(System.TimeSpan.FromSeconds(Random.Range(1.0f, 2.0f)));
        isConcurrentDone = true;
        Debug.Log(gameStateManager);
        gameStateManager.PrepareStage(save.StageIndex);

        OnStageLoaded();
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
