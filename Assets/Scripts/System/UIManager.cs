using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public GameObject header;
    public GameObject start;
    public GameObject buttonPlay;
    // public GameObject buttonAbout;
    public GameObject buttonSettings;
    // public GameObject buttonHelp;
    // public GameObject windowAbout;
    public GameObject windowSettings;
    public GameObject windowNewGame;
    // public GameObject loader;
    public Sprite soundsOn;
    public Sprite soundsOff;
    public Sprite musicOn;
    public Sprite musicOff;
    public Sprite tableInactive;
    public Sprite tableActive;
    public AudioClip menuClick;
    [SerializeField] private Text helpCounter;
    // private GameManager gm;
    private int currHelp;

    void Start() {
        // gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        GotoMain();
    }

    public void GotoStart() {
        ToggleHeader(true);
        ToggleStart(true);
        ToggleMain(false);
        ToggleAbout(false);
        ToggleSettings(false);
        ToggleNewGame(false);
    }

    public void GotoMain() {
        ToggleHeader(true);
        ToggleStart(false);
        ToggleMain(true);
        ToggleAbout(false);
        ToggleSettings(false);
        ToggleNewGame(false);
    }

    public void GotoAbout() {
        ToggleHeader(false);
        ToggleStart(false);
        ToggleMain(false);
        ToggleAbout(true);
        ToggleSettings(false);
        ToggleNewGame(false);
    }

    public void GotoAchievements() {
        ToggleHeader(false);
        ToggleStart(false);
        ToggleMain(false);
        ToggleAbout(false);
        ToggleSettings(false);
        ToggleNewGame(false);
    }

    public void GotoSettings() {
        ToggleHeader(false);
        ToggleStart(false);
        ToggleMain(false);
        ToggleAbout(false);
        ToggleSettings(true);
        ToggleNewGame(false);

        // if ( prefs.GetSounds() ) {
        //     GameObject.Find("ButtonSounds").GetComponent<Image>().sprite = soundsOn;
        // } else {
        //     GameObject.Find("ButtonSounds").GetComponent<Image>().sprite = soundsOff;
        // }
        // if ( prefs.GetMusic() ) {
        //     GameObject.Find("ButtonMusic").GetComponent<Image>().sprite = musicOn;
        // } else {
        //     GameObject.Find("ButtonMusic").GetComponent<Image>().sprite = musicOff;
        // }
    }

    public void GotoNewGame() {
        ToggleHeader(false);
        ToggleStart(false);
        ToggleMain(false);
        ToggleAbout(false);
        ToggleSettings(false);
        ToggleNewGame(true);
    }

    public void GotoStartGame() {
        // loader.SetActive(true);
        SceneManager.LoadScene(1);
        //Debug.Log("DM An roi deo chay");
    }

    public void ClickSounds() {
        // prefs.ToggleSounds();
        // if ( prefs.GetSounds() ) {
        //     GameObject.Find("ButtonSounds").GetComponent<Image>().sprite = soundsOn;
        // } else {
        //     GameObject.Find("ButtonSounds").GetComponent<Image>().sprite = soundsOff;
        // }
        // if ( prefs.GetSounds() ) { source.PlayOneShot(menuClick, 1.0f); }
    }

    public void ClickMusic() {
        // prefs.ToggleMusic();
        // if ( prefs.GetMusic() ) {
        //     GameObject.Find("ButtonMusic").GetComponent<Image>().sprite = musicOn;
        // } else {
        //     GameObject.Find("ButtonMusic").GetComponent<Image>().sprite = musicOff;
        // }
        // MusicPlay();
        // if ( prefs.GetSounds() ) { source.PlayOneShot(menuClick, 1.0f); }
    }

    public void MusicPlay() {
        // if ( prefs.GetMusic() ) { source.Play(); } else { source.Stop(); }
    }

    private void ToggleHeader(bool flag) {
        header.SetActive(flag);
    }

    private void ToggleStart(bool flag) {
        start.SetActive(flag);
    }

    private void ToggleMain(bool flag) {
        buttonPlay.SetActive(flag);
        // buttonAbout.SetActive(flag);
        buttonSettings.SetActive(flag);
        // buttonHelp.SetActive(flag);
    }

    private void ToggleAbout(bool flag) {
        // windowAbout.SetActive(flag);
    }

    private void ToggleSettings(bool flag) {
        windowSettings.SetActive(flag);
    }

    private void ToggleNewGame(bool flag) {
        windowNewGame.SetActive(flag);
    }
}
