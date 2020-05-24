using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour {
    public GameObject menuCanvas;
    public GameObject loadingCanvas;
    public GameObject loadingBar;
    public Text bottomText;
    private Slider slider;

    private float currentProgress = 0f;

    void Start() {
        menuCanvas.SetActive(false);
        loadingCanvas.SetActive(true);
        slider = loadingBar.GetComponent<Slider>();
        LoadGame();
    }

    public void LoadGame() {
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously() {
        //Fake progress
        bottomText.text = "Loading map...";
        yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        currentProgress = Random.Range(0.3f, 0.5f);
        slider.value = currentProgress;

        bottomText.text = "Loading assets...";
        yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        currentProgress = Random.Range(currentProgress, 0.9f);
        slider.value = currentProgress;

        AsyncOperation operation = SceneManager.LoadSceneAsync("Map_Hoa");

        while ( !operation.isDone ) {
            slider.value = Mathf.Max(operation.progress / 0.9f, currentProgress);
            yield return null;
        }

        bottomText.text = "Loading...";
        yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        slider.value = 1;

    }
}
