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
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");
        if ( objs.Length > 1 ) {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        if (_instance == null) {
            _instance = this;
        }
    }
}
