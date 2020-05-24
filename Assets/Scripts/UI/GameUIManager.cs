using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{

    public GameObject ingameCanvas;
    public GameObject pasueMenuCanvas;
    public GameObject optionsCanvas;
    public GameObject dialogCanvas;
    public void pauseClick() {
        // Pause game
        pasueMenuCanvas.SetActive(true);
    }

    public void onResumeClick() {
        pasueMenuCanvas.SetActive(false);
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
}
