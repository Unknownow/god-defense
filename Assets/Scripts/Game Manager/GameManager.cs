using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    public GameObject Road;
    public float gridSize = 0.5f;

    private Collider[] roadColliders;

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

        roadColliders = GameObject.FindGameObjectsWithTag("Ground").Select(obj => obj.GetComponent<Collider>()).ToArray();

        Debug.Log("Length road colliders: " + roadColliders.Length);
    }

    public void UpdateGround() {
        roadColliders = GameObject.FindGameObjectsWithTag("Ground").Select(obj => obj.GetComponent<Collider>()).ToArray();

        Debug.Log("Length road colliders: " + roadColliders.Length);
    }

    public Collider[] getRoadColliders() {
        return this.roadColliders;
    }

    
}
