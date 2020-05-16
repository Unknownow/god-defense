using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    public GameObject Road;
    public float gridSize = 0.5f;

    public static GameManager Instance() {
        return _instance;
    }

    void Awake() {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("GameController");
        if ( objects.Length > 1 ) {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        if (_instance == null) {
            _instance = this;
        }
    }
}
