using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private TowerHitPointManager _towerHitPoint;
    private bool _isGameOver;
    public bool IsGameOver
    {
        get
        {
            return this._isGameOver;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _towerHitPoint.SubscribeOnTowerDestroy(OnTowerDestroyed);
        _isGameOver = true;
    }

    // Update is called once per frame
    private void OnTowerDestroyed()
    {
        Debug.Log("LOSE!");
        _isGameOver = true;
    }
}
